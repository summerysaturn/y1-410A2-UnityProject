using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace IntroCutscene
{

  // enum of the keyframes in the animation
  public enum Keyframe
  {
    Initial = 0,
    Pan = 1,
    Launch = 2,
    Catchup = 3,
    Exit = 4
  }

  // inheritance precursor to the different keyframes
  // ---------------------------------------------------------------------------
  public class CutsceneState : State
  {
    public Keyframe ID { get { return _id; } }

    protected IntroController _cutscene = null;
    protected Keyframe _id;

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

  // first keyframe
  // ---------------------------------------------------------------------------
  public class Cutscene_Initial : CutsceneState
  {

    public float Duration { get; set; } = 2f;
    private float _time = 0.0f;
    private GameObject _titleText;
    private BackgroundController _starBC;
    private SpriteRenderer _titleRenderer;

    public Cutscene_Initial(IntroController cutscene) : base(cutscene)
    {
      _id = Keyframe.Initial;
      _starBC = cutscene.starBC;
    }

    public override void Enter()
    {
      Debug.Log("ic :: entering Cutscene_Initial");
      base.Enter();

      _time = Time.deltaTime;

      _starBC.globalSpeed = 0f;

      _titleText = GameObject.Find("title-text");
      _titleRenderer = _titleText.GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
      base.Update();

      _time += Time.deltaTime;

      if (_time > Duration * 1.5f)
      {
        _cutscene.cutsceneFSM.SetCurrentState(Keyframe.Pan);
        Object.Destroy(_titleText);
      }
      else
      {
        _titleRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1 - _time / Duration);
      }
    }
  }

  // second keyframe
  // ---------------------------------------------------------------------------
  public class Cutscene_Pan : CutsceneState
  {

    public float speed { get; set; } = 0.05f;
    public float keyframeLength { get; set; } = 3f;
    private float _progress;
    private float _time = 0.0f;
    private GameObject _mainMenu;
    private BackgroundController _starBC;
    private Vector3 _targetPos = new Vector3(0, -10, -5);
    private Vector3 _tempTarget;
    private Vector3 _deltaV3;

    public Cutscene_Pan(IntroController cutscene) : base(cutscene)
    {
      _id = Keyframe.Pan;
      _starBC = cutscene.starBC;
      _mainMenu = cutscene.mainMenu;
    }

    public override void Enter()
    {
      base.Enter();

      _starBC.globalSpeed = 0.5f;
      _time = Time.deltaTime;
    }

    public override void Update()
    {
      base.Update();

      _time += Time.deltaTime;
      _progress = _time / keyframeLength;

      if (Vector3.Distance(_mainMenu.transform.position, _targetPos) < 0.01f)
      {
        _cutscene.cutsceneFSM.SetCurrentState(Keyframe.Launch);
      }
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();

      _tempTarget = new Vector3(
        _mainMenu.transform.position.x,
        EaseInOutSine(0, _targetPos.y, _progress),
        _mainMenu.transform.position.z
      );
      _deltaV3 = _mainMenu.transform.position - _tempTarget;

      _mainMenu.transform.position = _tempTarget;

      _starBC.globalSpeed = _deltaV3.y * 7.5f;
    }

    public override void Exit()
    {
      base.Exit();

      _mainMenu.transform.position = _targetPos;
      _starBC.globalSpeed = 0f;
    }

    static float EaseInOutSine(float start, float end, float value)
    {
      end -= start;
      return -end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + start;
    }
  }

  // third keyframe
  // ---------------------------------------------------------------------------
  public class Cutscene_Launch : CutsceneState
  {

    private GameObject _mainMenu;
    private BackgroundController _starBC;
    private GameObject _fakePlayer;
    public float speed { get; set; } = 20f;


    public Cutscene_Launch(IntroController cutscene) : base(cutscene)
    {
      _id = Keyframe.Launch;
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
        _cutscene.cutsceneFSM.SetCurrentState(Keyframe.Catchup);
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

  // fourth keyframe
  // ---------------------------------------------------------------------------
  public class Cutscene_Catchup : CutsceneState
  {

    private GameObject _mainMenu;
    private BackgroundController _starBC;
    private GameObject _fakePlayer;

    public float keyframeLength { get; set; } = 5f;
    public Vector3 targetShipPos { get; set; } = new Vector3(0,-6.5f,0);
    private float _time;
    private float _progress;


    public Cutscene_Catchup(IntroController cutscene) : base(cutscene)
    {
      _id = Keyframe.Catchup;
      _starBC = cutscene.starBC;
      _mainMenu = cutscene.mainMenu;
      _fakePlayer = cutscene.fakePlayer;
    }

    public override void Enter()
    {
      _time = Time.deltaTime;
      base.Enter();
    }

    public override void Exit()
    {
      base.Exit();
      _starBC.globalSpeed = 1f;
      Object.Destroy(_mainMenu);
    }

    public override void Update()
    {
      _time += Time.deltaTime;
      _progress = _time / keyframeLength;
      base.Update();

      if (_progress > 1f)
      {
        _cutscene.cutsceneFSM.SetCurrentState(Keyframe.Exit);
      }
    }

    public override void FixedUpdate()
    {
      base.FixedUpdate();
      _starBC.globalSpeed = _CalcSpeed(_progress, 15);
      _fakePlayer.transform.position = new Vector3(0,_CalcShipPos(_progress,30),0) + targetShipPos;
      _mainMenu.transform.position -= new Vector3(0,1,0);
    }

    private float _CalcSpeed (float x, int scale)
    {
      return -scale * Mathf.Pow(x, 2) + (scale+1 * x);
    }

    private float _CalcShipPos (float x, int scale)
    {
      return -scale * Mathf.Pow(x,0.75f) + scale;
    }
  }

  // fifth keyframe
  // ---------------------------------------------------------------------------
  public class Cutscene_Exit : CutsceneState
  {

    private GameObject _fakePlayer;
    private GameObject _player;

    public Cutscene_Exit(IntroController cutscene) : base(cutscene)
    {
      _id = Keyframe.Exit;
      _fakePlayer = cutscene.fakePlayer;
      _player = cutscene.player;
    }

    public override void Enter()
    {
      base.Enter();

      _fakePlayer.SetActive(false);
      _player.SetActive(true);

      // TODO: signal back to the LogicController that the cutscene is done
    }
  }

  // fsm controller derived from LogicController.cs
  // this inheritance happens to make sure that the FSM type and State type
  // are properly changed into CutsceneFSM and CutsceneState types; they're
  // not necessarily inherited properly.
  public class CutsceneFSM : FSM
  {
    public CutsceneFSM() : base()
    {
    }

    public void Add(CutsceneState state)
    {
      m_states.Add((int)state.ID, state);
    }

    public CutsceneState GetState(Keyframe key)
    {
      return (CutsceneState)GetState((int)key);
    }

    public void SetCurrentState(Keyframe stateKey)
    {
      State state = m_states[(int)stateKey];
      if (state != null)
      {
        SetCurrentState(state);
      }
    }
  }

  // instance of the intro cutscene controller itself
  public class IntroController : MonoBehaviour
  {
    public CutsceneFSM cutsceneFSM = null;

    public Dictionary<string, Transform> Objects;

    public BackgroundController starBC;
    public GameObject mainMenu;
    public GameObject fakePlayer;
    public GameObject player;

    void Start()
    {
      cutsceneFSM = new CutsceneFSM();

      cutsceneFSM.Add(new Cutscene_Initial(this));
      cutsceneFSM.Add(new Cutscene_Pan(this));
      cutsceneFSM.Add(new Cutscene_Launch(this));
      cutsceneFSM.Add(new Cutscene_Catchup(this));
      cutsceneFSM.Add(new Cutscene_Exit(this));

      cutsceneFSM.SetCurrentState(Keyframe.Initial);

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

      if (player == null)
      {
        player = GameObject.Find("Player");
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
