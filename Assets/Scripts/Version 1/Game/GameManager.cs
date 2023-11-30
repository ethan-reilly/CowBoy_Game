using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Research don't destroy on load
    /// </summary>
    public int WeaponType = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Game Manager not null");
            Destroy(gameObject);
        }
    }


    public static GameManager Instance;


    public GameState State;
    public static event Action<GameState> OnGameChangeState;

    public void updateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Game:
                HandleGame();
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameChangeState?.Invoke(newState);
    }

    private void HandleLose()
    {
        throw new NotImplementedException();
    }

    void Start()
    {
        updateGameState(GameState.MainMenu);    
    }

    public void SetWeaponType(int wpn)
    {
        WeaponType = wpn;
    }
    
    public int GetWeaponType()
    {
        return WeaponType;
    }


    private void HandleGame()
    {
        LevelManager.Instance.LoadScene("SampleScene");
    }

    public enum GameState
{
    MainMenu,
    Game,
    Victory,
    Lose
}
}