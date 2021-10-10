using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public int sceneNumber = 1;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        sprite.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        sprite.color = Color.white;
    }

    private void OnMouseDown()
    {
        sprite.color = Color.cyan;
        SceneManager.LoadScene(sceneNumber);
    }
}
