using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryOrders : MonoBehaviour
{
    public static DeliveryOrders Instance { get; private set; }

    [SerializeField] private RecipeSOList _recepiSOList;
    [SerializeField] private List<RecipeSO> _waitingRecepiSOList;
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

        _waitingRecepiSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        RecipesSpawn();
    }

    public void DeliverRecepi(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecepiSOList.Count; i++)
        {
            RecipeSO waitingRecepiSO = _waitingRecepiSOList[i];

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

    public List<RecipeSO> GetWaitingRecepiSOLists()
    {
        return _waitingRecepiSOList;
    }

    public int GetSuccessfilRecipeAmount()
    {
        return _successfulRecipeAmout;
    }

    private void RecipesSpawn()
    {
        _currentRecepiSpawnTimer -= Time.deltaTime;

        if (_currentRecepiSpawnTimer <= 0)
        {
            _currentRecepiSpawnTimer = _recepiSpawnTimerMax;

            if (GameStates.Instance.IsGamePlaying() && _waitingRecepiSOList.Count < _waitingRecepiMax)
            {
                RecipeSO recepiSO = _recepiSOList.GetRecepiSoList()[UnityEngine.Random.Range(0, _recepiSOList.GetRecepiSoList().Count)];

                _waitingRecepiSOList.Add(recepiSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
