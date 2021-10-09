using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChoregraphieManager : MonoBehaviour
{
    [Header("MOVE MANAGEMENT")] public GameObject MovePrefab;
    public Movement[] Moves = new Movement[0];
    private Vector2 direction;
    private float timeBeforeNextMove = 2;

    private GameObject nextMove;
    private KeyCode nextInput;
    private KeyCode previousInput;

    [Header("TEMPO Punch")] public float Tempo = 2.4f;
    public float TempoTolerance = 0.1f;
    public float TempoPunchStrength = 1f;
    public float TempoPunchDuration = 0.1f;
    private bool TempoPunched = false;

    [Header("BPS Punch")] public float BPS = 2;
    public float BPSPunchStrength = 0.1f;
    public float BPSPunchDuration = 0.1f;
    private float BPSTime;

    [Header("Other")] 
    public GameObject circle;

    public SpriteRenderer ambiance;
    private bool validate = false;
    public AudioSource music;

    private void Start()
    {
        NewMove();
        PoseManager._poseManager.Posing += PoseManagerOnPosing;
        music.Play();
    }

    private void PoseManagerOnPosing(KeyCode keycode)
    {
        if ((nextInput == keycode && timeBeforeNextMove <= TempoTolerance) ||
            (previousInput == keycode && timeBeforeNextMove >= Tempo - TempoTolerance))
        {
            //GOOD MOVE
            Validate();
        }
        else
        {
            Unvalidate();
        }
    }

    private void Unvalidate()
    {
        Debug.Log("BAD  MOVE " + timeBeforeNextMove);
        Color ambianceColor = Color.red;
        ambianceColor.a = ambiance.color.a;
        ambiance.color = ambianceColor;
    }

    private void Validate()
    {
        validate = true;
        Debug.Log("GOOD MOVE");
        Color ambianceColor = Color.green;
        ambianceColor.a = ambiance.color.a;
        ambiance.color = ambianceColor;
    }

    /// <summary>
    /// On beat of tempo
    /// </summary>
    private void NewMove()
    {
        //Check if move was be done
        if (!validate) Unvalidate();
        validate = false;

        //Setup next move
        nextMove = Instantiate(MovePrefab, transform.position + time2pos(timeBeforeNextMove), Quaternion.identity,
            transform);
        Movement move;
        do
        {
            var random = Random.Range(0, Moves.Length);
            move = Moves[random];
        } while (move.key == nextInput);

        previousInput = nextInput;
        nextInput = move.key;

        nextMove.GetComponent<SpriteRenderer>().sprite = move.sprite;
        direction = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        //BPS
        BPSTime -= Time.deltaTime;
        if (BPSTime <= 0)
        {
            circle.transform.DOPunchPosition(Vector3.down * BPSPunchStrength, BPSPunchDuration, 0, 0);
            BPSTime += BPS;
        }

        //TEMPO
        timeBeforeNextMove -= Time.deltaTime;
        //nextMove.transform.localPosition = time2pos(timeBeforeNextMove);

        if (timeBeforeNextMove <= TempoPunchDuration / 8f && !TempoPunched)
        {
            TempoPunched = true;
            circle.transform.DOPunchPosition(TempoPunchStrength * Vector3.down, TempoPunchDuration, 0, 0);
        }

        if (timeBeforeNextMove <= 0)
        {
            TempoPunched = false;
            Destroy(nextMove);
            timeBeforeNextMove += Tempo;
            NewMove();
        }
    }

    private Vector3 time2pos(float time)
    {
        return new Vector3(direction.x * (1f - time / Tempo) * 5, direction.y * (1 - time / Tempo) * 5, 0);
    }
}