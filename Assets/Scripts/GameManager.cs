using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{

    public void LastLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenuLevel() { SceneManager.LoadScene(0); }

    public void QuitGame()
    {
        Application.Quit();
    }
}
