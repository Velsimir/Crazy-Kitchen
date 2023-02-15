using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryOrders : MonoBehaviour
{
    public static DeliveryOrders Instance { get; private set; }

    [SerializeField] private RecepiSOList _recepiSOList;
    [SerializeField] private List<RecepiSO> _waitingRecepiSOList;
    [SerializeField] private float _recepiSpawnTimerMax = 4f;
    [SerializeField] private int _waitingRecepiMax = 4;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeComplited;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed ;

    private float _currentRecepiSpawnTimer;
    private int _successfulRecipeAmout;

    private void Awake()
    {
        Instance = this;

        _waitingRecepiSOList = new List<RecepiSO>();
    }

    private void Update()
    {
        _currentRecepiSpawnTimer -= Time.deltaTime;

        if (_currentRecepiSpawnTimer <= 0)
        {
            _currentRecepiSpawnTimer = _recepiSpawnTimerMax;

            if (_waitingRecepiSOList.Count < _waitingRecepiMax)
            {
                RecepiSO recepiSO = _recepiSOList.GetRecepiSoList()[UnityEngine.Random.Range(0, _recepiSOList.GetRecepiSoList().Count)];

                _waitingRecepiSOList.Add(recepiSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecepi(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecepiSOList.Count; i++)
        {
            RecepiSO waitingRecepiSO = _waitingRecepiSOList[i];

            if (waitingRecepiSO.GetKitchenObjectSOList().Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;

                foreach (KitchenObjectSO recepiKitchenObjectSO in waitingRecepiSO.GetKitchenObjectSOList())
                {
                    bool isIngridienFound = false;

                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recepiKitchenObjectSO)
                        {
                            isIngridienFound = true;
                            break;
                        }
                    }

                    if (isIngridienFound == false)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    _waitingRecepiSOList.RemoveAt(i);

                    _successfulRecipeAmout++;

                    OnRecipeComplited?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this,EventArgs.Empty);

                    return; 
                }
            }
        }

        OnRecipeFailed?.Invoke(this,EventArgs.Empty);
    }

    public List<RecepiSO> GetWaitingRecepiSOLists()
    {
        return _waitingRecepiSOList;
    }

    public int GetSuccessfilRecipeAmount()
    {
        return _successfulRecipeAmout;
    }
}
