using UnityEngine;
using UnityEngine.UI;

public class ProgressBarCutting : MonoBehaviour
{
    [SerializeField] private Image _cuttingImage;
    [SerializeField] private GameObject _hasProgressBarGameObject;

    private IProgressBar _progressBar;

    private void Awake()
    {
        _progressBar = _hasProgressBarGameObject.GetComponent<IProgressBar>();
        _cuttingImage.fillAmount = 0f;

        _progressBar.OnProgressChanged += OnProgressChanged;
        Debug.Log("Подписался");

        Hide();
    }

    private void OnDestroy()
    {
        _progressBar.OnProgressChanged -= OnProgressChanged;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
            _progressBar.OnProgressChanged -= OnProgressChanged;
        else
            _progressBar.OnProgressChanged += OnProgressChanged;
    }

    private void OnProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e)
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
