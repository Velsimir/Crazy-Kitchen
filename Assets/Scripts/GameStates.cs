using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public static GameStates Instance { get; private set; }
    public event EventHandler OnStateChanged;

    enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;
    private float _waitingToStartTimer = 1f;
    private float _countdownToStart = 3f;
    private float _gamePlayingTimer;
    private float _gamePlayingTimerMax = 10f;

    private void Awake()
    {
        Instance = this;

        _state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;

                if (_waitingToStartTimer <= 0f)
                    _state = State.CountdownToStart;

                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;

            case State.CountdownToStart:
                _countdownToStart -= Time.deltaTime;

                if (_countdownToStart <= 0f)
                {
                    _state = State.GamePlaying;

                    _gamePlayingTimer = _gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                    
                break;

            case State.GamePlaying:
                _gamePlayingTimer -= Time.deltaTime;

                if (_gamePlayingTimer <= 0f)
                    _state = State.GameOver;

                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;

            case State.GameOver:
                break;
        }

        Debug.Log(_state);
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

    public float GetGamePlayinTimerNormalized ()
    {
        return 1 - _gamePlayingTimer / _gamePlayingTimerMax;
    }
}
