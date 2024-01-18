using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    public void PlayFootstepSound(int foot)
    {
        player.PlayFootstepSound(foot);
    }
}
