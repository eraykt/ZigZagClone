using kechigames.Singleton;
using UnityEngine;
using ZigZagClone.Actions;

public class ScoreManager : Singleton<ScoreManager>
{
    public int ClickPoint { get; private set; }
    public int Coin { get; private set; }
    public int Diamond { get; private set; }


    public void SetScore(string scoreType, int score)
    {
        switch (scoreType)
        {
            case "click":
                ClickPoint += score;
                UiActions.Instance.SetScoreHandler(scoreType, ClickPoint);
                break;

            case "coin":
                Coin += score;
                UiActions.Instance.SetScoreHandler(scoreType, Coin);
                break;

            case "diamond":
                Diamond += score;
                UiActions.Instance.SetScoreHandler(scoreType, Diamond);
                break;
            default:
                Debug.Log("Score type is not defined");
                return;
        }
    }
}