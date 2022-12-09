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

            Debug.Log("You have completed the level!");

            yield return null;
        }
    }
}