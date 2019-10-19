using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndiTimer : MonoBehaviour
{
    public float time;
    private float fAmount;
    private float step;

    private bool isActive = false;
    InteractController.endWaterTrapTimer visov;

    void Start()
    {
        transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().fillAmount = 0f;
        fAmount = 0;
        step = 1 / (time * 50);
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            fAmount += step;
            transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().fillAmount = fAmount;
            if (fAmount >= 1.0f)
            {
                visov();
            }
        }
    }

    private void OnEnable()
    {
        isActive = true;
        fAmount = 0;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    public void SetTimer( InteractController.endWaterTrapTimer calling, float time1 )
    {
        time = time1;
        visov = calling;
    }

}
