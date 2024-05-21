using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : ManagedUpdateBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] GameObject victoryScreen;
    Ball ball;
    public int lives = 3;
    public TMP_Text livesText;

    private void Start()
    {
        Time.timeScale = 1f;
        ball = Ball.Instance;
    }
    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public override void UpdateMe()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) LoadMainMenu();
        if (Input.GetKeyUp(KeyCode.R)) ResetGame();
        CheckLevelCompleted();
        UpdateLivesUI();
    }
    public void LoseHealth()
    {
        lives--;
        if (lives <= 0) DefeatScreen();
        else ResetLevel();
    }
    public void ResetLevel()
    {
        ball.ResetBall();
        FindObjectOfType<Player>().ResetPlayer();
    }
    public void ResetGame()
    {
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
        lives += 3;
        ActivateAllChildren();
        ResetLevel();
        ball.ResetBall();
        Time.timeScale = 1f;
        if (lives > 3) lives = 3;

    }
    public void ActivateAllChildren()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    public void CheckLevelCompleted()
    {
        bool allObjectsInactive = true;
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                allObjectsInactive = false;
                break;
            }
        }
        if (allObjectsInactive) VictoryScreen();
    }
    public void DefeatScreen()
    {
        Time.timeScale = 0f;
        if (defeatScreen == null) return;
        defeatScreen.SetActive(true);
    }
    public void VictoryScreen()
    {
        Time.timeScale = 0f;
        if (victoryScreen == null) return;
        victoryScreen.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void UpdateLivesUI()
    {
        if (livesText != null) livesText.text = "Vidas: " + lives.ToString();
    }
    
}
