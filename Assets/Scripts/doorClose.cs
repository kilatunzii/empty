using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorClose : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            anim.SetBool("Near", false);
        }
    }
}
