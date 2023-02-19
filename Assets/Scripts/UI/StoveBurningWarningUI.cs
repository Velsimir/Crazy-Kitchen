using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;

    private float _burnShowProgressAnount = 0.5f;
    private bool _isBurning;

    private void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
        Hide();
    }

    private void StoveCounterOnProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e)
    {
        _isBurning = _stoveCounter.IsFried() && e.ProgressNormalized >= _burnShowProgressAnount;

        if (_isBurning)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
