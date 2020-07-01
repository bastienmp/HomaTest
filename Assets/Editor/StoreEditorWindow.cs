using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

[Serializable]
public struct ColliderSettings
{
    public string Name;
    public Collider Collider;
}

[Serializable]
public struct AnimatorSettings
{
    public string Name;
    public Animator Animator;
}

public class StoreEditorWindow : EditorWindow
{
    private StoreEditorWindowSettings settings = null;
    private Vector2 scrollPos;

    [MenuItem("Window/Homa Games Store")]
    public static void ShowWindow()
    {
        GetWindow<StoreEditorWindow>("Homa Games Store");
    }

    public void Awake()
    {
        Debug.Log("awake");
        settings = (StoreEditorWindowSettings)AssetDatabase.LoadAssetAtPath("Assets/HomaGamesStore/StoreEditorWindowSettings.asset", typeof(StoreEditorWindowSettings));
        if (settings == null)
        {
            settings = CreateInstance<StoreEditorWindowSettings>();
            AssetDatabase.CreateAsset(settings, "Assets/HomaGamesStore/StoreEditorWindowSettings.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    void OnDestroy()
    {
        AssetDatabase.SaveAssets();
    }

    void OnGUI()
    {
        string[] folders = new string[1];
        folders[0] = settings.ItemsFolder;
        StoreItemSettings[] storeItemSettings = GetAllInstances<StoreItemSettings>(folders);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        ///ITEMS LIST
        GUILayout.Label("Items : ", EditorStyles.boldLabel);
        foreach (StoreItemSettings item in storeItemSettings)
        {
            ItemLabel(item);
        }
        GUIStyle itemFoundStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 10 };
        GUILayout.Label("total : " + storeItemSettings.Length + " item(s).", itemFoundStyle);

        EditorGUILayout.EndScrollView();
        GUILayout.Space(5);
        GUILayout.Label("Create new items using the Assets menu : Assets/Create/HomaGames/Store/...", EditorStyles.helpBox);

        if (GUILayout.Button("Sync Items"))
        {
            List<StoreItem> storeItems = new List<StoreItem>();
            foreach (StoreItemSettings item in storeItemSettings)
            {
                StoreItem storeItem = new StoreItem();
                storeItem.Icon = item.Icon;
                storeItem.Id = item.Id;
                storeItem.Name = item.Name;
                storeItem.Price = item.Price;

                GameObject prefab = item.FBX;
               // prefab.AddComponent(settings.Animators[0].Animator.GetType());
                prefab.AddComponent(settings.Colliders[0].Collider.GetType());

                storeItem.Prefab = prefab;

                storeItems.Add(storeItem);
            }
            Store.Instance.StoreItems = storeItems;
        }

        if (GUILayout.Button("Export Store Spreadsheet (CSV)"))
        {
            string filePath = Application.dataPath + "/StoreExport.csv";
            if (AssetDatabase.IsValidFolder(folders[0]))
                filePath = folders[0] + "/StoreExport.csv";

            StreamWriter writer = new StreamWriter(filePath);

            writer.WriteLine("Id,Name,Price");
            foreach (StoreItemSettings item in storeItemSettings)
            {
                writer.WriteLine(item.Id + "," + item.Name + "," + item.Price);
            }
            writer.Close();
        }
    }

    public static T[] GetAllInstances<T>(string[] folders) where T : ScriptableObject
    {
        string[] guids;
        if (folders != null && folders[0] != null && folders[0] != "")
            guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, folders);  //FindAssets uses tags check documentation for more info
        else
            guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info

        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }

    private static void ItemLabel(StoreItemSettings item)
    {
        GUILayout.BeginHorizontal();
        GUIStyle style = new GUIStyle(GUI.skin.label);
        GUIStyle imageStyle = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleLeft;
        style.fixedHeight = 50;
        imageStyle.fixedHeight = 50;
        imageStyle.fixedWidth = 50;
        GUILayout.Label(item.Icon.texture, imageStyle);
        GUILayout.Label(item.Id.ToString(), style);
        GUILayout.Label(item.Name, style);
        GUILayout.Label(item.Price.ToString(), style);
        GUILayout.EndHorizontal();
    }
}
