using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineCamera : MonoBehaviour
{
    public Transform target;
    public float camera_speed;

    private Vector3 offset;

    void Start()
    {
        GameObject submarine = GameObject.FindGameObjectWithTag("Submarine");

        if (submarine != null)
        {
            target = submarine.transform;
            offset = transform.position - target.position;
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 new_position = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, new_position, camera_speed * Time.fixedDeltaTime);
        }
    }
}