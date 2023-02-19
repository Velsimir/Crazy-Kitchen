using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NumberPopup = "NumberPopup";

    [SerializeField] private TextMeshProUGUI _countdownText;

    private Animator _animator;
    private int _previousCountdownTimer;
    private int _countdownTimer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameStates.Instance.OnStateChanged += Instance_OnStateChanged;
    }

    private void Update()
    {
        _countdownTimer = Mathf.CeilToInt(GameStates.Instance.GetCountdownStartTimer());
        _countdownText.text = _countdownTimer.ToString();

        if (_previousCountdownTimer != _countdownTimer)
        {
            _previousCountdownTimer = _countdownTimer;
            _animator.SetTrigger(NumberPopup);
            KitchenSounds.Instanse.PlayCountdownSound();
        }
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameStates.Instance.IsCountdownStart())
            Show();
        else
            Hide();
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
