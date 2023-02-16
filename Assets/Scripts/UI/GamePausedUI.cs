using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button _resume;
    [SerializeField] private Button _mainMenu;

    private void Awake()
    {
        _resume.onClick.AddListener(() =>
        {
            GameStates.Instance.IsGamePlaying();
        });

        _mainMenu.onClick.AddListener(() =>
        {
            LoadingScreen.Load(LoadingScreen.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameStates.Instance.OnGamePaused += GameStatesOnGamePaused;
        GameStates.Instance.OnGameUnpaused += GameStatesOnGameUnpaused;

        Hide();
    }

    private void GameStatesOnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameStatesOnGamePaused(object sender, System.EventArgs e)
    {
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
