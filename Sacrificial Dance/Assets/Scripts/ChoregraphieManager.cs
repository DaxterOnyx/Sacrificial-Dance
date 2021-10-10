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
    public GameObject CallLetterPrefab;
    public Movement[] Moves = new Movement[0];

    private GameObject nextMove;
    private GameObject nextLetter;
    private KeyCode nextInput =KeyCode.F15;

    internal static UnityEvent TempoEvent = new UnityEvent();

    public GameObject Circle240Prefab;

    public GameObject Circle120Prefab;

    enum InputType
    {
        Fail,
        Ok,
        Good,
        Excellent
    }

    private InputType inputType = InputType.Fail;

    private bool input = false;
    KeyCode pastInput = KeyCode.F15;

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

        if (nextLetter != null)
        {
            Destroy(nextLetter);
        }

        //Setup next move
        Movement move;
        do
        {
            var random = Random.Range(0, Moves.Length);
            move = Moves[random];
        } while (move.key == nextInput);

        pastInput = nextInput;
        nextInput = move.key;

        nextMove = Instantiate(CallPrefab, transform);
        nextMove.GetComponent<SpriteRenderer>().sprite = move.callSprite;
        if (MusicManager.index == 1)
        {
            nextLetter = Instantiate(CallLetterPrefab, transform);
            nextLetter.GetComponent<SpriteRenderer>().sprite = move.callLetter;
        }
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

        SpawnCircle(MusicManager.index);
    }

    GameObject circle;

    private void SpawnCircle(int count)
    {
        if (circle != null) Destroy(circle);
        if (count < 3)
        {
            //Pase 1 et 2
            circle = Instantiate(Circle240Prefab);
        }
        else
        {
            circle = Instantiate(Circle120Prefab);
        }
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

        if (!input &&  pastInput != KeyCode.F15)
        {
            ScoreManager.Late();
        }

        pastInput = KeyCode.F15;
        input = false;
    }
}