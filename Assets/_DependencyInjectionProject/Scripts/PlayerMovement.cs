using System;
using Reflex.Attributes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private IMovementInput _movementInput;

    [Inject]
    public void Construct(IMovementInput movementInput)
    {
        _movementInput = movementInput;
    }

    private void Update()
    {
        Vector2 dir = _movementInput.GetMovementDir();
        transform.Translate(new Vector3(dir.x, 0, dir.y) * 5f * Time.deltaTime);
    }
}
