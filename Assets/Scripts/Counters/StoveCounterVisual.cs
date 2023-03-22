using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _partincleOnGameObject;

    private bool _isVisual;

    private void Start()
    {
        _stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void OnDestroy()
    {
        _stoveCounter.OnStateChange -= StoveCounter_OnStateChange;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
            _stoveCounter.OnStateChange -= StoveCounter_OnStateChange;
        else
            _stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        _isVisual = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
        _stoveOnGameObject.SetActive(_isVisual);
        _partincleOnGameObject.SetActive(_isVisual);
    }
}
