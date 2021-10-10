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
            if (score <= gameOverGap)
            {
                //TODO GAME OVER   
                Debug.Log("FAIL !!!!!!!!!!!!!!!!!!!!!!!!");
            }

            textScore.text = score.ToString();
        }
    }

    private int score = 0;

    [Header("Score")] public TextMeshPro textScore;
    [SerializeField] private int gameOverGap = -25;
    [SerializeField] private int scoreExcellent = 50;
    [SerializeField] private int scoreGood = 30;
    [SerializeField] private int scoreOk = 10;
    [SerializeField] private int scoreEarly = 0;
    [SerializeField] private int scoreLate = 0;
    [SerializeField] private int scoreBadInput = -10;
    [SerializeField] private int scoreCollision = -5;

    [Header("Comment")] public TextMeshPro textComment;
    public string textExcellent = "Excellent";
    public string textGood = "Good";
    public string textOk = "Ok";
    public string textEarly = "Early";
    public string textLate = "Too Late !";
    public string textBadInput = "No !";
    public string textCollision = "Attention";

    private static ScoreManager _score;

    private void Start()
    {
        _score = this;
        Score = 0;
        textComment.text = "";
    }


    public static void Excellent()
    {
        _score.textComment.text = _score.textExcellent;
        _score.Score += _score.scoreExcellent;
    }

    public static void Good()
    {
        _score.textComment.text = _score.textGood;
        _score.Score += _score.scoreGood;
    }

    public static void Ok()
    {
        _score.textComment.text = _score.textOk;
        _score.Score += _score.scoreOk;
    }

    public static void Early()
    {
        _score.textComment.text = _score.textEarly;
        _score.Score += _score.scoreEarly;
    }

    public static void Late()
    {
        _score.textComment.text = _score.textLate;
        _score.Score += _score.scoreLate;
    }

    public static void BadInput()
    {
        _score.textComment.text = _score.textBadInput;
        _score.Score += _score.scoreBadInput;
    }

    public static void Collision()
    {
        _score.textComment.text = _score.textCollision;
        _score.Score += _score.scoreCollision;
    }
}