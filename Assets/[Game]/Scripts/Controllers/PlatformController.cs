using UnityEngine;

namespace ZigZagClone.Controllers
{
    public class PlatformController : CubeController
    {
        [SerializeField] private bool isFinishPlatform;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && isFinishPlatform)
                Invoke(nameof(WinGame), 0.5f);
        }

        private void WinGame()
        {
            GameManager.Instance.OnLevelEnded(true);
        }
    }
}