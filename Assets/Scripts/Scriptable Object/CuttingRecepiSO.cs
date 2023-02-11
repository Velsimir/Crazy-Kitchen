using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecepiSO : ScriptableObject
{
    public KitchenObjectSO KitchenObjectSOInput;
    public KitchenObjectSO KitchenObjectSOOutput;

    public int CuttingProgressMax;
}
