using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using static TransitionTableSO;
using static UnityEditor.EditorGUILayout;

[CustomEditor(typeof(TransitionTableSO))]
public class TransitionTableEditor : Editor
{
    private SerializedProperty transitions;
    private TransitionTableSO table;

    private void OnEnable()
    {
        serializedObject.Update();
        transitions = serializedObject.FindProperty("transitions");
        table = (TransitionTableSO)target;
    }


    public override void OnInspectorGUI()
    {
        DrawList();
        GUILayout.Space(15);
        if (GUILayout.Button("트렌지션 추가"))
        {
            transitions.InsertArrayElementAtIndex(transitions.arraySize);
            table.Transitions.Add(new TransitionItem());
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawList()
    {
        int count = transitions.arraySize;

        for (int i = 0; i < count; i++)
        {
            GUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(transitions.GetArrayElementAtIndex(i), new GUIContent(table.Transitions[i].FromState?.name + " -> " + table.Transitions[i].ToState?.name));

            if (GUILayout.Button("삭제", GUILayout.Width(50)))
            {
                transitions.DeleteArrayElementAtIndex(i);
                table.Transitions.RemoveAt(i);
            }

            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }
    }

}
