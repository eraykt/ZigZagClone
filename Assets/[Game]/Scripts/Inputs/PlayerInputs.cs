using UnityEngine;
using UnityEngine.EventSystems;

public static class PlayerInputs
{
    public static bool LeftClick => Input.GetMouseButtonDown(0);

    public static bool ChangeDirection => !EventSystem.current.IsPointerOverGameObject(0) && LeftClick;
}