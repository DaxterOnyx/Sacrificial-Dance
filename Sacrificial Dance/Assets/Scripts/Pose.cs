using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pose : MonoBehaviour
{
    public SpriteRenderer[] poses = new SpriteRenderer[0];

    private void Start()
    {
        if (poses.Length == 0)
        {
            poses = GetComponentsInChildren<SpriteRenderer>();
        }
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            SetPose(0);
        }
        
        if (Input.GetKey(KeyCode.Z))
        {
            SetPose(1);
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            SetPose(2);
        }
    }

    private void SetPose(int pose)
    {
        foreach (SpriteRenderer spriteRenderer in poses)
        {
            spriteRenderer.gameObject.SetActive(false);
        }
        
        poses[pose].gameObject.SetActive(true);
    }
}
