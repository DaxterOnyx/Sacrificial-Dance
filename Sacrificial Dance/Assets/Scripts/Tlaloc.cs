using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tlaloc : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Time", Mathf.Clamp(((float) ScoreManager.MyScore) / ((float) MusicManager.MaxScore), 0, 0.999f));
    }
}