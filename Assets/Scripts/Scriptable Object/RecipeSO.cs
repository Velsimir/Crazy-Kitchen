using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 52, menuName = "Recepies/Recipe")]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private List<KitchenObjectSO> _kitchenObjectSOList;
    [SerializeField] private string _recepiName;

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return _kitchenObjectSOList;
    }

    public string GetName()
    {
        return _recepiName;
    }
}
