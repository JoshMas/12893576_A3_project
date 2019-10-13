using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    private int linesCleared;
    private int level;
    private float speed;
    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Text levelText = null;

    private GameManager gameManager;

    private int topScore;
    private int currentScore;
    private static string TOP_SCORE = "Score";

    // Start is called before the first frame update
    void Start()
    {
        linesCleared = 0;
        level = 0;
        speed = 1.1f;

        gameManager = GetComponent<GameManager>();

        topScore = PlayerPrefs.GetInt(TOP_SCORE);
        currentScore = 0;
        SetScoreBoards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetScoreBoards()
    {
        scoreText.text = "Top Score:\n" + topScore.ToString("D6") + "\nCurrent Score:\n" + currentScore.ToString("D6");
        levelText.text = "Level:\n" + level.ToString("D2");
    }

    public float UpdateScore(int lines)
    {
        if (gameManager.enabled)
        {
            switch (lines)
            {
                case 1:
                    currentScore += 50;
                    break;
                case 2:
                    currentScore += 150;
                    break;
                case 3:
                    currentScore += 400;
                    break;
                case 4:
                    currentScore += 900;
                    break;
                case 20:
                    currentScore += 100000;
                    break;
            }

            linesCleared += lines;
            level = linesCleared / 10;
            speed = 1.1f - (Mathf.Clamp(level, 0.0f, 29.0f) / 29.0f);
            SetScoreBoards();
        }
        
        return speed;
    }

    public void GameOver(string text)
    {
        if (currentScore > topScore)
        {
            topScore = currentScore;
            PlayerPrefs.SetInt(TOP_SCORE, topScore);
        }
        levelText.text = text;
    }
}
