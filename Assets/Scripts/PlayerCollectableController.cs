using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectableController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Collectable col))
        {
            col.Collected();
            //GameCanvasManager.instance.

        }
    }
}
