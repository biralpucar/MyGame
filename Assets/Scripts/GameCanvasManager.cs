using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasManager : MonoSingleton<GameCanvasManager>
{

    [SerializeField] TextMeshProUGUI totalCount;
    [SerializeField] TextMeshProUGUI currentCount;
    [SerializeField] TextMeshProUGUI collectedCountTxt;
    [SerializeField] int collectedCount;
    [SerializeField] GameObject nextPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject pausePanel;

    private void Update()
    {
        currentCount.text = ObstacleManager.instance.GetCurrentCount().ToString();
        totalCount.text = ObstacleManager.instance.GetTotalCount().ToString() + "/ ";
        collectedCountTxt.text = collectedCount.ToString();

        GamePaused();
    }

    public int IncreaseCollected() { return collectedCount++; }

    public void GetNextPanel()
    {
        gamePanel.SetActive(false);
        nextPanel.SetActive(true);
    }

    public void GamePaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            nextPanel.SetActive(false);
            gamePanel.SetActive(false);
            pausePanel.SetActive(true);
        }
    }

    public void BackToGame()
    {
        nextPanel.SetActive(false);
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void GetQuit()
    {
        GameManager.instance.QuitGame();
    }
}
