using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSO_GameObjectsList;

    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }

    private void Start()
    {
        _plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in _kitchenObjectSO_GameObjectsList)
        {
            kitchenObjectSO_GameObject.GameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in _kitchenObjectSO_GameObjectsList)
        {
            if (kitchenObjectSO_GameObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSO_GameObject.GameObject.SetActive(true);
            }
        }
    }
}
