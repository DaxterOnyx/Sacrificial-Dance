using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int MyScore { get; private set; }
    private int Score
    {
        get => score;
        set
        {
            score = value;
            MyScore = score;
            if (score <= gameOverScore)
            {
                //TODO GAME OVER   
                Debug.Log("FAIL !!!!!!!!!!!!!!!!!!!!!!!!");
            }

            text.text = score.ToString();
        }
    }

    private int score = 0;
    [SerializeField] private int gameOverScore = -25;
    [SerializeField] private int excellent = 50;
    [SerializeField] private int good = 30;
    [SerializeField] private int ok = 10;
    [SerializeField] private int fail = 0;
    [SerializeField] private int badInput = -10;
    [SerializeField] private int collision = -5;

    private TextMeshPro text;
    
    private static ScoreManager _score;

    private void Start()
    {
        _score = this;
        text = GetComponent<TextMeshPro>();
        Score = 0;
    }


    public static void Excellent() => _score.Score += _score.excellent;
    public static void Good() => _score.Score += _score.good;
    public static void Ok() => _score.Score += _score.ok;
    public static void Fail() => _score.Score += _score.fail;
    public static void BadInput() => _score.Score += _score.badInput;
    public static void Collision() => _score.Score += _score.collision;
}