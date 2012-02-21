using System.Collections.Generic;
using System.Windows.Controls;

namespace StarfighterK
{
    public class Tunnel
    {
        public Tunnel()
        {
            Walls = new List<Wall>();
            for (int i = 0; i < 20; i++)
            {
                Walls.Add(new Wall { Width = 180, X= 320});
            }
        }

        public List<Wall> Walls { get; set; }

        public void Fly(Canvas playfield, double starshipY)
        {
            MoveWall(starshipY+16, playfield);
        }

        private bool _goLeft;
        private void MoveWall(double wallY, Canvas playfield)
        {
            for (var i = 0; i < Walls.Count; i++ )
            {
                var wall = Walls[i];
                wall.Draw(wallY, playfield);
                wallY = wallY - 16;
                if (i < Walls.Count - 1)
                {
                    wall.X = Walls[i+1].X;
                }
                else
                {
                    CheckEdgeCollision(wall, playfield.ActualWidth - 8);

                    if(_goLeft)
                    {
                        wall.X = wall.X - 8;
                        
                    }
                    else
                    {
                        wall.X = wall.X + 8;
                    }
                }
            }
        }

        private void CheckEdgeCollision(Wall wall, double tunnelWidth)
        {
            if(wall.X + wall.Width >= tunnelWidth)
            {
                _goLeft = true;

            }else if (wall.X- wall.Width <= 0)
            {
                _goLeft = false;

            }
        }
    }
}