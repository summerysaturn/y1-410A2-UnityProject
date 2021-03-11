using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace IntroCutscene
{

  public enum AnimationType
  {
    Initial = 0,
    Pan = 1,
    Launch = 2,
    Catchup = 3,
    Exit = 4
  }

  public class CutsceneState : State
  {
    public AnimationType ID { get { return _id; } }

    protected IntroController _cutscene = null;
    protected AnimationType _id;

    public CutsceneState(FSM fsm, IntroController cutscene) : base(fsm)
    {
      _cutscene = cutscene;
    }

    public CutsceneState(IntroController cutscene) : base()
    {
      _cutscene = cutscene;
      m_fsm = _cutscene.cutsceneFSM;
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


  public class Cutscene_Initial : CutsceneState
  {

    public float Duration { get; set; } = 2f;

    private float deltaTime = 0.0f;
    private GameObject _titleText;
    private BackgroundController _starBC;
    private SpriteRenderer _titleRenderer;

    public Cutscene_Initial(IntroController cutscene) : base(cutscene)
    {
      _id = AnimationType.Initial;
      _starBC = cutscene.starBC;
    }

    public override void Enter()
    {
      Debug.Log("ic :: entering Cutscene_Initial");

      deltaTime = Time.deltaTime;
      base.Enter();

      _starBC.globalSpeed = 0f;

      _titleText = GameObject.Find("title-text");
      _titleRenderer = _titleText.GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
      deltaTime += Time.deltaTime;

      base.Update();

      if (deltaTime > Duration * 1.5f)
      {
        _cutscene.cutsceneFSM.SetCurrentState(AnimationType.Pan);
        Object.Destroy(_titleText);
      }
      else
      {
        _titleRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1 - deltaTime / Duration);
      }
    }
  }

  public class Cutscene_Pan : CutsceneState
  {

    public float speed { get; set; } = 0.05f;
    private GameObject _mainMenu;
    private BackgroundController _starBC;
    private Vector3 _targetPos = new Vector3(0, -10, -5);

    public Cutscene_Pan(IntroController cutscene) : base(cutscene)
    {
      _id = AnimationType.Pan;
      _starBC = cutscene.starBC;
      _mainMenu = cutscene.mainMenu;
    }

    public override void Enter()
    {
      base.Enter();

      _starBC.globalSpeed = 0.5f;
    }

    public override void Exit()
    {
      base.Exit();
    }

    public override void Update()
    {
      base.Update();

      if (Vector3.Distance(_mainMenu.transform.position, _targetPos) < 0.01f)
      {
        _mainMenu.transform.position = _targetPos;
        _starBC.globalSpeed = 0f;
        _cutscene.cutsceneFSM.SetCurrentState(AnimationType.Launch);
      }
    }

    public override void FixedUpdate()
    {
      _mainMenu.transform.position = Vector3.MoveTowards(_mainMenu.transform.position, _targetPos, speed);

      base.FixedUpdate();
    }
  }

  public class Cutscene_Launch : CutsceneState
  {

    private GameObject _mainMenu;
    private BackgroundController _starBC;
    private GameObject _fakePlayer;
    public float speed { get; set; } = 20f;


    public Cutscene_Launch(IntroController cutscene) : base(cutscene)
    {
      _id = AnimationType.Launch;
      _starBC = cutscene.starBC;
      _mainMenu = cutscene.mainMenu;
      _fakePlayer = cutscene.fakePlayer;
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

      if (_fakePlayer.transform.position.y > 11)
      {
        _fakePlayer.transform.position = new Vector3(0, 11, 0);
        _cutscene.cutsceneFSM.SetCurrentState(AnimationType.Catchup);
      }
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();

      _fakePlayer.transform.position += new Vector3(
        0,
        Time.deltaTime * speed,
        0
      );
    }
  }


  public class Cutscene_Catchup : CutsceneState
  {

    private GameObject _mainMenu;
    private BackgroundController _starBC;
    private GameObject _fakePlayer;

    public Cutscene_Catchup(IntroController cutscene) : base(cutscene)
    {
      _id = AnimationType.Catchup;
      _starBC = cutscene.starBC;
      _mainMenu = cutscene.mainMenu;
      _fakePlayer = cutscene.fakePlayer;
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

      _cutscene.cutsceneFSM.SetCurrentState(AnimationType.Exit);
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();
    }
  }


  public class Cutscene_Exit : CutsceneState
  {
    public Cutscene_Exit(IntroController cutscene) : base(cutscene)
    {
      _id = AnimationType.Exit;
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

      // signal back to the LogicController that this is done
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();
    }

  }


  public class CutsceneFSM : FSM
  {
    public CutsceneFSM() : base()
    {
    }

    public void Add(CutsceneState state)
    {
      m_states.Add((int)state.ID, state);
    }

    public CutsceneState GetState(AnimationType key)
    {
      return (CutsceneState)GetState((int)key);
    }

    public void SetCurrentState(AnimationType stateKey)
    {
      State state = m_states[(int)stateKey];
      if (state != null)
      {
        SetCurrentState(state);
      }
    }
  }

  public class IntroController : MonoBehaviour
  {
    public CutsceneFSM cutsceneFSM = null;

    public Dictionary<string, Transform> Objects;

    public BackgroundController starBC;
    public GameObject mainMenu;
    public GameObject fakePlayer;

    void Start()
    {
      cutsceneFSM = new CutsceneFSM();

      cutsceneFSM.Add(new Cutscene_Initial(this));
      cutsceneFSM.Add(new Cutscene_Pan(this));
      cutsceneFSM.Add(new Cutscene_Launch(this));
      cutsceneFSM.Add(new Cutscene_Catchup(this));
      cutsceneFSM.Add(new Cutscene_Exit(this));

      cutsceneFSM.SetCurrentState(AnimationType.Initial);

      starBC = GameObject.Find("StarLayer").GetComponent<BackgroundController>();
      mainMenu = GameObject.Find("MainMenu");
      fakePlayer = GameObject.Find("FakePlayer");
    }

    void Update()
    {
      if (cutsceneFSM != null)
      {
        cutsceneFSM.Update();
      }
    }

    void FixedUpdate()
    {
      if (cutsceneFSM != null)
      {
        cutsceneFSM.FixedUpdate();
      }
    }
  }
}
