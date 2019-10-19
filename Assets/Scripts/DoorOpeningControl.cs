using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningControl : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("DoorIsOpened", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("DoorIsOpened", false);
    }
}
