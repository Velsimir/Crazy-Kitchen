using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipesDeliveredText;

    private void Start()
    {
        GameStates.Instance.OnStateChanged += Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameStates.Instance.IsGameOver())
        {
            _recipesDeliveredText.text = DeliveryOrders.Instance.GetSuccessfilRecipeAmount().ToString();
            Show();
        }
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
