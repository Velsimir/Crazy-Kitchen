using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button _soundEffectsButton;
    [SerializeField] private Button _backgroundMusicButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _interactButton;
    [SerializeField] private Button _interactAlternativeButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _gamepadInteractButton;
    [SerializeField] private Button _gamepadInteractAlternativeButton;
    [SerializeField] private Button _gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI _soundEffectsText;
    [SerializeField] private TextMeshProUGUI _backgorundMusicText;
    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _interactAlternativeText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractAlternativeText;
    [SerializeField] private TextMeshProUGUI _gamepadPauseText;
    [SerializeField] private Transform _pressToRebindKeyTransform;

    private Action _onCloseButtonAction;


    public static OptionsUI Instanse { get; private set; }

    private void Awake()
    {
        Instanse = this;

        _soundEffectsButton.onClick.AddListener(() =>
        {
            KitchenSounds.Instanse.ChangeVolume();
            UpdateVisual();
        });

        _backgroundMusicButton.onClick.AddListener(() =>
        {
            BackgroundMusic.Instanse.ChangeVolume();
            UpdateVisual();
        });

        _backButton.onClick.AddListener(() =>
        {
            Hide();
            _onCloseButtonAction();
        });

        _moveUpButton.onClick.AddListener(() =>{ RebindBindig(GameInput.Binding.MoveUp); });
        _moveDownButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.MoveDown); });
        _moveLeftButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.MoveLeft); });
        _moveRightButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.MoveRight); });
        _interactButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.Interact); });
        _interactAlternativeButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.InteractAlternative); });
        _pauseButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.Pause); });
        _gamepadInteractButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.GamepadInteract); });
        _gamepadInteractAlternativeButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.GamepadInteractAlternative); });
        _gamepadPauseButton.onClick.AddListener(() => { RebindBindig(GameInput.Binding.GamepadPause); });
    }

    private void Start()
    {
        GameStates.Instance.OnGamePaused += GameStatesOnGamePaused;

        UpdateVisual();

        HidePressRebindKey();
        Hide();
    }

    public void Show(Action onCloseButtonAction)
    {
        _onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        _soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(true );
    }

    private void HidePressRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void GameStatesOnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        float volumeSoundEffects = Mathf.Round(KitchenSounds.Instanse.GetVolume() * 10f);
        float volumeBackground = Mathf.Round(BackgroundMusic.Instanse.GetVolume() * 10f);

        _soundEffectsText.text = $"Sound effects:{volumeSoundEffects}";
        _backgorundMusicText.text = $"Background music:{volumeBackground}";
        _moveUpText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.MoveUp);
        _moveDownText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.MoveDown);
        _moveLeftText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.MoveLeft);
        _moveRightText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.MoveRight);
        _interactText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.Interact);
        _interactAlternativeText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.InteractAlternative);
        _pauseText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.Pause);
        _gamepadInteractText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.GamepadInteract);
        _gamepadInteractAlternativeText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.GamepadInteractAlternative);
        _gamepadPauseText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.GamepadPause);
    }

    private void RebindBindig(GameInput.Binding binding)
    {
        ShowPressRebindKey();
        GameInput.Instanse.RebindBinding(binding, () =>
        {
            HidePressRebindKey();
            UpdateVisual();
        });
    }
}
