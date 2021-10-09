using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float pitchOutFire = 1f;
    public float pitchInFire = 1.4f;
    public float pitchTimeVariation = 1f;
    public AnimationCurve pitchVariation = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public static float Speed = 1;
    
    private AudioSource _audioSource;
    private float timeInFire = 0f;
    private bool _inFire = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        DeplacementManager.InFire.AddListener(GoInFire);
        DeplacementManager.OutFire.AddListener(GoOutFire);
    }
    
    private void GoInFire()
    {
        Debug.Log("Infire");
        _inFire = true;
    }
    
    private void GoOutFire()
    {
        Debug.Log("OutFire");
        _inFire = false;
    }

    private void Update()
    {
        if (_inFire)
        {
            timeInFire += Time.deltaTime;
            if (timeInFire > pitchTimeVariation) timeInFire = pitchTimeVariation;
        }
        else
        {
            timeInFire -= Time.deltaTime;
            if (timeInFire < 0) timeInFire = 0;
        }

        Speed = pitchOutFire +
                       (pitchInFire - pitchOutFire) * pitchVariation.Evaluate(timeInFire / pitchTimeVariation);

        _audioSource.pitch = Speed;
    }
}