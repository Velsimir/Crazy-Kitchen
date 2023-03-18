using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform _counterTopPoint;

    public static event EventHandler OnAnyObjectPlacedHere;

    private KitchenObject _kitchenObject;

    public virtual void Interact(Player player) { }

    public virtual void InteractAlternate(Player player) { }

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKithcenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;

        if (kitchenObject != null)
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKithcenObject()
    {
        return _kitchenObject;
    }

    public void ClearKithcenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKithcenObject()
    {
        return _kitchenObject != null;
    }
}
