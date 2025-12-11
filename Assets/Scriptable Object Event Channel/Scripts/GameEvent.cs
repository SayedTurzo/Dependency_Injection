using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    #region For Inspector

    private List<GameEventListener> listeners = new List<GameEventListener>();
    public List<GameEventListener> InspectorListeners => listeners;

    #endregion
    
    #region For Code
    
    private List<Action> codeListeners = new List<Action>();
    public List<Action> CodeListeners => codeListeners;
    
    #endregion
    
    public void Raise()
    {
        // For Inspector usage
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            if(listeners[i] != null) listeners[i].OnEventRaised();
        }

        // For Code usage
        for (int i = codeListeners.Count - 1; i >= 0; i--)
        {
            codeListeners[i]?.Invoke();
        }
    }

    // --- Inspector Setup ---
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener)) listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener)) listeners.Remove(listener);
    }

    // --- Code Setup ---
    public void AddListener(Action listener)
    {
        if (!codeListeners.Contains(listener)) codeListeners.Add(listener);
    }

    public void RemoveListener(Action listener)
    {
        if (codeListeners.Contains(listener)) codeListeners.Remove(listener);
    }
    
    private void OnDisable()
    {
        listeners.Clear();
        codeListeners.Clear();
    }
}