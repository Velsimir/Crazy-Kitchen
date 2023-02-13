using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class RecepiSO : ScriptableObject
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
