using System.Collections;
using System.Collections.Generic;
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
            direction = playerTransform.right;
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
            playerTransform.Translate(direction * (speed * Time.deltaTime));
        }
    }
}