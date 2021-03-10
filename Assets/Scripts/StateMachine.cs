using System.Collections.Generic;

namespace StateMachine
{
  public class State
  {
    protected FSM m_fsm;
    public State(FSM fsm)
    {
      m_fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
  }

  public class FSM
  {
    protected List<State> m_states;
    protected State m_currentState;

    public void Add(State state)
    {
      m_states.Add(state);
    }

    public void SetCurrentState(State state)
    {
      if (m_currentState != null)
      {
        m_currentState.Exit();
      }

      m_currentState = state;

      if (m_currentState != null)
      {
        m_currentState.Enter();
      }
    }

    public void Update()
    {
      if (m_currentState != null)
      {
        m_currentState.Update();
      }
    }
  }
}
