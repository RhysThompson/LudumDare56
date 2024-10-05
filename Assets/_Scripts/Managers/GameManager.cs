using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[Serializable]
public enum GameState
{
    Starting = 0,
    Paused = 1,
    Playing = 2,
    Win = 5,
    Lose = 6,
}
/// <summary>
/// Base for a Game Manager. Mainly features: State machine for switching between game states.
/// I'm not sure if this is needed so it's not currently used in the project, but it's here if someone
/// wants to use it.
/// </summary>
public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public static GameState lastGameState = GameState.Starting;
    public string winText;
    public GameState State { get; private set; }
    /// <summary>
    /// Stops all state changes from the first state to any of the listed states. OnBeforeStateChanged will not be called.
    /// </summary>
    [SerializedDictionary("Can't change from this State", "To these States")]
    [SerializeField] private SerializedDictionary<GameState,List<GameState>> ForbidenStateTransitions;
    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState) {
        if (isValidTransition(State,newState)==false) return;

        lastGameState = State;
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            case GameState.Paused:
                break;
            case GameState.Playing:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }
    public bool isValidTransition(GameState oldState, GameState newState)
    {
        bool inForbiddenTable = ForbidenStateTransitions.ContainsKey(oldState) && ForbidenStateTransitions[oldState].Contains(newState);
        bool isRepeat = oldState == newState;
        return !(inForbiddenTable || isRepeat);
    }
    ///automatically resumes game when exiting the pause GameState
    
    private void HandleStarting() {
        // Do some start setup, could be environment, cinematics etc

        // Eventually call ChangeState again with your next state
        ChangeState(GameState.Playing);
    }
    private void HandleWin() {
        ScoreMenu.Instance.Open();
        ScoreMenu.Instance.SetText("You Win!", winText);
        PauseMenuManager.Instance.PauseWithoutMenu();
    }
    private void HandleLose()
    {
        PauseMenuManager.Instance.PauseWithoutMenu();
        ScoreMenu.Instance.Open();
        ScoreMenu.Instance.SetText("You Died", "Try Again?");
        ScoreMenu.Instance.SetColor(new Color(1, 0.5f, 0.5f, 0.5f));

    }
}

