using System;
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
                Walls.Add(new Wall { Width = 200, X= 320});
            }
        }

        public List<Wall> Walls { get; set; }

        public void Fly(Canvas playfield, double starshipY)
        {
            MoveWall(starshipY+16, playfield);
        }

        private enum Move{Left, Straight, Right}

        private int _directionLength = 30;
        private int _directionCounter = 0;
        private Random _random = new Random();
        private Move _direction = Move.Straight;

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
                    if(_directionCounter >= _directionLength)
                    {
                        ChangeDirection();
                    }

                    CheckEdgeCollision(wall, playfield.ActualWidth - 8);

                    if(_direction == Move.Left)
                    {
                        wall.X = wall.X - 8;
                        
                    }
                    else if (_direction == Move.Right)
                    {
                        wall.X = wall.X + 8;
                    }
                }
            }
            _directionCounter++;
        }

        private void ChangeDirection()
        {
            var _rndValue = _random.Next(10);
            if(_rndValue < 2)
            {
                _direction = Move.Straight;
            }
            else if (_rndValue < 6)
            {
                _direction = Move.Left;
            }
            else
            {
                _direction = Move.Right;
            }

            _directionCounter = 0;
            _directionLength = _random.Next(20);
        }

        private void CheckEdgeCollision(Wall wall, double tunnelWidth)
        {
            if (wall.X + wall.Width >= tunnelWidth && _direction == Move.Right)
            {
                _direction = Move.Left;

            }
            else if (wall.X - wall.Width <= 0 && _direction == Move.Left)
            {
                _direction = Move.Right;
            }
        }
    }
}