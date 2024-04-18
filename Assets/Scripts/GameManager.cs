using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //esto no es necesario
    //[SerializeField] GameObject defeatScreen;
    //[SerializeField] GameObject victoryScreen;
    Ball ball;
    public int lives;

    private void Start()
    {
        ball = Ball.Instance;
    }

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Exit();
        if (Input.GetKeyUp(KeyCode.R)) ResetGame();

        //acá dejo un print que me contaba los ladrillos
        //print(transform.childCount);
    }

    public void LoseHealth()
    {
        lives -= 1;
        if (lives <= 0) LoadGameOverScreen(); //acá hice que esto llame a un método privado
        else ResetLevel();
    }

    private void LoadGameOverScreen()
    {
        //esta es la carga de la game over screen
        SceneManager.LoadScene(2);
    }

    public void ResetLevel()
    {
        ball.ResetBall();
        FindObjectOfType<Player>().ResetPlayer();
    }

    public void ResetGame()
    {
        //esto también lo cambié para que los botones
        //de las defeat y victory scenes funcionen bien
        SceneManager.LoadScene(0);
    }

    public void ActivateAllChildren()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    public void CheckLevelCompleted()
    {
        if (transform.childCount <= 1)
        {
            LoadVictoryScreen();
        }
    }

    //public void DefeatScreen()
    //{
    //    if (defeatScreen == null) return;
    //    defeatScreen.SetActive(true);
    //}

    public void LoadVictoryScreen()
    {
        Time.timeScale = 0f;

        //accá esto lo comento porque lo manejamos de otra forma

        //if (victoryScreen == null) return;
        //victoryScreen.SetActive(true);

        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
