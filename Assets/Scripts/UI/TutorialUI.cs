using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyMoveUpText;
    [SerializeField] private TextMeshProUGUI _keyMoveDownText;
    [SerializeField] private TextMeshProUGUI _keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI _keyMoveRightText;
    [SerializeField] private TextMeshProUGUI _keyMoveGamepadText;
    [SerializeField] private TextMeshProUGUI _keyMoveInteractionText;
    [SerializeField] private TextMeshProUGUI _keyMoveInteractionAlternativeText;
    [SerializeField] private TextMeshProUGUI _keyMovePauseText;
    [SerializeField] private TextMeshProUGUI _keyInteractionGamepadText;
    [SerializeField] private TextMeshProUGUI _keyInteractionAlternativeGamepadText;
    [SerializeField] private TextMeshProUGUI _keyPauseGamepadText;

    private void Start()
    {
        GameInput.Instanse.OnBindingRebind += GameInputOnBindingRebind;
        GameStates.Instance.OnStateChanged += GameStatesOnStateChanged;

        UpdateVisual();
        Show();
    }

    private void GameStatesOnStateChanged(object sender, System.EventArgs e)
    {
        if (GameStates.Instance.IsCountdownStart())
        {
            Hide();
        }
    }

    private void GameInputOnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _keyMoveUpText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.MoveUp);
        _keyMoveDownText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.MoveDown);
        _keyMoveLeftText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.MoveLeft);
        _keyMoveRightText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.MoveRight);
        _keyMoveInteractionText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.Interact);
        _keyMoveInteractionAlternativeText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.InteractAlternative);
        _keyMovePauseText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.Pause);
        _keyInteractionGamepadText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.GamepadInteract);
        _keyInteractionAlternativeGamepadText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.GamepadInteractAlternative);
        _keyPauseGamepadText.text = GameInput.Instanse.GetBindingText(GameInput.Binding.GamepadPause);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
