using kechigames.Singleton;
using ZigZagClone.Actions;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameStarted { get; private set; }

    private void Update()
    {
        OnGameStarting();
    }

    private void OnGameStarting()
    {
        if (!PlayerInputs.LeftClick) return;
        if (IsGameStarted) return;
        
        IsGameStarted = true;
        UiActions.Instance.HandleHomeUi?.Invoke(false);
    }
}