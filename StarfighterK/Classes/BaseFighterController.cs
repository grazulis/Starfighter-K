namespace StarfighterK
{
    public class BaseFighterController
    {
        public void IncreaseSpeed(Starfighter starfighter)
        {
            if (starfighter.Speed < 30)
                starfighter.Speed += 5;
        }

        public void DecreaseSpeed(Starfighter starfighter)
        {
            if (starfighter.Speed > 5)
                starfighter.Speed -= 5;
            else
                starfighter.Speed = 5;
        }

        public void MoveRight(Starfighter starfighter)
        {
            starfighter.X += 10;
        }

        public void MoveLeft(Starfighter starfighter)
        {
            starfighter.X -= 10;
        }
    }
}