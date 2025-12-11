using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameEvent onPlayerHurtEvent;

    private void OnEnable()
    {
        onPlayerHurtEvent.AddListener(ShakeCamera);
    }

    private void OnDisable()
    {
        onPlayerHurtEvent.RemoveListener(ShakeCamera);
    }

    private void ShakeCamera()
    {
        Debug.Log("Camera: ঝাঁকুনি দিচ্ছে... (Code Based Response)");
    }
}