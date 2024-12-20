using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
/// Decorate levels with obstacles, cover, etc
/// Possibly add coins and skill tree
/// Enemies spawned through script?
/// Save to file
/// Make clear new level has been loaded
/// Don't destroy player on defeat, disable input and make invisible, deleting messes with bullets
/// Make list of levels, randomly pick one out and remove from list
/// Boss?
/// 
/// </summary>
public class Player : MonoBehaviour
{
    public static Player Instance;
    public int weaponType;
    private int health = 100;
    private TextMeshProUGUI hpDisplay;

    public Camera mainCamera;

    // Arrow UI
    private Canvas arrowUICanvas;
    private Image arrowUIImage;

    // Coin UI
    private Canvas coinUICanvas;
    private Image coinUIImage;


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


        /// Setting up Arrow Canvas
        GameObject tempObj = GameObject.Find("Arrow - Toward End");

        if (tempObj != null)
        {
            arrowUICanvas = tempObj.GetComponent<Canvas>();

            if (arrowUICanvas != null)
            {
                arrowUIImage = arrowUICanvas.GetComponentInChildren<Image>();
                //Debug.Log("Image found");
                deactivateArrowUI();
            }
            else
            {
                //Debug.Log("Image not found");
                throw new Exception("arrowUICanvas not found");
            }
        }
        else
        {
            Debug.Log("Canvas not found");
        }


        /// Setting up Coin Canvas
        tempObj = GameObject.Find("CoinCollect - Canvas");

        if (tempObj != null)
        {
            coinUICanvas = tempObj.GetComponent<Canvas>();

            if (coinUICanvas != null)
            {
                coinUIImage = coinUICanvas.GetComponentInChildren<Image>();
                deactivateCoinUI();
            }
            else
            {
                throw new Exception("arrowUICanvas not found");
            }
        }
        else
        {
            Debug.Log("Canvas not found");
        }



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


        if (arrowUICanvas.isActiveAndEnabled)
        {
            //Debug.Log("Arrow UI active");
            arrowUIImage.transform.rotation = Quaternion.LookRotation(new Vector3(0, -90, 0));

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

    public void activateArrowUI()
    {
        arrowUICanvas.gameObject.SetActive(true);
    }

    public void deactivateArrowUI()
    {
        arrowUICanvas.gameObject.SetActive(false);
    }

    public void activateCoinUI()
    {

        coinUICanvas.gameObject.SetActive(true);
        StartCoroutine(MyCoroutine());
    }

    public void deactivateCoinUI()
    {
        coinUICanvas.gameObject.SetActive(false);
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(2);
        deactivateCoinUI();
    }

}
 

