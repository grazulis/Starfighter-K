using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using StarfighterK.Properties;

namespace StarfighterK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        bool _runningGameThread = true;
        DateTime _predNextFrame = DateTime.Now;
        private double targetFramerate = 20;
        double _actualFrameTime;
        private DateTime _lastFrameDrawn;
        private int _frameCount;
        private const int TimerResolution = 2;
        private const int MinFramerate = 2;

        private readonly Starfighter _starfighter = new Starfighter();
         readonly IFighterController _fighterController = new KeyboardController();
        //private readonly IFighterController _fighterController = new KinectController();
        private double _starshipY = 0;

        private Tunnel _tunnel;
        private int _score;
        
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartThread()
        {
            Win32.timeBeginPeriod(2);
            var gameThread = new Thread(GameThread);
            gameThread.SetApartmentState(ApartmentState.STA);
            gameThread.Start();
        }


        private void GameThread()
        {
            _runningGameThread = true;
            _predNextFrame = DateTime.Now;

            _actualFrameTime = 1000.0/targetFramerate;

            // Try to dispatch at as constant of a framerate as possible by sleeping just enough since
            // the last time we dispatched.
            while (_runningGameThread)
            {
                //// Calculate average framerate.  
                DateTime now = DateTime.Now;
                if (_lastFrameDrawn == DateTime.MinValue)
                    _lastFrameDrawn = now;
                double ms = now.Subtract(_lastFrameDrawn).TotalMilliseconds;
                _actualFrameTime = _actualFrameTime*0.95 + 0.05*ms;
                _lastFrameDrawn = now;

                // Adjust target framerate down if we're not achieving that rate
                _frameCount++;
                //if (((_frameCount%100) == 0) && (1000.0/_actualFrameTime < targetFramerate*0.92) && (_starfighter.Speed > _actualFrameTime))
                //    targetFramerate = Math.Max(MinFramerate, (targetFramerate + 1000.0/_actualFrameTime)/2);

                if (now > _predNextFrame)
                    _predNextFrame = now;
                else
                {
                    double msSleep = _predNextFrame.Subtract(now).TotalMilliseconds;
                    if (msSleep >= TimerResolution)
                        Thread.Sleep((int) (msSleep + 0.5));
                }
                _predNextFrame += TimeSpan.FromMilliseconds(1000.0/targetFramerate);

                Dispatcher.Invoke(DispatcherPriority.Send,
                                  new Action<int>(HandleGameTimer), 0);
            }
        }


        private void HandleGameTimer(int param)
        {
            //Check input and move starfighter
            _fighterController.CheckInput(_starfighter);

            if (_starfighter.Speed > 0)
            {
                if (_starfighter.Speed > 0)
                {
                    targetFramerate = _starfighter.Speed;
                }

                canvas1.Children.Clear();

                _starfighter.Draw(canvas1, _starshipY);
                _tunnel.Fly(canvas1, _starshipY);

                IncrementScore();

                //Update Video
                video.Source = _fighterController.GetType() == typeof (KinectController)
                                   ? ((KinectController) _fighterController).video.Source
                                   : null;

                //messageText.Text = string.Format("FR:{0} AR:{1} SR:{2}", targetFramerate, _actualFrameTime, _starfighter.Speed);
                //canvas1.Children.Add(messageText);
                
                //CHeck if collided and run game over))
                if (_starfighter.CheckCollision(_tunnel.Walls[0]))
                {
                    GameOver();
                }
            }
        }

        private void IncrementScore()
        {
            if (_starfighter != null) _score += (int)_starfighter.Speed;
            score.Text = string.Format("{0}", _score);
            //canvas1.Children.Add(score);
        }

        private void GameOver()
        {
            _runningGameThread = false;
            messageText.Text = "Game Over!";
            canvas1.Children.Add(messageText);
        }

        public void SetupScreen()
        {

            _starfighter.Speed = 0;
            _starfighter.X = canvas1.Width/2;
            _starshipY = canvas1.Height - 30;

            _tunnel = new Tunnel();

        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            SetupScreen();
            StartThread();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _runningGameThread = false;
            Properties.Settings.Default.Save();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //TODO If add speech recognition
            //if (recognizer != null)
            //    recognizer.Stop();
            
            if(_fighterController.GetType() == typeof(KinectController))
            {
                ((KinectController)_fighterController).CloseController();
            }
            Environment.Exit(0);
        }



    }

    public class Win32
    {
        [DllImport("Winmm.dll")]
        public static extern int timeBeginPeriod(UInt32 uPeriod);
    }
}
