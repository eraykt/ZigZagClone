using UnityEngine;

namespace ZigZagClone.Controllers
{
    public class CubeController : MonoBehaviour
    {
        private Rigidbody rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                rig.useGravity = true;
                rig.isKinematic = false;
            }            
        }
    }
}