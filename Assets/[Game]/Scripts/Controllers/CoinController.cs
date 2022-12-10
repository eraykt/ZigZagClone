using UnityEngine;

namespace ZigZagClone.Controllers
{
    public class CoinController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ScoreManager.Instance.SetScore("coin", 10);
                Destroy(gameObject);
                //TODO: Add coin sound
            }
        }
    }
}