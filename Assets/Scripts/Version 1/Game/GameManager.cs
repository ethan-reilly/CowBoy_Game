using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // Weapon 0 = revolver Weapon 1 = rifle
    public int WeaponType = 0;

    // Once all enemies in list are killed, player victory zone activates
    public bool enemiesDefeated = false;

    // Is rifle locked?
    private bool rifleUnlocked = false;

    // Default player pos
    private Vector3 initialPos = new Vector3(0, .318f, 1.4f);

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
           // Debug.Log("Game Manager not null");
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
        Destroy(FindObjectOfType<Player>().gameObject);
        //Destroy(CompareTag("MainCamera"));
        LevelManager.Instance.LoadScene("MainMenu");
        //LevelManager.Instance.LoadScene("Lose");
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

    public bool GetEnemiesDefeated()
    {
        return enemiesDefeated;
    }


    public void NextLevel()
    {
        levelNum++;
        if (levelNum == 1)
          levelNum++;
        enemiesDefeated = false;

        if (FindObjectOfType<Player>() != null)
            FindObjectOfType<Player>().deactivateArrowUI();

        LevelManager.Instance.LoadScene(levelNum);

        if (FindObjectOfType<Player>() != null)
            FindObjectOfType<Player>().gameObject.transform.position = initialPos;
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
            enemiesDefeated = true;
            FindObjectOfType<Player>().activateArrowUI();
        }

    }

    public bool GetRifleUnlocked()
    {
        return rifleUnlocked;
    }

    public void UnlockRifle()
    {
        rifleUnlocked = true;
    }


    public enum GameState
{
    MainMenu,
    Game,
    Victory,
    Lose
}
}