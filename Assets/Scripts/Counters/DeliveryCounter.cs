using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKithcenObject())
        {
            if (player.GetKithcenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                player.GetKithcenObject().DestrySelf();
            }
        }
    }
}
