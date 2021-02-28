using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

  public Vector2 ScreenSize;

    // line(2520,0), (1920, -2)
    // this is used to keep the ship in frame no matter the screen height

    // Update is called once per frame
    void Update()
    {
      // debug
      ScreenSize = new Vector2(
        Camera.main.pixelHeight,
        Camera.main.pixelWidth
      );

      transform.position = new Vector3(0,
      (0.0033333f * Camera.main.pixelHeight) - 8.4f,
      transform.position.z);
    }
}
