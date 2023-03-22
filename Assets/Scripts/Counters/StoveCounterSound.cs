using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;

    private AudioSource _audioSource;
    private float _burnShowProgressAnount = 0.5f;
    private float _warningSoundTimer;
    private float _warningSoundTimerMax = 0.2f;
    private bool _isWorningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
    }

    private void Start()
    {
        _stoveCounter.OnStateChange += StoveCounterOnStateChange;
    }

    private void Update()
    {
        AlarmWarning();
    }

    private void OnDestroy()
    {
        _stoveCounter.OnProgressChanged -= StoveCounterOnProgressChanged;
        _stoveCounter.OnStateChange -= StoveCounterOnStateChange;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            _stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
            _stoveCounter.OnStateChange += StoveCounterOnStateChange;
        }
        else
        {
            _stoveCounter.OnProgressChanged -= StoveCounterOnProgressChanged;
            _stoveCounter.OnStateChange -= StoveCounterOnStateChange;
        }
    }

    private void AlarmWarning()
    {
        if (_isWorningSound)
        {
            _warningSoundTimer -= Time.deltaTime;

            if (_warningSoundTimer <= 0f)
            {
                _warningSoundTimer = _warningSoundTimerMax;

                KitchenSounds.Instanse.PlayWarningSound(_stoveCounter.transform.position);
            }
        }
    }

    private void StoveCounterOnProgressChanged(object sender, IProgressBar.OnProgressChangedEventArgs e)
    {
        _isWorningSound = _stoveCounter.IsFried() && e.ProgressNormalized >= _burnShowProgressAnount;
    }

    private void StoveCounterOnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool playSound = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;

        if (playSound)
            _audioSource.Play();
        else
            _audioSource.Pause();
    }
}
