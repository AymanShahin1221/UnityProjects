using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSkyscraper : MonoBehaviour
{
    private Animator skyScraperAnimator;

    private bool skyscraperPrefabInstantiated;

    private DrawShape script;

    void Start()
    {
        skyScraperAnimator = GetComponent<Animator>();

        script = FindObjectOfType<DrawShape>();

        skyscraperPrefabInstantiated = script.skyscraperPrefabInstantiated;
    }

    void Update()
    {
        if (script.animate && skyscraperPrefabInstantiated)
        {
            if (skyscraperPrefabInstantiated)
            {
                skyScraperAnimator.SetBool("isAnimating", true);
                script.animate = true;
            }
        }
        else
        {
            skyScraperAnimator.SetBool("isAnimating", false);
            script.animate = false;
        }
        skyScraperAnimator.speed = script.currentAnimationSpeed;
    }
}
