using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  // public so we can access this for AI
  public float offset;

  public float shipOffset = 8f;
  public float shipPadding = 1.375f;

  void FixedUpdate()
  {

    // this snippet is useful for debug, as it shows the viewport size.
    // Commented because it's rather slow for every frame
    //ScreenSize = new Vector2(
    //  Camera.main.pixelHeight,
    //  Camera.main.pixelWidth
    //);

    // this gets the current orthographic size of the camera, in vertical units,
    // then subtracts the offset and the padding. These two values are used to /
    // center the ship at the bottom of the frame by an equal amount of pixels /
    // regardless of camera height.
    offset = Camera.main.orthographicSize - shipOffset - shipPadding;

    transform.position = new Vector3(0,
    offset,
    transform.position.z);
  }
}
