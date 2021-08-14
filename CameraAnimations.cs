using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimations : MonoBehaviour
{
    private Animator animator;
    public PlayerController playerController;
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (playerController.dashMovement)
        {
            case true:
            animator.SetTrigger("dashCamera");
            animator.ResetTrigger("idleCamera");
            break;

            case false:
            animator.ResetTrigger("dashCamera");
            animator.SetTrigger("idleCamera");
            break;
        }

    }
}
