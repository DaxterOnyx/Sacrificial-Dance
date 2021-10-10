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

    public TextMeshPro Text;
    public string Excellent = "Excellent";
    public string Good = "Good";
    public string Ok = "Ok";
    public string Fail = "Early";
    public string BadInput = "No !";
    public string Late = "Too Late !";

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
        Text.text = "";
    }

    private void PoseManagerOnPosing(KeyCode keycode)
    {
        input = true;
        if (keycode == nextInput)
        {
            switch (inputType)
            {
                case InputType.Fail:
                    Text.text = Fail;
                    ScoreManager.Fail();
                    break;
                case InputType.Ok:
                    Text.text = Ok;
                    ScoreManager.Ok();
                    break;
                case InputType.Good:
                    Text.text = Good;
                    ScoreManager.Good();
                    break;
                case InputType.Excellent:
                    Text.text = Excellent;
                    ScoreManager.Excellent();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            Text.text = BadInput;
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
        {
            Text.text = Late;
            ScoreManager.Fail();
        }
        input = false;
    }
}