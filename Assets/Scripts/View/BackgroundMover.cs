using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public bool isTiled;
    public int tileOffset;

    [Header("- Applies If Tiled")]
    // is in units per second
    public float speed = 2.0f;

    public LogicController Logic;

    // Move Object at the end of the frame
    void FixedUpdate()
    {
      if (!Logic.Paused)
      {
        MoveSelf();
      }
    }

    void MoveSelf()
    {
      // Move the object by a speed every second (not every frame!)
      transform.position += new Vector3 (
        0,
        -speed * Time.deltaTime,
        0
      );

      // if the position is below the offset, it's off the screen. therefore we
      // can manipulate the object based on this information.
      if (transform.position.y < -tileOffset) {

        // tiled objects loop around (move to y=21), whereas untiled objects
        // are deleted
        if (isTiled) {

          // move the transform by the offset
          // this movement is always -y for background, so we can hard-code it.
          transform.position = new Vector3(
            transform.position.x,
            tileOffset,
            transform.position.z
          );
        }
        else
        {

          // Kill the GameObject
          Destroy(gameObject);
        }
      }
    }
}
