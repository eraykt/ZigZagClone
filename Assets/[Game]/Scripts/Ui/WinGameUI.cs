using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ZigZagClone.Ui
{
    public class WinGameUI : MonoBehaviour
    {
        public Text clickText;
        public Text coinText;
        public Text diamondText;

        int clickCount;
        int coinCount;
        int diamondCount;

        public Button continueButton;

        public Animator animator;

        private bool isUiUpdated;

        private Coroutine updateUiCoroutine;

        private void Awake()
        {
            continueButton.onClick.AddListener(NextLevel);
        }

        private void Start()
        {
            ResetText();
        }

        private void Update()
        {
            UpdateUi();
        }

        private void UpdateUi()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("anim")) return;
            if (isUiUpdated) return;

            if (updateUiCoroutine == null)
                updateUiCoroutine = StartCoroutine(ScoreUpdater());

            if (PlayerInputs.LeftClick && updateUiCoroutine != null)
            {
                StopCoroutine(updateUiCoroutine);

                clickText.text = ScoreManager.Instance.CurrentClickPoint.ToString();
                coinText.text = ScoreManager.Instance.CurrentCoin.ToString();
                diamondText.text = ScoreManager.Instance.CurrentDiamond.ToString();
                isUiUpdated = true;
            }
        }

        private IEnumerator ScoreUpdater()
        {
            while (true)
            {
                if (clickCount < ScoreManager.Instance.CurrentClickPoint)
                {
                    clickCount++;
                    clickText.text = clickCount.ToString();
                    yield return new WaitForSeconds(0.2f);
                }
                else break;
            }

            while (true)
            {
                if (coinCount < ScoreManager.Instance.CurrentCoin)
                {
                    coinCount++;
                    coinText.text = coinCount.ToString();
                    yield return new WaitForSeconds(0.2f);
                }
                else if (ScoreManager.Instance.CurrentCoin == 0)
                {
                    coinText.text = coinCount.ToString();
                    break;
                }

                else break;
            }

            while (true)
            {
                if (diamondCount < ScoreManager.Instance.CurrentDiamond)
                {
                    diamondCount++;
                    diamondText.text = diamondCount.ToString();
                    yield return new WaitForSeconds(0.2f);
                }
                else if (ScoreManager.Instance.CurrentDiamond == 0)
                {
                    diamondText.text = diamondCount.ToString();
                    break;
                }

                else
                {
                    StopCoroutine(ScoreUpdater());
                    break;
                }
            }
        }

        private void NextLevel()
        {
            ResetText();
            GameManager.Instance.NextLevel();
        }

        private void ResetText()
        {
            clickText.text = "";
            coinText.text = "";
            diamondText.text = "";

            clickCount = 0;
            coinCount = 0;
            
            isUiUpdated = false;
            updateUiCoroutine = null;
        }
    }
}