using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateDoor : MonoBehaviour
{
    private Animator doorAnimator;

    private bool housePrefabInstantiated;

    private DrawShape script;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();

        script = FindObjectOfType<DrawShape>();

        housePrefabInstantiated = script.housePrefabInstantiated;
    }

    void Update()
    {
        if (script.animate && housePrefabInstantiated)
        {
            if (housePrefabInstantiated)
            {
                doorAnimator.SetBool("isAnimating", true);
                script.animate = true;
            }
        }
        else
        {
            doorAnimator.SetBool("isAnimating", false);
            script.animate = false;
        }

        doorAnimator.speed = script.currentAnimationSpeed;
    }
}
