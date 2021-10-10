using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomPose : MonoBehaviour
{
    public SpriteRenderer renderer;

    private void Start()
    {
        ChoregraphieManager.TempoEvent.AddListener(ChangePos);
    }

    private void ChangePos()
    {
        var movement = ChoregraphieManager.Move;
        if (movement != null)
            renderer.sprite = movement.sprite;
    }
}