using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject InfoPanel;

    public void GetFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuQuitGame()
    {
        Application.Quit();
    }

    public void GetInfoPanel()
    {
        MenuPanel.SetActive(false);
        InfoPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPanel.SetActive(true);
            InfoPanel.SetActive(false);
        }
    }
}
