using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
  enum bg
  {
    Stars,
    Nebula
  }

  [Header("- Control Variables")]
  public bool paused;
  public int tileOffset;
  public float globalSpeed = 1.0f;

  [Header("- Top Layer")]
  public float topMult = 3f;
  public GameObject[] topLayer;

  [Header("- Middle Layer")]
  public float middleMult = 2f;
  public GameObject[] middleLayer;

  [Header("- Bottom Layer")]
  public float bottomMult = 1f;
  public GameObject[] bottomLayer;

  [Header("- Nebula Layer")]
  public float nebulaMult = 1.5f;
  public GameObject[] nebulaLayer;

  [Header("- Nebula Controls")]
  public Sprite[] nebulaSprites;

  void Start()
  {
    foreach (var item in nebulaLayer)
    {
      RefreshSprite(item);
    }
  }

  void FixedUpdate()
  {
    foreach (var item in topLayer)
    {
      MoveChild(item, topMult);
      CheckWrap(item, bg.Stars);
    }
    foreach (var item in middleLayer)
    {
      MoveChild(item, middleMult);
      CheckWrap(item, bg.Stars);
    }
    foreach (var item in bottomLayer)
    {
      MoveChild(item, bottomMult);
      CheckWrap(item, bg.Stars);
    }
    foreach (var item in nebulaLayer)
    {
      MoveChild(item, nebulaMult);
      CheckWrap(item, bg.Nebula);
    }
  }

  void MoveChild(GameObject child, float speed)
  {
    child.transform.position += new Vector3(
      0,
      -speed * Time.deltaTime * globalSpeed,
      0
    );
  }

  void CheckWrap(GameObject g, bg type)
  {
    if (g.transform.position.y < -tileOffset)
    {
      // tiled objects loop around (move to y=21), whereas untiled objects
      // are deleted
      // move the transform by the offset
      // this movement is always -y for background, so we can hard-code it.
      g.transform.position = new Vector3(
        g.transform.position.x,
        tileOffset,
        g.transform.position.z
      );
      if (type == bg.Nebula)
      {
        RefreshSprite(g);
      }
    }
  }

  void RefreshSprite(GameObject g)
  {
    g.GetComponent<SpriteRenderer>().sprite = nebulaSprites[
      Random.Range(0, nebulaSprites.Length - 1)
    ];
  }
}
