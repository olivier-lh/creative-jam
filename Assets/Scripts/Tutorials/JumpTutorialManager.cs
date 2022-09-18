using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTutorialManager : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator.SetBool("IsFlashing", true);
    }

    public void StopFlashing()
    {
        animator.SetBool("IsFlashing", false);
    }

}
