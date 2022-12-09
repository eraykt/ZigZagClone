using System.Collections;
using UnityEngine;

namespace ZigZagClone.Controllers
{
    public class PlatformController : CubeController
    {
        [SerializeField] private bool isFinishPlatform;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player") && isFinishPlatform)
                StartCoroutine(WinGame());
        }

        private IEnumerator WinGame()
        {
            yield return new WaitForSeconds(0.5f);

            GameManager.Instance.OnLevelEnded(true);
            //TODO: Win ui

            yield return null;
        }
    }
}