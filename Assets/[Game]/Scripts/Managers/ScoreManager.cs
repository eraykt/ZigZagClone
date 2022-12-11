using kechigames.Singleton;
using UnityEngine;
using ZigZagClone.Actions;

public class ScoreManager : Singleton<ScoreManager>
{
    public int CurrentClickPoint { get; private set; }
    public int CurrentCoin { get; private set; }
    public int CurrentDiamond { get; private set; }

    public int TotalClickPoint { get; private set; }
    public int TotalCoin { get; private set; }
    public int TotalDiamond { get; private set; }


    public void SetScore(string scoreType, int score)
    {
        switch (scoreType)
        {
            case "click":
                CurrentClickPoint += score;
                UiActions.Instance.SetScoreHandler(scoreType, CurrentClickPoint);
                break;

            case "coin":
                CurrentCoin += score;
                UiActions.Instance.SetScoreHandler(scoreType, CurrentCoin);
                break;

            case "diamond":
                CurrentDiamond += score;
                UiActions.Instance.SetScoreHandler(scoreType, CurrentDiamond);
                break;
            default:
                Debug.Log("Score type is not defined");
                return;
        }
    }

    public void ResetCurrentScore()
    {
        CurrentClickPoint = CurrentCoin = CurrentDiamond = 0;
        UiActions.Instance.SetScoreHandler("click", CurrentClickPoint);
        UiActions.Instance.SetScoreHandler("coin", CurrentCoin);
        UiActions.Instance.SetScoreHandler("diamond", CurrentDiamond);
    }
}