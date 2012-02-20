using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace StarfighterK
{
    public class Wall
    {
        /// <summary>
        /// Width from centre line to wall in pixels
        /// </summary>
        public int Width { get; set; }
        public int X { get; set; }
        private BitmapImage _spriteBitmap = new BitmapImage(new Uri("/Resources/Wall.png", UriKind.Relative));
        private bool _goLeft;

        public void Draw(double wallY, Canvas playfield)
        {

            var leftImage =
                  new Image { Source = _spriteBitmap, Visibility = Visibility.Visible};

            var rightImage =
                  new Image { Source = _spriteBitmap, Visibility = Visibility.Visible };

                playfield.Children.Add(leftImage);
            playfield.Children.Add(rightImage);

            Canvas.SetLeft(leftImage, X-(Width));
            Canvas.SetTop(leftImage, wallY );

            Canvas.SetLeft(rightImage, X+(Width));
            Canvas.SetTop(rightImage, wallY );

            //This is temporary, needs to inherit xy from previous and 
            //be managed in the tnnel code.
            //if(_goLeft)
            //{
            //    X--;
            //    _goLeft = X >= 50;
            //}
            //else
            //{
            //    X++;
            //    _goLeft = X >= 250;
            //}
            
        }
        
    }
}