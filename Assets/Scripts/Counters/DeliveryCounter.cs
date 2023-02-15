using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public static DeliveryCounter Instanse { get; private set; }

    private void Awake()
    {
        Instanse = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKithcenObject())
        {
            if (player.GetKithcenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryOrders.Instance.DeliverRecepi(plateKitchenObject);

                player.GetKithcenObject().DestrySelf();
            }
        }
    }
}
