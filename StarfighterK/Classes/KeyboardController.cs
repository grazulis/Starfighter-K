
using System.Windows.Input;

namespace StarfighterK
{
    /// <summary>
    /// A keyboard controller for the starfighter
    /// which can be used for testing purposes
    /// </summary>
    public class KeyboardController : BaseFighterController, IFighterController
    {
        public void CheckInput(Starfighter starfighter)
        {
            if (starfighter.Speed == 0 && Keyboard.IsKeyDown(Key.Up))
                starfighter.Speed = 19;
            if(Keyboard.IsKeyDown(Key.Left)) MoveLeft(starfighter);
            if(Keyboard.IsKeyDown(Key.Right)) MoveRight(starfighter);
            if(Keyboard.IsKeyDown(Key.Up)) IncreaseSpeed(starfighter);
            if(Keyboard.IsKeyDown(Key.Down)) DecreaseSpeed(starfighter);
        }

        
    }
}