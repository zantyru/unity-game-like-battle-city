namespace Game
{
    public interface IActeable
    {
        void Do(ActionData actionData, float deltaTime);
    }
}