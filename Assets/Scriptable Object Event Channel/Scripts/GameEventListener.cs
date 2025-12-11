using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response; // ড্র্যাগ-এন্ড-ড্রপ এর জন্য

    private void OnEnable() => Event.RegisterListener(this);
    private void OnDisable() => Event.UnregisterListener(this);

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}