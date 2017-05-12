using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public float speed;

    void Update()
    {

        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);


        transform.LookAt(target);


    }
}
