using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public enum GameStateType
{
  Menu = 0,
  Cutscene,
  Rest,
  Battle,
  BossBattle,
}

public class GameState : State
{
  public GameStateType ID { get { return _id; } }

  protected LogicController _logicController = null;
  protected GameStateType _id;

  public GameState(FSM fsm, LogicController logicController) : base(fsm)
  {
    _logicController = logicController;
  }

  public GameState(LogicController logicController) : base()
  {
    _logicController = logicController;
    m_fsm = _logicController.logicFSM;
  }

  public override void Enter()
  {
    base.Enter();
  }
  public override void Exit()
  {
    base.Exit();
  }
  public override void Update()
  {
    base.Update();
  }
  public override void FixedUpdate()
  {
    base.FixedUpdate();
  }
}

public class GameState_Menu : GameState
{
  public GameState_Menu(LogicController logicController) : base(logicController)
  {
    _id = GameStateType.Menu;
  }

  // add functionality after the base.Update()
  public override void Enter()
  {
    base.Enter();

    Debug.Log("Hey!");
    _logicController.logicFSM.SetCurrentState(GameStateType.Cutscene);
  }
  public override void Exit()
  {
    base.Exit();
    Debug.Log("Making cutscene happen?");
  }
  public override void Update()
  {
    base.Update();
  }
  public override void FixedUpdate()
  {
    base.FixedUpdate();
  }
}

public class GameState_Cutscene : GameState
{
  public GameState_Cutscene(LogicController logicController) : base(logicController)
  {
    _id = GameStateType.Cutscene;
  }

  // add functionality after the base.Update()
  public override void Enter()
  {
    base.Enter();
    Debug.Log("Cutscene!!!!");
  }
  public override void Exit()
  {
    base.Exit();
  }
  public override void Update()
  {
    base.Update();
  }
  public override void FixedUpdate()
  {
    base.FixedUpdate();
  }
}

public class GameState_Rest : GameState
{
  public GameState_Rest(LogicController logicController) : base(logicController)
  {
    _id = GameStateType.Rest;
  }

  // add functionality after the base.Update()
  public override void Enter()
  {
    base.Enter();
  }
  public override void Exit()
  {
    base.Exit();
  }
  public override void Update()
  {
    base.Update();
  }
  public override void FixedUpdate()
  {
    base.FixedUpdate();
  }
}

public class GameState_Battle : GameState
{
  public GameState_Battle(LogicController logicController) : base(logicController)
  {
    _id = GameStateType.Battle;
  }

  // add functionality after the base.Update()
  public override void Enter()
  {
    base.Enter();
  }
  public override void Exit()
  {
    base.Exit();
  }
  public override void Update()
  {
    base.Update();
  }
  public override void FixedUpdate()
  {
    base.FixedUpdate();
  }
}

public class GameState_BossBattle : GameState
{
  public GameState_BossBattle(LogicController logicController) : base(logicController)
  {
    _id = GameStateType.BossBattle;
  }

  // add functionality after the base.Update()
  public override void Enter()
  {
    base.Enter();
  }
  public override void Exit()
  {
    base.Exit();
  }
  public override void Update()
  {
    base.Update();
  }
  public override void FixedUpdate()
  {
    base.FixedUpdate();
  }
}

public class LogicFSM : FSM
{
  public LogicFSM() : base()
  {
  }

  public void Add(GameState state)
  {
    m_states.Add((int)state.ID, state);
  }

  public GameState GetState(GameStateType key)
  {
    return (GameState)GetState((int)key);
  }

  public void SetCurrentState(GameStateType stateKey)
  {
    State state = m_states[(int)stateKey];
    if (state != null)
    {
      SetCurrentState(state);
    }
  }
}

public class LogicController : MonoBehaviour
{
  public bool Debug = false;
  public bool Paused = false;
  public int Level = 1;

  public LogicFSM logicFSM = null;

  void Start()
  {
    logicFSM = new LogicFSM();

    logicFSM.Add(new GameState_Menu (this));
    logicFSM.Add(new GameState_Cutscene (this));
    logicFSM.Add(new GameState_Rest (this));
    logicFSM.Add(new GameState_Battle (this));
    logicFSM.Add(new GameState_BossBattle (this));

    logicFSM.SetCurrentState(GameStateType.Menu);
  }


  void Update()
  {
    if (logicFSM != null)
    {
      logicFSM.Update();
    }
  }

  void FixedUpdate()
  {
    if (logicFSM != null)
    {
      logicFSM.FixedUpdate();
    }
  }
}
