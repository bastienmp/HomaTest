using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(StoreItemSettings))]
public class StoreItemEditor : Editor
{
    private StoreEditorWindowSettings storeSettings = null;
    private SerializedProperty _colliderSettings;
    private SerializedProperty _AnimatorSettings;

    private void OnEnable()
    {
        _colliderSettings = serializedObject.FindProperty("ColliderName");
        _AnimatorSettings = serializedObject.FindProperty("AnimatorName");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        storeSettings = (StoreEditorWindowSettings)AssetDatabase.LoadAssetAtPath("Assets/HomaGamesStore/StoreEditorWindowSettings.asset", typeof(StoreEditorWindowSettings));

        int colliderSelected = 0;
        string[] colliderOptions = new string[storeSettings.Colliders.Count];
        for (int i = 0; i < storeSettings.Colliders.Count; i++)
        {
            colliderOptions[i] = storeSettings.Colliders[i].Name;
        }
        colliderSelected = EditorGUILayout.Popup("Collider", colliderSelected, colliderOptions);
        _colliderSettings.stringValue = colliderOptions[colliderSelected];

        int animatorSelected = 0;
        string[] animatorOptions = new string[storeSettings.Animators.Count];
        for (int i = 0; i < storeSettings.Animators.Count; i++)
        {
            animatorOptions[i] = storeSettings.Animators[i].Name;
            if (storeSettings.Animators[i].Name == _AnimatorSettings.stringValue)
                animatorSelected = i;
        }
        animatorSelected = EditorGUILayout.Popup("Animator", animatorSelected, animatorOptions);
        _AnimatorSettings.stringValue = animatorOptions[animatorSelected];

        serializedObject.ApplyModifiedProperties();
    }
}