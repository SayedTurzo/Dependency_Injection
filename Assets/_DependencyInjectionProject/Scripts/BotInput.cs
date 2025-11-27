using UnityEngine;

public class BotInput : IMovementInput
{
    public Vector2 GetMovementDir()
    {
        return new Vector2(1, 0);
    }
}
