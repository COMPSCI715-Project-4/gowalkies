using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingLevel2Cat : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void walk()
    {
        animator.SetBool("isRunning", true); 
    }
}
