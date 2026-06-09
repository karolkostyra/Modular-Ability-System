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

        var lifetimeProp = serializedObject.FindProperty("lifetimeType");

        EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);

        if ((StatusLifetimeType)lifetimeProp.enumValueIndex == StatusLifetimeType.Conditional)
        {
            DrawActionsList();

            if (GUILayout.Button("Add Condition"))
            {
                ShowAddMenu();
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Conditions are only used for Conditional lifetime type.", MessageType.Info);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawActionsList()
    {
        var conditionsProp = serializedObject.FindProperty("conditions");

        for (int i = 0; i < conditionsProp.arraySize; i++)
        {
            var element = conditionsProp.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();

            string label = element.managedReferenceValue != null ? element.managedReferenceValue.GetType().Name : "Null";

            GUILayout.Label(label, EditorStyles.boldLabel);

            if (GUILayout.Button("↑", GUILayout.Width(25)) && i > 0)
            {
                conditionsProp.MoveArrayElement(i, i - 1);
                break;
            }

            if (GUILayout.Button("↓", GUILayout.Width(25)) && i < conditionsProp.arraySize - 1)
            {
                conditionsProp.MoveArrayElement(i, i + 1);
                break;
            }

            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                conditionsProp.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(element, true);

            EditorGUILayout.EndVertical();
        }
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
        serializedObject.Update();

        var conditionsProp = serializedObject.FindProperty("conditions");

        int index = conditionsProp.arraySize;
        conditionsProp.InsertArrayElementAtIndex(index);

        var newElement = conditionsProp.GetArrayElementAtIndex(index);

        newElement.managedReferenceValue = Activator.CreateInstance(type);

        serializedObject.ApplyModifiedProperties();
    }
}