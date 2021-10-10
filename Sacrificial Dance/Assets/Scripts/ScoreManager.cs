using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int Score
    {
        get => score;
        set
        {
            score = value;
            if (score <= gameOverScore)
            {
                //TODO GAME OVER   
                Debug.Log("FAIL !!!!!!!!!!!!!!!!!!!!!!!!");
            }
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

    private static ScoreManager _score;

    private void Start()
    {
        _score = this;
    }


    public static void Excellent() => _score.score += _score.excellent;
    public static void Good() => _score.score += _score.good;
    public static void Ok() => _score.score += _score.ok;
    public static void Fail() => _score.score += _score.fail;
    public static void BadInput() => _score.score += _score.badInput;
    public static void Collision() => _score.score += _score.collision;
}