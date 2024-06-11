using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //[SerializeField] private GameObject credits;
    [SerializeField] private GameObject levelButton;

    //private void Awake()
    //{
    //    credits.SetActive(false);
    //}
    public void QuitGame()
    {
        Application.Quit();
    }
    //public void ToggleCredits()
    //{
    //    credits.SetActive(!credits.active);
    //}
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
