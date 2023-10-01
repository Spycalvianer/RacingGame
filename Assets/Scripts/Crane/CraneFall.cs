using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneFall : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            anim.SetTrigger("CraneFall");
        }
    }
}
