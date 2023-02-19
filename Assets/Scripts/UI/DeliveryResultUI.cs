using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]

public class DeliveryResultUI : MonoBehaviour
{
    private const string Popup = "Popup"; 

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _faliedColor;
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _faliedSprite;

    private Animator _animator;
    private string _deliveryFailedText = $"Delivery \nFailed";
    private string _deliverySuccessText = $"Delivery \nSuccess";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryOrders.Instance.OnRecipeSuccess += DeliveryOrdersOnRecipeSuccess;
        DeliveryOrders.Instance.OnRecipeFailed += DeliveryOrdersOnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void DeliveryOrdersOnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);

        _animator.SetTrigger(Popup);

        _backgroundImage.color = _faliedColor;
        _iconImage.sprite = _faliedSprite;
        _messageText.text = _deliveryFailedText;
    }

    private void DeliveryOrdersOnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);

        _animator.SetTrigger(Popup);

        _backgroundImage.color = _successColor;
        _iconImage.sprite = _successSprite;
        _messageText.text = _deliverySuccessText;
    }
}
