using UnityEngine;

public static class PlayerInputs
{
    public static bool LeftClick => Input.GetMouseButtonDown(0);

    public static bool ChangeDirection => LeftClick;
}