using System;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    [SerializeField] private float _gamePlayingTimerMax = 10f;
     
    public static GameStates Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;
    private float _countdownToStart = 3f;
    private float _gamePlayingTimer;
    private bool _isGamePaused = false;

    private void Awake()
    {
        Instance = this;

        _state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instanse.Pause += GameInputOnPause;
        GameInput.Instanse.InteractAction += GameInputOnInteractAction;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                break;

            case State.CountdownToStart:
                CountdownToStart();
                break;

            case State.GamePlaying:
                GamePlaying();
                break;

            case State.GameOver:
                break;
        }
    }

    private void OnDisable()
    {
        GameInput.Instanse.Pause -= GameInputOnPause;
        GameInput.Instanse.InteractAction -= GameInputOnInteractAction;
    }

    private void GameInputOnInteractAction(object sender, EventArgs e)
    {
        if (_state == State.WaitingToStart)
        {
            _state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInputOnPause(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }

    public bool IsCountdownStart()
    {
        return _state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }

    public float GetCountdownStartTimer()
    {
        return _countdownToStart;
    }

    public float GetGamePlayinTimerNormalized()
    {
        return 1 - _gamePlayingTimer / _gamePlayingTimerMax;
    }

    public void TogglePauseGame()
    {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused == false)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GamePlaying()
    {
        _gamePlayingTimer -= Time.deltaTime;

        if (_gamePlayingTimer <= 0f)
            _state = State.GameOver;

        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void CountdownToStart()
    {
        _countdownToStart -= Time.deltaTime;

        if (_countdownToStart <= 0f)
        {
            _state = State.GamePlaying;

            _gamePlayingTimer = _gamePlayingTimerMax;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
