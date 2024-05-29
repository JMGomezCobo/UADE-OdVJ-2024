using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject defeatScreen;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Boll;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject LightProbe;
    BallController _ballController;
    public int lives = 3;
    public TMP_Text livesText;
    

    private void Start()
    {
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
        Time.timeScale = 1f;
        _ballController = BallController.Instance;
    }
    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    private void OnEnable()
    {
        CustomUpdateManager.Instance.SubscribeToUpdate(UpdateUI);
    }

    private void UpdateUI()
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
        _ballController.ResetBall();
        FindObjectOfType<PaddleController>().ResetPlayer();
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

    }
    public void Activate()
    {
        Player.SetActive(true);
        Boll.SetActive(true);
        UI.SetActive(true);
        LightProbe.SetActive(true);
    }

    public void ActivateAllChildren()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }
    public void DeactivateAllChildren()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
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
        if (allObjectsInactive && lives > 0) VictoryScreen();
    }
    public void DefeatScreen()
    {
        Time.timeScale = 0f;
        if (defeatScreen == null) return;
        defeatScreen.SetActive(true);
        victoryScreen.SetActive(false);
        Deactivate();
        DeactivateAllChildren();
    }
    public void VictoryScreen()
    {
        Time.timeScale = 0f;
        if (victoryScreen == null) return;
        victoryScreen.SetActive(true);
        Deactivate();
    }
    public void Deactivate()
    {
        Player.SetActive(false);
        Boll.SetActive(false);
        UI.SetActive(false);
        LightProbe.SetActive(false);
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
