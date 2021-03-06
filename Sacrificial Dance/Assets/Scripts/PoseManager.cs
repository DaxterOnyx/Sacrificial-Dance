using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseManager : MonoBehaviour
{
    public Movement[] poses = new Movement[0];
    public SpriteRenderer renderer;

    public delegate void KeyEvent(KeyCode keyCode);
    public static event KeyEvent Posing;
    
    private void Start()
    {
        if (renderer == null)
        {
            renderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        foreach (Movement pose in poses)
        {
            if (Input.GetKeyDown(pose.key))
            {
                renderer.sprite = pose.sprite;
                Posing?.Invoke(pose.key);
                break;
            }
        }
    }
}