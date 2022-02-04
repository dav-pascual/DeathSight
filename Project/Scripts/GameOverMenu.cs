using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverMenuUI;
    public float gameOverDelay;
    private float finalScore;
    public Text scoreCount;

    void Start()
    {
        isGameOver = false;
        finalScore = 0f;
        gameOverMenuUI.SetActive(false);
    }

    public void GameOver(float score)
    {
        isGameOver = true;
        finalScore = score;
        Invoke("showUI", gameOverDelay);
    }

    void showUI()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        scoreCount.text = finalScore.ToString();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
