﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal")  * speed, Input.GetAxisRaw("Vertical") * speed));
    }
}
