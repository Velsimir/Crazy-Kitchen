using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;

    private void Start()
    {
        GameStates.Instance.OnStateChanged += Instance_OnStateChanged;
    }

    private void Update()
    {
        _countdownText.text = Mathf.Ceil(GameStates.Instance.GetCountdownStartTimer()).ToString();
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
