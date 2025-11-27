using UnityEngine;

public class PlayerInput : IMovementInput
{
    public Vector2 GetMovementDir()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        return new Vector2(x, y);
    }
}
