using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace StarfighterK
{
    /// <summary>
    /// The wall object is a single segment of the tunnel with a wall at each side
    /// </summary>
    public class Wall
    {
        /// 
        
        public Wall()
        {
            _leftImage.Source = _spriteBitmap;
            _rightImage.Source = _spriteBitmap;
        }

        /// <summary>
        /// Width from centre line to wall in pixels
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The location of the mid-point between the walls
        /// </summary>
        public int X { get; set; }
        public double Left { get { return X - Width; } }
        public double Right { get { return X + Width - 4; } }
        private BitmapImage _spriteBitmap = new BitmapImage(new Uri("/Resources/Wall.png", UriKind.Relative));

        private readonly Image _leftImage =
            new Image();

        private readonly Image _rightImage =
            new Image();

        public void Draw(double wallY, Canvas playfield)
        {
            playfield.Children.Add(_leftImage);
            playfield.Children.Add(_rightImage);

            Canvas.SetLeft(_leftImage, X-(Width));
            Canvas.SetTop(_leftImage, wallY );

            Canvas.SetLeft(_rightImage, X+(Width));
            Canvas.SetTop(_rightImage, wallY );

        }
        
    }
}