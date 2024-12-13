using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UserData))]
public class UDataCustomEditor : Editor
{
    SerializedProperty m_nameProp;
    SerializedProperty m_contentsProp;
    SerializedProperty m_indexProp;
    SerializedProperty m_spriteProp;
    SerializedProperty m_openscoreProp;

    private void OnEnable()
    {
        m_nameProp = serializedObject.FindProperty("m_name");
        m_contentsProp = serializedObject.FindProperty("m_contents");
        m_indexProp = serializedObject.FindProperty("m_index");
        m_spriteProp = serializedObject.FindProperty("m_sprite");
        m_openscoreProp = serializedObject.FindProperty("m_openScore");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(m_nameProp, new GUIContent("�̸�"));

        EditorGUILayout.LabelField(new GUIContent("����"));
        m_contentsProp.stringValue = EditorGUILayout.TextArea(m_contentsProp.stringValue, GUILayout.Height(60));

        EditorGUILayout.PropertyField(m_indexProp, new GUIContent("��Ų ��ȣ"));
        EditorGUILayout.PropertyField(m_spriteProp, new GUIContent("��Ų"));
        EditorGUILayout.PropertyField(m_openscoreProp, new GUIContent("��Ų �ر� ����"));

        serializedObject.ApplyModifiedProperties();
    }
}
