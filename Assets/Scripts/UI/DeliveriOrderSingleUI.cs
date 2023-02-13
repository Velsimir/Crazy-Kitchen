using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveriOrderSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeNameText;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSOName(RecepiSO recepiSO)
    {
        _recipeNameText.text = recepiSO.GetName();

        foreach (Transform child in _iconContainer)
        {
            if (child == _iconTemplate) continue;
            {
                Destroy(child.gameObject);
            }
        }

        foreach (KitchenObjectSO kitchenObjectSO in recepiSO.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(_iconTemplate, _iconContainer);

            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.Sprite;
        }
    }
}
