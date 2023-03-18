using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IsFlashing = "IsFlashing";

    [SerializeField] private StoveCounter _stoveCounter;

    private float _burnShowProgressAnount = 0.5f;
    private bool _isBurning;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
    }

    private void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
    }

    private void OnDisable()
    {
        _stoveCounter.OnProgressChanged -= StoveCounterOnProgressChanged;
    }

    private void StoveCounterOnProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e)
    {
        _isBurning = _stoveCounter.IsFried() && e.ProgressNormalized >= _burnShowProgressAnount;

        _animator.SetBool(IsFlashing, _isBurning);
    }
}
