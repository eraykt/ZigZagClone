using UnityEngine;
using UnityEngine.UI;

namespace ZigZagClone.Ui
{
    public class InGameUI : MonoBehaviour
    {
        public Text clickText;
        public Text coinText;
        public Text diamondText;

        public void SetClick(int score)
        {
            clickText.text = score.ToString();
        }
        public void SetCoin(int score)
        {
            coinText.text = score.ToString();
        }
        public void SetDiamond(int score)
        {
            diamondText.text = score.ToString();
        }
    }
}