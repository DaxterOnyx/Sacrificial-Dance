using System;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float pitchOutFire = 1f;
    public float pitchInFire = 1.4f;
    public float pitchTimeVariation = 1f;
    public AnimationCurve pitchVariation = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private static float previousTime = 0f;

    public static float Time
    {
        get
        {
            return _audioSource.time;
        }
    }


    public static float Speed => _audioSource.pitch;

    private static AudioSource _audioSource;

    private float timeEnterFire = -100f;
    private bool _inFire = false;

    internal static int index = 0;
    public AudioClip[] clips;
    public int[] thresholds;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        DeplacementManager.InFire.AddListener(GoInFire);
        DeplacementManager.OutFire.AddListener(GoOutFire);
        _audioSource.clip = clips[index];
        _audioSource.Play();
    }

    private void GoInFire()
    {
        Debug.Log("Infire");
        timeEnterFire = Time;
        _inFire = true;
    }

    private void GoOutFire()
    {
        Debug.Log("OutFire");
        timeEnterFire = Time;
        _inFire = false;
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            if (ScoreManager.MyScore > thresholds[index]) index++;
            _audioSource.clip = clips[index];
            _audioSource.Play();
        }
        
        var deltaTime = 0f;
        float time = Time;
        if (_inFire)
        {
            deltaTime = (time - timeEnterFire) / pitchTimeVariation;
        }
        else
        {
            deltaTime = 1 - ((time - timeEnterFire) / pitchTimeVariation);
        }

        if (deltaTime > 1)
        {
            deltaTime = 1;
        }
        if (deltaTime < 0)
        {
            deltaTime = 0;
            timeEnterFire = -100;
        }

        var speed = pitchOutFire +
                    (pitchInFire - pitchOutFire) * pitchVariation.Evaluate(deltaTime);

        _audioSource.pitch = speed;
    }
}