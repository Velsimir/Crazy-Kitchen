using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;

    public override void Interact(Player player)
    {
        if (player.HasKithcenObject())
        {
            player.GetKithcenObject().DestrySelf();

            OnTrash?.Invoke(this,EventArgs.Empty);
        }
    }
}
