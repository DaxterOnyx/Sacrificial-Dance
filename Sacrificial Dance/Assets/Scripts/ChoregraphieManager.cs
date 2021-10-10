using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ChoregraphieManager : MonoBehaviour
{
    [Header("MOVE MANAGEMENT")] public GameObject CallPrefab;
    public Movement[] Moves = new Movement[0];

    private GameObject nextMove;
    private KeyCode nextInput;

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
    KeyCode pastInput;

    private void Start()
    {
        PoseManager.Posing += PoseManagerOnPosing;
    }

    private void PoseManagerOnPosing(KeyCode keycode)
    {
        if (input) return;
        input = true;
        if (keycode == nextInput || keycode == pastInput)
        {
            switch (inputType)
            {
                case InputType.Fail:
                    ScoreManager.Early();
                    break;
                case InputType.Ok:
                    ScoreManager.Ok();
                    break;
                case InputType.Good:
                    ScoreManager.Good();
                    break;
                case InputType.Excellent:
                    ScoreManager.Excellent();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            ScoreManager.BadInput();
        }
    }

    public void NewMove()
    {
        if (nextMove != null)
        {
            Destroy(nextMove);
        }

        //Setup next move
        nextMove = Instantiate(CallPrefab, transform);
        Movement move;
        do
        {
            var random = Random.Range(0, Moves.Length);
            move = Moves[random];
        } while (move.key == nextInput);

        pastInput = nextInput;
        nextInput = move.key;

        nextMove.GetComponent<SpriteRenderer>().sprite = move.callSprite;
    }

    public void StartOk(int count)
    {
        if (count > MusicManager.index) return;
        inputType = InputType.Ok;
    }

    public void StartGood(int count)
    {
        if (count > MusicManager.index) return;
        inputType = InputType.Good;
    }

    public void StartExcellent(int count)
    {
        if (count > MusicManager.index) return;
        inputType = InputType.Excellent;
    }

    public void Tempo(int count)
    {
        if (count > MusicManager.index) return;
        TempoEvent?.Invoke();
        Destroy(nextMove);


        NewMove();
    }

    public void StopExcellent(int count)
    {
        if (count > MusicManager.index) return;
        inputType = InputType.Good;
    }

    public void StopGood(int count)
    {
        if (count > MusicManager.index) return;
        inputType = InputType.Ok;
    }

    public void StopOk(int count)
    {
        if (count > MusicManager.index) return;

        inputType = InputType.Fail;

        if (!input)
        {
            ScoreManager.Late();
        }

        pastInput = KeyCode.F15;
        input = false;
    }
}