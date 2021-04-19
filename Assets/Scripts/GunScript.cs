using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GunAnim(bool bulletHere)
    {
        animator.SetBool("isShooting", bulletHere);
    } 
    
}
