using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameEvent gameEvent = (GameEvent)target;

        GUILayout.Space(20);
        GUILayout.Label("🔌 Live Connections:", EditorStyles.boldLabel);

        if (!Application.isPlaying)
        {
            GUILayout.Label("Info updates during Gameplay only.", EditorStyles.helpBox);
            return;
        }

        var inspectorListeners = gameEvent.InspectorListeners;
        if (inspectorListeners.Count > 0)
        {
            GUILayout.Label($"[Inspector Wired: {inspectorListeners.Count}]", EditorStyles.miniBoldLabel);
            GUI.enabled = false;
            foreach (var listener in inspectorListeners)
            {
                if (listener != null)
                    EditorGUILayout.ObjectField(listener.gameObject.name, listener, typeof(MonoBehaviour), true);
            }
            GUI.enabled = true;
        }

        GUILayout.Space(5);

        var codeListeners = gameEvent.CodeListeners;
        if (codeListeners.Count > 0)
        {
            GUILayout.Label($"[Code Wired: {codeListeners.Count}]", EditorStyles.miniBoldLabel);
            GUI.enabled = false;
            foreach (var action in codeListeners)
            {
                var target = action.Target as MonoBehaviour;
                if (target != null)
                {
                    EditorGUILayout.ObjectField(target.gameObject.name + " (Code)", target, typeof(MonoBehaviour), true);
                }
                else
                {
                    EditorGUILayout.LabelField("Non-MonoBehaviour Listener", action.Target.ToString());
                }
            }
            GUI.enabled = true;
        }

        if (inspectorListeners.Count == 0 && codeListeners.Count == 0)
        {
            GUILayout.Label("No active listeners.", EditorStyles.miniLabel);
        }

        GUILayout.Space(20);
        if (GUILayout.Button("Raise Event (Test)"))
        {
            gameEvent.Raise();
        }
    }
}