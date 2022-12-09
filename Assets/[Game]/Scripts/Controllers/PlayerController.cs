using UnityEngine;
using ZigZagClone.Movements;

namespace ZigZagClone.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        #region Player Movement Variables

        [SerializeField] private float movingSpeed = 2f;
        private PlayerEnums directionEnum = PlayerEnums.Forward;
        private PlayerMovement playerMover;

        #endregion

        #region Ground Check Variables

        private RaycastHit hit;
        [SerializeField] private Transform raycastPosition;

        #endregion

        private void Awake()
        {
            playerMover = new PlayerMovement(movingSpeed, transform);
        }

        private void Update()
        {
            DirectionControl();
            GroundControl();
        }

        private void FixedUpdate()
        {
            playerMover.Move();
        }

        private void GroundControl()
        {
            if (!GameManager.Instance.IsGameStarted) return;
            if (GameManager.Instance.IsGameEnded) return;


            if (Physics.Raycast(raycastPosition.position, -raycastPosition.transform.up, out hit, 1f))
                if (hit.collider.CompareTag("Cube"))
                    return;


            GameManager.Instance.OnLevelEnded(false);
        }


        private void DirectionControl()
        {
            if (!GameManager.Instance.IsGameStarted) return;
            if (GameManager.Instance.IsGameEnded) return;
            if (!PlayerInputs.ChangeDirection) return;

            directionEnum = directionEnum == PlayerEnums.Right ? PlayerEnums.Forward : PlayerEnums.Right;

            switch (directionEnum)
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