using kechigames.Singleton;
using UnityEngine;
using ZigZagClone.Actions;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameStarted { get; private set; }
    public bool IsGameEnded { get; private set; }

    private void Update()
    {
        OnGameStarting();
    }

    private void OnGameStarting()
    {
        if (!PlayerInputs.LeftClick) return;
        if (IsGameStarted) return;
        if (IsGameEnded) return;

        IsGameStarted = true;

        UiActions.Instance.HandleHomeUi?.Invoke(false);
        UiActions.Instance.HandleInGameUi?.Invoke(true);
    }

    public void OnLevelEnded(bool hasWon)
    {
        // IsGameStarted = false;
        IsGameEnded = true;

        UiActions.Instance.HandleInGameUi?.Invoke(false);

        if (hasWon)
        {
            UiActions.Instance.HandleWinGameUi?.Invoke(true);
        }

        else
        {
            Debug.Log("maalesef");
        }
    }
}