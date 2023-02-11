using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (HasKithcenObject() == false)
        {
            if (player.HasKithcenObject())
                player.GetKithcenObject().SetKitchenObjectParent(this);
        }
        else
        {
            if (player.HasKithcenObject() == false)
                GetKithcenObject().SetKitchenObjectParent(player);
        }
    }
}