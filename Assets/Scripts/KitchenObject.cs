using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    private IKitchenObjectParent _KitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent KitchenObject)
    {
        if (_KitchenObjectParent != null)
        {
            _KitchenObjectParent.ClearKithcenObject(); 
        }

        _KitchenObjectParent = KitchenObject;

        if (_KitchenObjectParent.HasKithcenObject())
        {
            Debug.Log("KitchenObject alredy has Kitchen Object");
        }

        _KitchenObjectParent.SetKithcenObject(this);

        transform.parent = _KitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return _KitchenObjectParent;
    }

    public void DestrySelf()
    {
        _KitchenObjectParent.ClearKithcenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}