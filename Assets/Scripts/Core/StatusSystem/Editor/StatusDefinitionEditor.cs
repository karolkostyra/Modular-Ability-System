using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatusDefinition), true)]
public class StatusDefinitionEditor : Editor
{
    private StatusDefinition status;

    private void OnEnable()
    {
        status = (StatusDefinition)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "conditions");

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);

        DrawActionsList();

        if (GUILayout.Button("Add Condition"))
        {
            ShowAddMenu();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawActionsList()
    {
        for (int i = 0; i < status.Conditions.Count; i++)
        {
            var condition = status.Conditions[i];

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();

            GUILayout.Label(condition.GetDisplayName(), EditorStyles.boldLabel);

            if (GUILayout.Button("↑", GUILayout.Width(25)) && i > 0)
            {
                Undo.RecordObject(status, "Move Condition Up");
                (status.Conditions[i - 1], status.Conditions[i]) = (status.Conditions[i], status.Conditions[i - 1]);
                EditorUtility.SetDirty(status);
                break;
            }

            if (GUILayout.Button("↓", GUILayout.Width(25)) && i < status.Conditions.Count - 1)
            {
                Undo.RecordObject(status, "Move Condition Down");
                (status.Conditions[i + 1], status.Conditions[i]) = (status.Conditions[i], status.Conditions[i + 1]);
                EditorUtility.SetDirty(status);
                break;
            }

            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                Undo.RecordObject(status, "Remove Condition");
                status.Conditions.RemoveAt(i);
                EditorUtility.SetDirty(status);
                break;
            }

            EditorGUILayout.EndHorizontal();

            DrawActionFields(condition);

            EditorGUILayout.EndVertical();
        }
    }

    private void DrawActionFields(StatusCondition condition)
    {
        var so = new SerializedObject(status);
        var conditionsProp = so.FindProperty("conditions");

        EditorGUILayout.PropertyField(conditionsProp.GetArrayElementAtIndex(status.Conditions.IndexOf(condition)), true);

        so.ApplyModifiedProperties();
    }

    private void ShowAddMenu()
    {
        var types = StatusConditionDatabase.GetAllConditions();
        var menu = new GenericMenu();

        foreach (var type in types)
        {
            menu.AddItem(new GUIContent(type.Name), false, () =>
            {
                AddCondition(type);
            });
        }

        menu.ShowAsContext();
    }

    private void AddCondition(Type type)
    {
        var action = (StatusCondition)Activator.CreateInstance(type);

        Undo.RecordObject(status, "Add Condition");

        status.Conditions.Add(action);

        EditorUtility.SetDirty(status);
    }
}