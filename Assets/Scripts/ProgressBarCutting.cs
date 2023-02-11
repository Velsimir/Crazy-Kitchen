using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarCutting : MonoBehaviour
{
    [SerializeField] private Image _cuttingImage;

    [SerializeField] private CuttingCounter _cuttingCounter;

    private void Start()
    {
        _cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        _cuttingImage.fillAmount = 0f;

        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        _cuttingImage.fillAmount = e.ProgressNormalized;

        if (e.ProgressNormalized == 0 || e.ProgressNormalized == 1)
            Hide();
        else
            Show();
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
