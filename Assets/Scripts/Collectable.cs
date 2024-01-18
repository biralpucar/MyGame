using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public void Collected()
    {
        GameCanvasManager.instance.IncreaseCollected();
        LevelManager.instance.RemoveCollectables(this);
        Destroy(gameObject);
    }

}
