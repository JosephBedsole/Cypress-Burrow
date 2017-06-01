using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

    public Animator animator;

    void Start()
    {
        Animator animator = GetComponent<Animator>();
    }

    public void animateTrain()
    {
        animator.SetTrigger("pressE");
    }

}
