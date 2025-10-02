using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float camera_speed;

    private Vector3 offset;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
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