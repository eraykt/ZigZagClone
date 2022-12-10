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
            this.playerTransform = playerTransform;
            SetSpeed(speed);
            SetDirection(playerTransform.right);
        }

        public void SetDirection(Vector3 newDirection)
        {
            direction = newDirection;
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        public void Move()
        {
            if (!GameManager.Instance.IsGameStarted) return;

            playerTransform.Translate(direction * (speed * Time.deltaTime));
        }
    }
}