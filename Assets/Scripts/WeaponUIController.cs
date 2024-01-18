using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIController : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] TextMeshProUGUI ammoTxt;
    [SerializeField] Image backGround;

    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        if (weapon.Reloading())
        {
            backGround.fillAmount = (1 / weapon.ReloadTiming()) - 0.45f;
        }
        ammoTxt.text = weapon.GetAmmoCount().ToString();
    }


}
