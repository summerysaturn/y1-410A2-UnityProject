using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 lastMousePosition;
    private Vector2 lastTouchPosition;

    // Update is called once per frame
    void Update()
      {
        lastMousePosition = Input.mousePosition;

        if (Input.touchCount > 0)
            lastTouchPosition = Input.touches[0].position;
        else
            lastTouchPosition = Vector2.zero;
      }
}
