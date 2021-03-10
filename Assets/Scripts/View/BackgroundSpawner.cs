using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
  public GameObject prefab;
  public float timeout = 1.0f;
  public Vector3 offset;
  private GameObject child;
  private LogicController Logic;


  void Start()
  {
    // Get logic controller from heirarchy
    Logic = GameObject.Find("Foreground").GetComponent(typeof(LogicController)) as LogicController;

    StartCoroutine(SpawnChild());
  }

  IEnumerator SpawnChild()
  {
    while (true) {
      if (!Logic.Paused)
      {
        child = Instantiate(prefab, offset, Quaternion.identity);
        child.transform.parent = transform;
        child.GetComponent<BackgroundMover>().Logic = Logic;
      } else {
        yield return new WaitForSeconds(0.1f);
        continue;
      }
      yield return new WaitForSeconds(timeout);
    }
  }
}
