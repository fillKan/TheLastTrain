using InGame.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public sealed class StateVisualizationAdapter : MonoBehaviour
{
    [ContextMenu("VIEW_POLICY_MENU")]
    public void VIEW_POLICY_MENU()
    {
        Debug.Log("PolicyCount : " + PolicySystem.Instance.GetAccumulatePolicy.Count);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(StateVisualizationAdapter))]
public class StateEditor : Editor
{
    private GameEvent gameEvent = null;
    public override void OnInspectorGUI()
    {
        gameEvent = GameEvent.Instance;
        Debug.Assert(gameEvent, "NullReferenceException");

        EditorGUILayout.LabelField("All of Game State");
        EditorGUILayout.Space(2);

        gameEvent.eventEffect.eventTable.ForEach(e =>
        {
            EditorGUILayout.Toggle(e.name, e.IsEventOn);
        });
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        gameEvent.switchCondition.switchTable.ForEach(e =>
        {
            EditorGUILayout.Toggle(e.name, e.IsOn);
        });

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();


    }


}
#endif