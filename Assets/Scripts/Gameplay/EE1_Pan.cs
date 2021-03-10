using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EE1_Pan : MonoBehaviour
{

  void Start()
  {
    if (Random.Range(0, 99) <= 89)
    {
      Destroy(gameObject);
    }
  }

  void FixedUpdate()
  {
    transform.position += new Vector3(
      -Time.deltaTime,
      Mathf.Sin(Time.time * 5) * Time.deltaTime * 0.2f,
      0
    );
  }
}
