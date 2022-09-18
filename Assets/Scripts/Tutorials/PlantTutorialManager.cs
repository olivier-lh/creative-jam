using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTutorialManager : MonoBehaviour
{
    public Animator PlantAnimator;
    public Animator FFAnimator;
    public Animator BlocAnimator;
    [SerializeField] private TextScroll ts;
    private bool FFIsFlashing = false;

    public void Start()
    {
        PlantAnimator.SetBool("IsFlashing", true);
    }

    public void Update()
    {
        switch (ts.currentDisplayingText)
        {
            case 2:
                if (!FFIsFlashing)
                {
                    PlantAnimator.SetBool("IsFlashing", false);
                }
                break;
            case 3:
                if (!FFIsFlashing)
                {
                    FFAnimator.SetBool("IsFlashing", true);
                    FFIsFlashing = true;
                }
                break;
            case 4:
                FFAnimator.SetBool("IsFlashing", false);
                BlocAnimator.SetBool("IsFlashing", true);
                break;
        }

        if (!ts.isActiveAndEnabled)
        {
            BlocAnimator.SetBool("IsFlashing", false); 
        }
    }
}
