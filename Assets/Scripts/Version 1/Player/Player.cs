using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private int weaponType;
    private int health = 100;
    public TextMeshProUGUI hpDisplay;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if (hpDisplay != null)
        {
            hpDisplay.SetText("HEALTH: " + health);
        }

        Debug.Log("Weapontype = " + weaponType);

    }

    public void TakeDamage(int x)
    {
        health -= x;
    }

    public void SelectWeapon()
    {
        string wpn = GameManager.instance.GetWeaponType();
        switch (wpn)
        {
            case "revolver":
                weaponType = 1;
                break;
            case "rifle":
                weaponType = 2;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(wpn), wpn, null);
        }
    }
}
 

