public abstract class BaseState
{
    public StateMachine stateMachine;
    public Enemy enemy;
    public abstract void Enter();
    public abstract void Perform(double speed);
    public abstract void Exit();
}