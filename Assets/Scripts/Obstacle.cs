using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] Collectable collectablePrefab;

    public void DecreaseHealth(float _health)
    {
        health -= _health;
        if (health < 0)
        {
            Death();
        }
    }

    void Death()
    {

        ObstacleManager.instance.GetRemainingObstacle();
        ObstacleManager.instance.RemoveList(this);
        Collectable collectable = Instantiate(collectablePrefab, transform.position, Quaternion.identity);
        LevelManager.instance.AddCollectables(collectable);
        Destroy(gameObject);
    }


    public float CurrentHealth()
    {
        return health;
    }

}
