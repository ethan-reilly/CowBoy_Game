using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int WeaponType = 0;
    public bool playerInVictoryZone = false;
    // Trigger in Player Victory scene

    public List<Enemy> enemies = new List<Enemy>();

    public int levelNum;

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

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Game:
                NextLevel();
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
        UpdateGameState(GameState.MainMenu);
        levelNum = 0;
        LevelManager.Instance.LoadScene("MainMenu");
    }

    public void SetWeaponType(int wpn)
    {
        WeaponType = wpn;
    }
    
    public int GetWeaponType()
    {
        return WeaponType;
    }


    private void NextLevel()
    {
        levelNum++;
        //if (levelNum == 1)
          //  levelNum++;
        
        LevelManager.Instance.LoadScene(levelNum);
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    internal void KillEnemy(Enemy enemy)
    {
        Debug.Log("Enemy killed");
        enemies.Remove(enemy);

        Debug.Log(enemies.Count);

        if (enemies.Count <= 0)
        {
            NextLevel();            
        }

    }

    public enum GameState
{
    MainMenu,
    Game,
    Victory,
    Lose
}
}