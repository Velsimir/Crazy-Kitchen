using System;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;

    new public static void ResetStaticData()
    {
        OnTrash = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKithcenObject())
        {
            player.GetKithcenObject().DestrySelf();

            OnTrash?.Invoke(this,EventArgs.Empty);
        }
    }
}
