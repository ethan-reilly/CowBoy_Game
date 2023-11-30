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

    public void PickWeapon(int weapon)
    {
        GameManager.instance.SetWeaponType(weapon);

        GameManager.instance.updateGameState(GameManager.GameState.Game);
    }

    private void Start()
    {
        
    }


}
