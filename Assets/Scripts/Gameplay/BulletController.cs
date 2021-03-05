using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
  // is in units per second
  public float speed = 10.0f;
  public int lifeSpan = 5;
  private float _startTime;


  // Move Object at the end of the frame
  void LateUpdate()
  {
    // Move the object by a speed every second (not every frame!)
    transform.position += new Vector3(
      0,
      speed * Time.deltaTime,
      0
    );

    if (Time.time > _startTime + lifeSpan)
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    _startTime = Time.time;
  }
}
