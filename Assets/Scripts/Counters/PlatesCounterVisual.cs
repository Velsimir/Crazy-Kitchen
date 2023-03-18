using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter _platesCounter;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;

    private List<GameObject> _plateVisualGameObjectList;

    private void Awake()
    {
        _plateVisualGameObjectList = new List<GameObject>();
    }

    private void OnEnable()
    {
        _platesCounter.OnPlatesSpawn += PlatesCounter_OnPlatesSpawn;
        _platesCounter.OnPlatesRemoved += PlatesCounter_OnPlatesRemoved;
    }

    private void OnDisable()
    {
        _platesCounter.OnPlatesSpawn -= PlatesCounter_OnPlatesSpawn;
        _platesCounter.OnPlatesRemoved -= PlatesCounter_OnPlatesRemoved;
    }

    private void PlatesCounter_OnPlatesRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];

        _plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlatesSpawn(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);

        float plateOffsetY = .1f;

        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualGameObjectList.Count,0);

        _plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
