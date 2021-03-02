using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

  //public Vector2 ScreenSize;
  public float offset;

  // line(2520,0), (1920, -2)
  // this is used to keep the ship in frame no matter the screen height
  // this seems broken at higher resolution widths (i.e. 1440x2560). more
  // investigation needed. out of scope for this project.

  // Update is called once per frame
  void Update()
  {

    // this snippet is useful for debug, as it shows the viewport size.
    // Commented because it's rather slow for every frame
    //ScreenSize = new Vector2(
    //  Camera.main.pixelHeight,
    //  Camera.main.pixelWidth
    //);

    offset = (0.0033333f * Camera.main.pixelHeight) - 8.4f;

    transform.position = new Vector3(0,
    offset,
    transform.position.z);
  }
}
