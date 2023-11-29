using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

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

    public void PickWeapon(string weapon)
    {
        GameManager.instance.SetWeaponType(weapon);
    }

    private void Start()
    {
        
    }


}
