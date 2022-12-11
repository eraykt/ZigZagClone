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


        protected override void OnCollisionExit(Collision other)
        {
            base.OnCollisionExit(other);

            if (other.gameObject.CompareTag("Player"))
                Destroy(gameObject, 2f);
        }

        private void WinGame()
        {
            GameManager.Instance.OnLevelEnded(true);
        }
    }
}