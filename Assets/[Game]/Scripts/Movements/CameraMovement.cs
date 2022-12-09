using UnityEngine;

namespace ZigZagClone.Movements
{
    public class CameraMovement : MonoBehaviour
    {
        public Transform target;

        public Vector3 offset;

        public float smoothSpeed = 3f;

        private void Start()
        {
            offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            SmoothFollow();
        }

        public void SmoothFollow()
        {
            Vector3 targetPos = target.position + offset;
            Vector3 smoothFollow = Vector3.Lerp(transform.position,
                targetPos, smoothSpeed * Time.deltaTime);

            transform.position = smoothFollow;
        }
    }
}