using System;
using System.Collections;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private float _maxTimerSpawnPlate;
    [SerializeField] private int _maxPlatesSpawnCount;

    public event EventHandler OnPlatesSpawn;
    public event EventHandler OnPlatesRemoved;

    private int _currentPlatesCount;
    private Coroutine _coroutineSpanPlate;

    private void Update()
    {
        if (_coroutineSpanPlate == null)
        {
            _coroutineSpanPlate = StartCoroutine(TimerSpawnPlate());
        }
    }

    public override void Interact(Player player)
    {
        if (player.HasKithcenObject() == false)
        {
            if (_currentPlatesCount > 0)
            {
                _currentPlatesCount--;

                KitchenObject.SpawnKitchenObject(_kitchenObjectSO, player);

                OnPlatesRemoved?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    private IEnumerator TimerSpawnPlate()
    {
        yield return new WaitForSeconds(3);

        if (GameStates.Instance.IsGamePlaying() && _currentPlatesCount < _maxPlatesSpawnCount)
        {
            _currentPlatesCount++;
            OnPlatesSpawn?.Invoke(this, EventArgs.Empty);
        }

        StopCoroutine(_coroutineSpanPlate);
        _coroutineSpanPlate = null;
    }
}