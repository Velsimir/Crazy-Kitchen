using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    private PlateKitchenObject _plateKitchenObject;

    public override void Interact(Player player)
    {
        if (HasKithcenObject() == false)
        {
            if (player.HasKithcenObject())
                player.GetKithcenObject().SetKitchenObjectParent(this);
        }
        else
        {
            if (player.HasKithcenObject())
            {
                if (player.GetKithcenObject().TryGetPlate(out _plateKitchenObject))
                {
                    if (_plateKitchenObject.TryAddingIngridient(GetKithcenObject().GetKitchenObjectSO()))
                    {
                        GetKithcenObject().DestrySelf();
                    }
                }
                else
                {
                    if (GetKithcenObject().TryGetPlate(out _plateKitchenObject))
                    {
                        if (_plateKitchenObject.TryAddingIngridient(player.GetKithcenObject().GetKitchenObjectSO()))
                        {
                            player.GetKithcenObject().DestrySelf();
                        }
                    }
                }
            }
            else
            {
                GetKithcenObject().SetKitchenObjectParent(player);
            }
        }
    }
}