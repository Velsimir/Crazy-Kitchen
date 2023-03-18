using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IProgressBar
{
    [SerializeField] private CuttingRecepiSO[] _cuttingRecepiSOArray;

    public static event EventHandler OnAnyCut;
    public event EventHandler <IProgressBar.OnProgressChangedEventArgs>  OnProgressChanged;
    public event EventHandler OnCut;

    private int _cuttingProgress;
    private int _minCuttingProgress = 0;
    private PlateKitchenObject _plateKitchenObject;

    public override void Interact(Player player)
    {
        if (HasKithcenObject() == false)
        {
            if (player.HasKithcenObject())
            {
                if (HasRecepiWithInput(player.GetKithcenObject().GetKitchenObjectSO()))
                {
                    player.GetKithcenObject().SetKitchenObjectParent(this);
                    _cuttingProgress = _minCuttingProgress;

                    CuttingRecepiSO cuttingRecepiSO = GetCuttingRecepiSOWithInput(GetKithcenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = (float) _cuttingProgress / cuttingRecepiSO.CuttingProgressMax
                    });
                }
            }
        }
        else
        {
            if (player.HasKithcenObject())
            {
                if (player.GetKithcenObject().TryGetPlate(out _plateKitchenObject))
                {
                    if (_plateKitchenObject.TryAddingIngridient(GetKithcenObject().GetKitchenObjectSO()))
                        GetKithcenObject().DestrySelf();
                }
            }
            else
                GetKithcenObject().SetKitchenObjectParent(player);
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKithcenObject() && GetKithcenObject().GetKitchenObjectSO() == HasRecepiWithInput(GetKithcenObject().GetKitchenObjectSO()))
        {
            _cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            CuttingRecepiSO cuttingRecepiSO = GetCuttingRecepiSOWithInput(GetKithcenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
            {
                ProgressNormalized = (float)_cuttingProgress / cuttingRecepiSO.CuttingProgressMax
            });

            if (_cuttingProgress >= cuttingRecepiSO.CuttingProgressMax)
            {
                KitchenObjectSO kitchenObjectSO = GetInputRecepiToOutput(GetKithcenObject().GetKitchenObjectSO());

                GetKithcenObject().DestrySelf();

                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            }
        }
    }

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }

    private bool HasRecepiWithInput(KitchenObjectSO inputKitchenObject)
    {
        CuttingRecepiSO cuttingRecepiSO = GetCuttingRecepiSOWithInput(inputKitchenObject);

        return cuttingRecepiSO != null;
    }

    private KitchenObjectSO GetInputRecepiToOutput(KitchenObjectSO inputKitchenObject)
    {
        CuttingRecepiSO cuttingRecepiSO = GetCuttingRecepiSOWithInput(inputKitchenObject);

        if (cuttingRecepiSO != null)
            return cuttingRecepiSO.KitchenObjectSOOutput;
        else
        return null;
    }

    private CuttingRecepiSO GetCuttingRecepiSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var cuttingRecepiSO in _cuttingRecepiSOArray)
        {
            if (cuttingRecepiSO.KitchenObjectSOInput == inputKitchenObjectSO)
                return cuttingRecepiSO;
        }

        return null;
    }
}
