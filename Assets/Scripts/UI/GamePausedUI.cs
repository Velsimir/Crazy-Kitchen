using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _optionsButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(() =>
        {
            GameStates.Instance.TogglePauseGame();
        });

        _optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instanse.Show(Show );
        });

        _mainMenuButton.onClick.AddListener(() =>
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

    private void OnDisable()
    {
        GameStates.Instance.OnGamePaused -= GameStatesOnGamePaused;
        GameStates.Instance.OnGameUnpaused -= GameStatesOnGameUnpaused;
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

        _resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
