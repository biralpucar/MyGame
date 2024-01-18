using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    public List<Collectable> collectables;

    protected override void Awake()
    {
        base.Awake();
        collectables = new List<Collectable>();
    }

    public void AddCollectables(Collectable collectable)
    {
        collectables.Add(collectable);
    }

    public void RemoveCollectables(Collectable collectable)
    {
        collectables.Remove(collectable);
        CheckNextLevel();
    }

    void CheckNextLevel()
    {
        if (collectables.Count == 0 && ObstacleManager.instance.CheckObstacleCount() == 0)
        {
            GameCanvasManager.instance.GetNextPanel();
        }
    }

    public void OnNextButton()
    {
        GameManager.instance.LastLevel();
    }

    public void OnMainMenuButton()
    {
        GameManager.instance.MainMenuLevel();
    }
}
