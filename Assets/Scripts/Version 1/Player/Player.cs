using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *  Weapon type 0: Revolver
 *  Weapon type 1: Rifle
 */

/// <summary>
/// Player class
/// 
/// Weapon Type: 0 - Revolver, 1 - Rifle
/// Shoot with left mouse button, shoot function located in projectile script
/// 
/// @TODO Need a way for level swap to put player in correct position
/// Need to make main camera attach to player in new levels
/// Possibly make GameManager create player when 1st level loaded
/// Lock rifle behind a requirement
/// Animations, sounds etc
/// GameManager auto moves to next level based on enemies killed, maybe change to enemies killed and player in victory zone
/// Decorate levels with obstacles, cover, etc
/// </summary>
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
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
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

        if (health <= 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
            Destroy(gameObject);
        }
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

    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
}
 

