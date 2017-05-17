using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour {

    public float moveSpeed = 1;
    public float turnSpeed = 5;

    public Transform target;
    public Vector3 offset;
    public Vector3 lookOffset;


    void Update()
    {

        if (target == null)
        {

            target = GameObject.FindWithTag("Player").transform;

            return;

        }


        transform.position = Vector3.Lerp(
            transform.position, 
            target.position + target.right * offset.x + target.up * offset.y + target.forward * offset.z, 
            Time.deltaTime * moveSpeed
            );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation
            ((target.position + lookOffset) - transform.position),
            Time.deltaTime * turnSpeed
            );



    }
}
