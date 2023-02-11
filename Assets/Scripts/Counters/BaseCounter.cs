using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;

    public virtual void Interact(Player player) { }

    public virtual void InteractAlternate(Player player) { }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKithcenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
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