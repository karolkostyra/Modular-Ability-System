using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityDefinition))]
public class AbilityDefinitionEditor : Editor
{
    private AbilityDefinition ability;

    private void OnEnable()
    {
        ability = (AbilityDefinition)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "actions");

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);

        DrawActionsList();

        if (GUILayout.Button("Add Action"))
        {
            ShowAddMenu();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawActionsList()
    {
        for (int i = 0; i < ability.Actions.Count; i++)
        {
            var action = ability.Actions[i];

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();

            GUILayout.Label(action.GetDisplayName(), EditorStyles.boldLabel);

            if (GUILayout.Button("↑", GUILayout.Width(25)) && i > 0)
            {
                Undo.RecordObject(ability, "Move Action Up");
                (ability.Actions[i - 1], ability.Actions[i]) = (ability.Actions[i], ability.Actions[i - 1]);
                EditorUtility.SetDirty(ability);
                break;
            }

            if (GUILayout.Button("↓", GUILayout.Width(25)) && i < ability.Actions.Count - 1)
            {
                Undo.RecordObject(ability, "Move Action Down");
                (ability.Actions[i + 1], ability.Actions[i]) = (ability.Actions[i], ability.Actions[i + 1]);
                EditorUtility.SetDirty(ability);
                break;
            }

            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                Undo.RecordObject(ability, "Remove Action");
                ability.Actions.RemoveAt(i);
                EditorUtility.SetDirty(ability);
                break;
            }

            EditorGUILayout.EndHorizontal();

            DrawActionFields(action);

            EditorGUILayout.EndVertical();
        }
    }

    private void DrawActionFields(AbilityAction action)
    {
        var so = new SerializedObject(ability);
        var actionsProp = so.FindProperty("actions");

        EditorGUILayout.PropertyField(actionsProp.GetArrayElementAtIndex(ability.Actions.IndexOf(action)), true);

        so.ApplyModifiedProperties();
    }

    private void ShowAddMenu()
    {
        var types = AbilityActionDatabase.GetAllActions();
        var menu = new GenericMenu();

        foreach (var type in types)
        {
            menu.AddItem(new GUIContent(type.Name), false, () =>
            {
                AddAction(type);
            });
        }

        menu.ShowAsContext();
    }

    private void AddAction(Type type)
    {
        var action = (AbilityAction)Activator.CreateInstance(type);

        Undo.RecordObject(ability, "Add Action");

        ability.Actions.Add(action);

        EditorUtility.SetDirty(ability);
    }
}