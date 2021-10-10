using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ChoregraphieManager : MonoBehaviour
{
    [Header("MOVE MANAGEMENT")] public GameObject CallPrefab;
    public Movement[] Moves = new Movement[0];

    private GameObject nextMove;
    private KeyCode nextInput;

    public GameObject ExcellentText;
    public GameObject GoodText;
    public GameObject OkText;
    public GameObject BadText;
    public GameObject FailText;

    internal static UnityEvent TempoEvent = new UnityEvent();


    enum InputType
    {
        Fail,
        Ok,
        Good,
        Excellent
    }

    private InputType inputType = InputType.Fail;

    private bool input = false;

    private void Start()
    {
        PoseManager.Posing += PoseManagerOnPosing;
    }

    private void PoseManagerOnPosing(KeyCode keycode)
    {
        GameObject text;
        input = true;
        if (keycode == nextInput)
        {
            switch (inputType)
            {
                case InputType.Fail:
                    text = Instantiate(FailText);
                    ScoreManager.Fail();
                    break;
                case InputType.Ok:
                    text = Instantiate(OkText);
                    ScoreManager.Ok();
                    break;
                case InputType.Good:
                    text = Instantiate(GoodText);
                    ScoreManager.Good();
                    break;
                case InputType.Excellent:
                    text = Instantiate(ExcellentText);
                    ScoreManager.Excellent();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            text = Instantiate(BadText);
            ScoreManager.BadInput();
        }

        Destroy(text, 1);
    }

    public void NewMove()
    {
        //Setup next move
        nextMove = Instantiate(CallPrefab, transform);
        Movement move;
        do
        {
            var random = Random.Range(0, Moves.Length);
            move = Moves[random];
        } while (move.key == nextInput);

        nextInput = move.key;

        nextMove.GetComponent<SpriteRenderer>().sprite = move.callSprite;
    }

    public void StartOk()
    {
        inputType = InputType.Ok;
    }

    public void StartGood()
    {
        inputType = InputType.Good;
    }

    public void StartExcellent()
    {
        inputType = InputType.Excellent;
    }

    public void Tempo()
    {
        TempoEvent?.Invoke();
        Destroy(nextMove);
    }

    public void StopExcellent()
    {
        inputType = InputType.Good;
    }

    public void StopGood()
    {
        inputType = InputType.Ok;
    }

    public void StopOk()
    {
        inputType = InputType.Fail;

        if (!input)
            ScoreManager.Fail();
        input = false;
    }
}