using UnityEngine;
using ZigZagClone.Movements;

namespace ZigZagClone.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float movingSpeed = 2f;

        private PlayerEnums eDirection = PlayerEnums.Right;

        private PlayerMovement playerMover;

        private void Awake()
        {
            playerMover = new PlayerMovement(movingSpeed, transform);
        }

        private void Update()
        {
            DirectionControl();
        }

        private void FixedUpdate()
        {
            playerMover.Move();
        }

        private void DirectionControl()
        {
            if (!PlayerInputs.ChangeDirection) return;

            eDirection = eDirection == PlayerEnums.Right ? PlayerEnums.Forward : PlayerEnums.Right;

            switch (eDirection)
            {
                case PlayerEnums.Right:
                    playerMover.SetDirection(transform.right);
                    break;
                case PlayerEnums.Forward:
                    playerMover.SetDirection(transform.forward);
                    break;
            }
        }
    }
}