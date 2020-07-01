using UnityEngine;

[CreateAssetMenu(fileName="NewStoreItem", menuName = "HomaGames/Store/Character")]
public class StoreItemSettings : ScriptableObject
{
    public int Id;
    public string Name;
    public int Price;
    public Sprite Icon;
    public GameObject FBX;
    [HideInInspector]
    public string AnimatorName;
    [HideInInspector]
    public string ColliderName;
}
