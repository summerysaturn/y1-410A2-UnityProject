using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float timeout = 1.0f;
    public Vector3 offset;
    private GameObject child;

    void Start()
    {
      StartCoroutine(SpawnChild());
    }

    IEnumerator SpawnChild()
    {
      while (true) {
        child = Instantiate(prefab, offset, Quaternion.identity);
        child.transform.parent = transform;
        yield return new WaitForSeconds(timeout);
      }
    }
}
