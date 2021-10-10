using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPose : MonoBehaviour
{
    public Movement[] poses = new Movement[0];
    public SpriteRenderer renderer;

    
    private void Start()
    {
        ChoregraphieManager.TempoEvent.AddListener(ChangePos);
    }

    private void ChangePos()
    {
        renderer.sprite = poses[Random.Range(0, poses.Length)].sprite;
    }
}
