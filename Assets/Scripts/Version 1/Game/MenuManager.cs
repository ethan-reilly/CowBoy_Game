using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    public Button rifleButton;

    [SerializeField]
    public Sprite lockedRifleSprite;

    [SerializeField]
    public Sprite unlockedRifleSprite;
    
    private void Awake()
    {
        GameManager.OnGameChangeState += GameManager_OnGameChangeState;
        

    }

    private void OnDestroy()
    {
        GameManager.OnGameChangeState -= GameManager_OnGameChangeState;
    }

    private void GameManager_OnGameChangeState(GameManager.GameState state)
    {

    }

    public void PickWeapon(int weapon)
    {
        GameManager.Instance.SetWeaponType(weapon);

        GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
    }

    public void Update()
    {
        if (!GameManager.Instance.GetRifleUnlocked())
        {
            //Debug.Log("Rifle unlocked");
            rifleButton.GetComponent<Image>().sprite = lockedRifleSprite;
        }
        else
        {
            rifleButton.GetComponent<Image>().sprite = unlockedRifleSprite;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {

            GameManager.Instance.UnlockRifle();
        }
    }
    
    // Change button in code to Unlock Rifle


    private void Start()
    {
        
    }


}
