using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecepiSO[] _cuttingRecepiSOArray;

    public event EventHandler <OnProgressChangedEventArgs>  OnProgressChanged;
    public event EventHandler OnCut;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float ProgressNormalized;
    }

    private int _cuttingProgress;
    private int _minCuttingProgress = 0;

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

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        ProgressNormalized = (float) _cuttingProgress / cuttingRecepiSO.CuttingProgressMax
                    });
                }
            }
        }
        else
        {
            if (player.HasKithcenObject() == false)
                GetKithcenObject().SetKitchenObjectParent(player);
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKithcenObject() && GetKithcenObject().GetKitchenObjectSO())
        {
            _cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecepiSO cuttingRecepiSO = GetCuttingRecepiSOWithInput(GetKithcenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
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
            {
                return cuttingRecepiSO;
            }
        }

        return null;
    }
}
