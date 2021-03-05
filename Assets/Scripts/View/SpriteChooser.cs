﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChooser : MonoBehaviour
{

    public Sprite[] values;

    // Start is called before the first frame update
    void Start()
    {
      GetComponent<SpriteRenderer>().sprite = values[
        Random.Range(0, values.Length - 1)
      ];
    }
}
