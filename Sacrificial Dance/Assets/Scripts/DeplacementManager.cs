using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementManager : MonoBehaviour
{
    public float speed = 1;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        var mouse = camera.ScreenToWorldPoint(Input.mousePosition);

        mouse.z = 0;

        Vector3 diff = (mouse - transform.position);
        if (diff.magnitude > speed)
        {
            transform.position += diff.normalized * speed;
        }
        else
        {
            transform.position = mouse;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("colide");
    }
}