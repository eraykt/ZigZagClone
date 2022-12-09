using UnityEngine;

namespace ZigZagClone.Movements
{
    public class PlayerMovement
    {
        private Vector3 direction;
        private float speed;
        private readonly Transform playerTransform;

        public PlayerMovement(float speed, Transform playerTransform)
        {
            this.speed = speed;
            this.playerTransform = playerTransform;
        }

        public void SetDirection(Vector3 newDirection)
        {
            this.direction = newDirection;
        }

        public void SetSpeed(float newSpeed)
        {
            this.speed = newSpeed;
        }

        public void Move()
        {
            if (!GameManager.Instance.IsGameStarted) return;

            playerTransform.Translate(direction * (speed * Time.deltaTime));
        }
    }
}