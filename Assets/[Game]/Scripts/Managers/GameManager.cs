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
    }

    public void OnLevelEnded(bool hasWon)
    {
        // IsGameStarted = false;
        IsGameEnded = true;
        
        if (hasWon)
        {
            Debug.Log("tebriks");
        }
        
        else
        {
            Debug.Log("maalesef");
        }
    }
}