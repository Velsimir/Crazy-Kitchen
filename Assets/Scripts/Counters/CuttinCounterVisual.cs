using UnityEngine;

public class CuttinCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObjectArray;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void OnDisable()
    {
        Player.Instance.OnSelectedCounterChanged -= Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectCounter == _baseCounter)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        foreach (var item in _visualGameObjectArray)
        {
            item.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var item in _visualGameObjectArray)
        {
            item.SetActive(false );
        }
    }
}
