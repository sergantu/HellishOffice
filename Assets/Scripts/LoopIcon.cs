using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopIcon : MonoBehaviour
{
    private float step;


    void Start()
    {
        transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
        step = -2;
    }

    private void FixedUpdate()
    {
        transform.GetChild(1).transform.GetChild(0).Rotate(0, 0, step);
    }
}
