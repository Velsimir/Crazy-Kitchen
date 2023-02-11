using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecepiSO : ScriptableObject
{
    public KitchenObjectSO KitchenObjectSOInput;
    public KitchenObjectSO KitchenObjectSOOutput;

    public int FryingTimerMax;
}
