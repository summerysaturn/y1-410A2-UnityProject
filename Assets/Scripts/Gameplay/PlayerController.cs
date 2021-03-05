using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  private Transform Ship;
  private Transform ShipMarker;
  private int lastWeaponID;
  private int lastBulletID;

  public int weaponID = 1;   // display weapon
  public int bulletID = 1;   // display sprite
  public float shootSpeed = 5f; // bullets per second

  public bool debug = true; // mouse control debug switch

  [Header("- Movement Handling")]
  //private Vector3 lastMousePosition; mouse support
  private Vector2 lastTouchPosition;
  private Vector2 normalisedTouch;
  private Vector2 screenSize;
  public Vector4 touchDeadzone = new Vector4(
    35,
    10,
    5,
    10
  ); // this is a percentage rect of the touch deadzone. % of screen size.
  public int MoveSpeed = 10;

  [Header("- Weapon Handling")]

  public GameObject bulletPrefab;
  public float lastShoot;
  public Vector3 offset;

  [Header("- Prefabs & Sprites")]

  public Sprite[] WeaponTypes;
  public Sprite[] WeaponTypesUI;
  public Sprite[] BulletTypes;
  public Sprite[] BulletTypesUI; // TODO: add UI bullet indicator

  private Sprite SelectedWeapon;
  private Sprite SelectedWeaponUI;
  private Sprite SelectedBullet;
  private Sprite SelectedBulletUI;

  public GameObject weaponPrefab;

  void Start()
  {

    // Get Transform References from heirarchy
    Ship = transform.GetChild(0);
    ShipMarker = transform.GetChild(1);
    UpdateSprites();
  }

  void UpdateSprites()
  {
    // update sprites
    SelectedWeapon = WeaponTypes[weaponID];
    SelectedWeaponUI = WeaponTypesUI[weaponID];
    SelectedBullet = BulletTypes[bulletID];

    Ship.transform.GetChild(1)
                  .GetComponent<SpriteRenderer>()
                  .sprite = SelectedWeapon;

    GameObject.Find("WeaponOverlayUI")
              .GetComponent<SpriteRenderer>()
              .sprite = SelectedWeaponUI;
  }

  void Update()
  {
    if (lastWeaponID != weaponID || lastBulletID != bulletID)
    {
      UpdateSprites();
    }
    lastWeaponID = weaponID;
    lastBulletID = bulletID;

    if (debug)
    {
      lastTouchPosition = Input.mousePosition;
      MoveMarker();

      if (lastShoot + (1 / shootSpeed) < Time.time)
      {
        lastShoot = Time.time;
        GameObject child = Instantiate(weaponPrefab, Ship.transform.position + offset, Quaternion.identity);
        child.GetComponent<SpriteRenderer>().sprite = SelectedBullet;
      }
    }

    if (Input.touchCount > 0)
    {
      lastTouchPosition = Input.touches[0].position;
      MoveMarker();

      if (lastShoot + (1 / shootSpeed) < Time.time)
      {
        lastShoot = Time.time;
        GameObject child = Instantiate(weaponPrefab, Ship.transform.position + offset, Quaternion.identity);
        child.GetComponent<SpriteRenderer>().sprite = SelectedBullet;
      }
    }
    Move();
  }

  // movement system

  void MoveMarker()
  {
    Ray ray = Camera.main.ScreenPointToRay(ClampTouch(lastTouchPosition));
    ShipMarker.transform.position = ray.GetPoint(10);
  }

  void Move()
  {
    Ship.transform.position = Vector3.Lerp(
      Ship.transform.position,
      ShipMarker.transform.position - new Vector3(0, 0, 1),
      MoveSpeed * Time.deltaTime
    );
  }

  Vector2 ClampTouch(Vector2 xy)
  {
    screenSize = new Vector2(
      Camera.main.pixelWidth,
      Camera.main.pixelHeight
    );

    return new Vector2(
      Mathf.Clamp(
        xy.x,
        0 + screenSize.x * (touchDeadzone.w / 100),
        screenSize.x - screenSize.x * (touchDeadzone.y / 100)
      ),
      Mathf.Clamp(
        xy.y,
        0 + screenSize.y * (touchDeadzone.z / 100),
        screenSize.y - screenSize.y * (touchDeadzone.x / 100)
      )
    );
  }
}
