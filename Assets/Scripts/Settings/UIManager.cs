using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI player1Score, player2Score, playTimer, player1ScoreFin, player2ScoreFin, playTimerFin, winText;

    public Image healthP1, healthP2;

    private int player1ScoreValue, player2ScoreValue;

    private float playTime, startTime;

    public GameObject player1TextPanel, player2TextPanel, gameFinishPanel;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameFinishPanel.SetActive(false);
    }

    private void Start()
    {
        player1TextPanel.SetActive(true);
        player2TextPanel.SetActive(true);

        startTime = 0;

        Screen.SetResolution(1920, 1080, true);
        Application.targetFrameRate = 30;
    }

    private void Update()
    {
        if(startTime != 0)
        {
            playTime = Time.time - startTime;

            int minutes = Mathf.FloorToInt(playTime / 60);
            int second = Mathf.FloorToInt(playTime - (minutes * 60));

            string timer = string.Format("{0:0}:{1:00}", minutes, second);

            playTimer.text = timer;
        }
    }

    public void Player1Joined()
    {
        player1TextPanel.SetActive(false);
        player1Score.text = "0";

        startTime = Time.time;
    }

    public void Player2Joined()
    {
        player2TextPanel.SetActive(false);
        player2Score.text = "0";
    }

    public void ScoreUpdate(int score, PlayerInput input)
    {
        if(input.playerIndex == 0)
        {
            player1ScoreValue += score;
            player1Score.text = player1ScoreValue.ToString();
        }
        else
        {
            player2ScoreValue += score;
            player2Score.text = player2ScoreValue.ToString();
        }
    }

    public void HealthUpdate(float curHealth, float totalHealth, PlayerInput input)
    {
        if (input.playerIndex == 0)
        {
            healthP1.fillAmount = curHealth / totalHealth;
        }
        else
        {
            healthP2.fillAmount = curHealth / totalHealth;
        }
    }

    public void GameFinished()
    {
        gameFinishPanel.SetActive(true);
        player1ScoreFin.text = player1ScoreValue.ToString();
        player2ScoreFin.text = player2ScoreValue.ToString();
        if(player1ScoreValue > player2ScoreValue)
        {
            winText.text = "Player 1 Wins";
        }
        else
        {
            winText.text = "Player 2 Wins";
        }

        playTimerFin.text = playTimer.text;
    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
