using kechigames.Singleton;
using ZigZagClone.Ui;

namespace ZigZagClone.Actions
{
    public class UiActions : Singleton<UiActions>
    {
        public System.Action<bool> HandleHomeUi;
        public HomeUi homeUi;
        private void OnEnable()
        {
            HandleHomeUi += HomeUiHandler;
        }

        private void OnDisable()
        {
            HandleHomeUi -= HomeUiHandler;
        }

        private void HomeUiHandler(bool active)
        {
            homeUi.gameObject.SetActive(active);
        }
    }
}