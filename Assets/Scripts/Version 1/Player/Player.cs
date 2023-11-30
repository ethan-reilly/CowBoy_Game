using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *  Weapon type 0: Revolver
 *  Weapon type 1: Rifle
 */
public class Player : MonoBehaviour
{
    public static Player Instance;
    public int weaponType;
    private int health = 100;
    public TextMeshProUGUI hpDisplay;

    [SerializeField] GameObject revolver, rifle;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);
        SelectWeapon();
    }

    private void Update()
    {
        if(health <= 0)
        {
            GameManager.Instance.updateGameState(GameManager.GameState.Lose);
            Destroy(gameObject);
        }

        if (hpDisplay != null)
        {
            hpDisplay.SetText("HEALTH: " + health);
        }

    }

    public void TakeDamage(int x)
    {
        health -= x;
    }

    public void SelectWeapon()
    {
        int wpn = GameManager.Instance.GetWeaponType();
        switch (wpn)
        {
            case 0:
                weaponType = wpn;
                rifle.SetActive(false);
                break;
            case 1:
                weaponType = wpn;
                revolver.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(wpn), wpn, null);
        }
    }
}
 

