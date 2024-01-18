using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoSingleton<ObstacleManager>
{
    [SerializeField] List<Obstacle> obstacles;
    [SerializeField] Obstacle obstaclePrefab;
    [SerializeField] int obstacleCount;
    [SerializeField] int currentCount;
    private float xPos, yPos, zPos;

    protected override void Awake()
    {
        base.Awake();
        obstacles = new List<Obstacle>();
    }
    private void Start()
    {
        CreateObstacle();
    }

    void CreateObstacle()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            xPos = Random.Range(-9f, 9f);
            yPos = Random.Range(0.5f, 1f);
            zPos = Random.Range(-9f, 9f);
            Vector3 spawnPoses = new Vector3(xPos, yPos, zPos);
            Obstacle obstacle = Instantiate(obstaclePrefab, spawnPoses, Quaternion.identity);
            obstacles.Add(obstacle);

        }
        currentCount = obstacleCount;
    }

    public int GetCurrentCount()
    {
        return currentCount;
    }

    public int GetTotalCount()
    {
        return obstacleCount;
    }

    public int GetRemainingObstacle()
    {
        return currentCount--;
    }

    public int CheckObstacleCount()
    {
        return obstacles.Count;
    }

    public void RemoveList(Obstacle obstacle)
    {
        obstacles.Remove(obstacle);
    }
}
