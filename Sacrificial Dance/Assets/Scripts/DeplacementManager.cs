using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeplacementManager : MonoBehaviour
{
    internal static UnityEvent InFire = new UnityEvent();
    internal static UnityEvent OutFire = new UnityEvent();
    
    public float speed = 1;
    private Camera _camera;
    private Rigidbody2D _rb;

    [Header("In Fire")] 
    public float speedInFire = 0.5f;
    bool inFire = false;
        
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        InFire.AddListener(GoInFire);
        OutFire.AddListener(GoOutFire);
    }

    private void GoInFire()
    {
        inFire = true;
    }

    private void GoOutFire()
    {
        inFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = _camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 diff = (mouse - _rb.position);
        float trueSpeed = inFire ? speedInFire : speed;
        if (diff.magnitude > trueSpeed)
        {
            _rb.MovePosition(_rb.position + diff.normalized * trueSpeed);
        }
        else
        {
            _rb.MovePosition(mouse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            InFire?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            OutFire?.Invoke();
        }
    }
}