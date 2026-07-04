using System;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Aiming,
    BulletInAction,
    LevelComplete,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        OnGameStateChanged?.Invoke(newState);

        switch (CurrentState)
        {
            case GameState.MainMenu:
                Debug.Log("Game Manager: Switched to Main Menu");
                break;
            case GameState.Aiming:
                Debug.Log("Game Manager: Switched to Aiming");
                break;
            case GameState.BulletInAction:
                Debug.Log("Game Manager: Switched to BulletInAction");
                break;
            case GameState.LevelComplete:
                Debug.Log("Game Manager: Switched to LevelComplete");
                break;
            case GameState.GameOver:
                Debug.Log("Game Manager: Switched to GameOver");
                break;
            default:
                break;
        }
    }
}
