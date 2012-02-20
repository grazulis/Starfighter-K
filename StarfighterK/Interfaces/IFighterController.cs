namespace StarfighterK
{
    public interface IFighterController
    {
        void CheckInput(Starfighter starfighter);
        void IncreaseSpeed(Starfighter starfighter);
        void DecreaseSpeed(Starfighter starfighter);
        void MoveRight(Starfighter starfighter);
        void MoveLeft(Starfighter starfighter);

    }
}