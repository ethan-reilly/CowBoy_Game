using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game manager is null");

            return _instance;
        }
    }

    public GameState State;
    public static event Action<GameState> OnGameChangeState;

    public void updateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Game:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameChangeState?.Invoke(newState);
    }

        private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        updateGameState(GameState.MainMenu);    
    }


    private void HandleMainMenu()
    {
        throw new NotImplementedException();
    }

    public enum GameState
{
    MainMenu,
    Game,
    Victory,
    Lose
}
}