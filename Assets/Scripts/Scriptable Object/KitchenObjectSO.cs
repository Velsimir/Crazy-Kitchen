using UnityEngine;

[CreateAssetMenu(menuName = "KitchenObject/KitchenObject", order = 52)]
public class KitchenObjectSO : ScriptableObject
{
    public Transform Prefab;
    public Sprite Sprite;
    public string Name;
}
