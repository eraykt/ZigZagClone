using kechigames.Singleton;
using ZigZagClone.Ui;

namespace ZigZagClone.Actions
{
    public class UiActions : Singleton<UiActions>
    {
        public System.Action<bool> HandleHomeUi;
        public HomeUi homeUi;

        public System.Action<bool> HandleInGameUi;
        public InGameUI inGameUi;

        public System.Action<string, int> SetScoreHandler;

        public System.Action<bool> HandleWinGameUi;
        public WinGameUI winGameUi;

        private void OnEnable()
        {
            HandleHomeUi += HomeUiHandler;
            HandleInGameUi += InGameUiHandler;
            SetScoreHandler += SetScoreToUi;
            HandleWinGameUi += WinGameUiHandler;
        }


        private void OnDisable()
        {
            HandleHomeUi -= HomeUiHandler;
            HandleInGameUi -= InGameUiHandler;
            SetScoreHandler -= SetScoreToUi;
            HandleWinGameUi -= WinGameUiHandler;
        }

        private void SetScoreToUi(string scoreType, int score)
        {
            switch (scoreType)
            {
                case "click":
                    inGameUi.SetClick(score);
                    break;

                case "coin":
                    inGameUi.SetCoin(score);
                    break;

                case "diamond":
                    inGameUi.SetDiamond(score);
                    break;
            }
        }

        private void WinGameUiHandler(bool active)
        {
            winGameUi.gameObject.SetActive(active);
        }

        private void InGameUiHandler(bool active)
        {
            inGameUi.gameObject.SetActive(active);
        }

        private void HomeUiHandler(bool active)
        {
            homeUi.gameObject.SetActive(active);
        }
    }
}