using System.Collections;
using UnityEngine;
using ZigZagClone.Level;

namespace ZigZagClone.Controllers
{
    public class CubeController : MonoBehaviour
    {
        private Rigidbody rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
        }

        protected float speed;

        protected virtual void OnCollisionExit(Collision other)
        {
            if (GameManager.Instance.IsGameEnded) return;

            if (other.gameObject.CompareTag("Player"))
            {
                speed = other.gameObject.GetComponent<PlayerController>().CurrentSpeed / 5f;
                Invoke(nameof(DropCube), 0.5f - speed);
            }
        }

        private void DropCube()
        {
            rig.useGravity = true;
            rig.isKinematic = false;
            // rig.AddForce(Vector3.down * 100f);
            StartCoroutine(RecycleCube());
        }

        private IEnumerator RecycleCube()
        {
            yield return new WaitForSeconds(1f - speed / 2f);
            LevelCreator.Instance.RecycleCube(this, rig);
            yield return null;
        }
    }
}