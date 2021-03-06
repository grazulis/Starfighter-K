
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace StarfighterK
{
    /// <summary>
    /// The Starfighter
    /// </summary>
    public class Starfighter
    {
        private readonly BitmapImage _spriteBitmap = new BitmapImage(new Uri("/Resources/Ship.png", UriKind.Relative));

        public double Speed { get; set; }
        public double X { get; set; }
        
        public void Draw(Canvas playfield, double y)
        {
            var i =
                  new Image { Source = _spriteBitmap, Visibility = Visibility.Visible, Name = "Starfighter" };
            if(!(playfield.Children.IndexOf(i)>-1))
            {
                playfield.Children.Add(i);
            }
            

            Canvas.SetLeft(i, X);
            Canvas.SetTop(i, y );
            
        }

        public bool CheckCollision(Wall wall)
        {
            //TODO Add properties to wall to calculate position of left and right wall
            return (X < wall.Left )
                || (X > wall.Right )
                ;
        }
    }
}