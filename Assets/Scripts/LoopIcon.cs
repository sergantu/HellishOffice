using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopIcon : MonoBehaviour
{
    private float step;


    void Start()
    {
        step = -2;
    }

    private void FixedUpdate()
    {
        transform.GetChild(0).Rotate(0, 0, step);
    }
}
