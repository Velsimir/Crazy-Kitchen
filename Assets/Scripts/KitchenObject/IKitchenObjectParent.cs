using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKithcenObject(KitchenObject kitchenObject);

    public KitchenObject GetKithcenObject();

    public void ClearKithcenObject();

    public bool HasKithcenObject();
}
