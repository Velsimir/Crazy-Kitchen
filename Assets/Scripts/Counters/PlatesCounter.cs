using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private float _maxTimerSpawnPlate;
    [SerializeField] private int _maxPlatesSpawnCount;

    public event EventHandler OnPlatesSpawn;
    public event EventHandler OnPlatesRemoved;

    private float _currentTimerSpawnPlate;
    private float _defoultTimerSpawnPlate = 0f;
    private int _currentPlatesCount;

    private void Update()
    {
        SpawnPlates();
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

    private void SpawnPlates()
    {
        _currentTimerSpawnPlate += Time.deltaTime;

        if (_currentTimerSpawnPlate > _maxTimerSpawnPlate)
        {
            _currentTimerSpawnPlate = _defoultTimerSpawnPlate;

            if (GameStates.Instance.IsGamePlaying() && _currentPlatesCount < _maxPlatesSpawnCount)
            {
                _currentPlatesCount++;

                OnPlatesSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
