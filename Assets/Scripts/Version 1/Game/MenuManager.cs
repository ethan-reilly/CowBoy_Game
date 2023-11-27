using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponSelectPanel, attackButton;

    [SerializeField]
    private TextMeshProUGUI _stateText;

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
        weaponSelectPanel.SetActive(state == GameManager.GameState.MainMenu);

    }

    private void Start()
    {
        
    }


}
