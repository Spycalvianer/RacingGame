using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBuilding : MonoBehaviour
{
    public Animator anim;
    public AnimationClip animationName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            anim.Play(animationName.name);
        }
    }
}
