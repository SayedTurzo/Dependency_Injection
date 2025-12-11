using UnityEngine;
using UnityEngine.Events;

// Creates a menu item to create this asset easily
[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    // The actual "Action" that happens
    public UnityAction OnEventRaised;

    // The function the Sender calls
    public void RaiseEvent()
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
        else
        {
            Debug.LogWarning("Event was raised, but no one was listening!");
        }
    }
}