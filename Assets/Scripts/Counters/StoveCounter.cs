using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgressBar
{
    [SerializeField] private FryingRecepiSO[] _fryingRecepiSOArray;
    [SerializeField] private BurningRecepiSO[] _burningRecepiSOArray;

    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public class OnStateChangeEventArgs : EventArgs
    {
        public State State; 
    }

    private float _fryingTimer;
    private float _burnedTimer;
    private float _defoultFryingTimer = 0f;
    private FryingRecepiSO _fryingRecepiSO;
    private BurningRecepiSO _burningRecepiSO;
    private State _state;

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private void Start()
    {
        _state = State.Idle;
    }
    private void Update()
    {
        if (HasKithcenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;

                case State.Frying:
                    _fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = _fryingTimer / _fryingRecepiSO.FryingTimerMax
                    });

                    if (_fryingTimer >= _fryingRecepiSO.FryingTimerMax)
                    {
                        GetKithcenObject().DestrySelf();

                        KitchenObject.SpawnKitchenObject(_fryingRecepiSO.KitchenObjectSOOutput, this);

                        _burningRecepiSO = GetBurningRecepiSOWithInput(GetKithcenObject().GetKitchenObjectSO());
                        _state = State.Fried;

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            State = _state
                        });
                    }
                    break;

                case State.Fried:
                    _burnedTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = _burnedTimer / _burningRecepiSO.BurningTimerMax
                    });

                    if (_burnedTimer >= _burningRecepiSO.BurningTimerMax)
                    {
                        GetKithcenObject().DestrySelf();

                        KitchenObject.SpawnKitchenObject(_burningRecepiSO.KitchenObjectSOOutput, this);
                        _state = State.Burned;

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            State = _state
                        });
                    }
                    break;

                case State.Burned:
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = 0f
                    });
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (HasKithcenObject() == false)
        {
            if (player.HasKithcenObject())
            {
                if (HasRecepiWithInput(player.GetKithcenObject().GetKitchenObjectSO()))
                {
                    player.GetKithcenObject().SetKitchenObjectParent(this);
                    _fryingRecepiSO = GetFryingRecepiSOWithInput(GetKithcenObject().GetKitchenObjectSO());

                    _state = State.Frying;
                    _fryingTimer = _defoultFryingTimer;
                    _burnedTimer = _defoultFryingTimer;

                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = _fryingTimer / _fryingRecepiSO.FryingTimerMax
                    });

                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                    {
                        State = _state
                    });
                }
            }
        }
        else
        {
            if (player.HasKithcenObject() == false)
                GetKithcenObject().SetKitchenObjectParent(player);
            _state = State.Idle;

            OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
            {
                ProgressNormalized = 0f
            });

            OnStateChange?.Invoke(this, new OnStateChangeEventArgs
            {
                State = _state
            });
        }
    }

    private bool HasRecepiWithInput(KitchenObjectSO inputKitchenObject)
    {
        FryingRecepiSO fryingRecepiSO = GetFryingRecepiSOWithInput(inputKitchenObject);

        return fryingRecepiSO != null;
    }

    private KitchenObjectSO GetInputRecepiToOutput(KitchenObjectSO inputKitchenObject)
    {
        FryingRecepiSO fryingRecepiSO = GetFryingRecepiSOWithInput(inputKitchenObject);

        if (fryingRecepiSO != null)
            return fryingRecepiSO.KitchenObjectSOOutput;
        else
            return null;
    }

    private FryingRecepiSO GetFryingRecepiSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var fryingRecepiSO in _fryingRecepiSOArray)
        {
            if (fryingRecepiSO.KitchenObjectSOInput == inputKitchenObjectSO)
            {
                return fryingRecepiSO;
            }
        }

        return null;
    }

    private BurningRecepiSO GetBurningRecepiSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var burningRecepiSO in _burningRecepiSOArray)
        {
            if (burningRecepiSO.KitchenObjectSOInput == inputKitchenObjectSO)
            {
                return burningRecepiSO;
            }
        }

        return null;
    }
}
