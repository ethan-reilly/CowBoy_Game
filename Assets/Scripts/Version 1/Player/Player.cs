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
/// @TODO
/// Possibly make GameManager create player when 1st level loaded
/// Lock rifle behind a requirement
/// Animations, sounds etc
/// GameManager auto moves to next level based on enemies killed, change to enemies killed and player in victory zone
/// Decorate levels with obstacles, cover, etc
/// Possibly add coins and skill tree
/// 
/// </summary>
public class Player : MonoBehaviour
{
    public static Player Instance;
    public int weaponType;
    private int health = 100;
    private TextMeshProUGUI hpDisplay;

    public Camera mainCamera;

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

        //hpDisplay = UI.Instance.getHpDisplay();
    }

    private void Update()
    {
        if (hpDisplay != null)
        {
            hpDisplay.SetText("HEALTH: " + health);

            if (health <= 0)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);

            }
        }
        else
        {
            Debug.Log("HP Display not set yet");
            hpDisplay = UI.Instance.getHpDisplay();
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

    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
    
    
}
 

