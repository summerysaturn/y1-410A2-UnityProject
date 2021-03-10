using StateMachine;

public class GameState : State
{
  public GameState (FSM fsm) : base(fsm)
  {

  }

  public delegate void StateDelegate();

  public StateDelegate OnEnterDelegate { get; set; } = null;
  public StateDelegate OnExitDelegate { get; set; } = null;
  public StateDelegate OnUpdateDelegate { get; set; } = null;
  public StateDelegate OnFixedUpdateDelegate { get; set; } = null;

  public override void Enter()
  {
    OnEnterDelegate?.Invoke();
  }
  public override void Exit()
  {
    OnExitDelegate?.Invoke();
  }
  public override void Update()
  {
    OnUpdateDelegate?.Invoke();
  }
}
