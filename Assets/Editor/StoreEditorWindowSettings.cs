using System.Collections.Generic;
using UnityEngine;

public class StoreEditorWindowSettings : ScriptableObject
{
    [HideInInspector]
    public string ItemsFolder = "";
    [HideInInspector]
    public bool ShowInfos = true;
    [HideInInspector]
    public bool ShowSettings = false;
    public List<ColliderSettings> Colliders;
    public List<AnimatorSettings> Animators;
}
