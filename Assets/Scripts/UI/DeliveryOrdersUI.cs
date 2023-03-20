using UnityEngine;

public class DeliveryOrdersUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _recipeTemplate;

    private void Awake()
    {
        _recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryOrders.Instance.OnRecipeSpawned += DeliveryManageOnRecipeSpawned;
        DeliveryOrders.Instance.OnRecipeComplited += DeliveryManageOnRecipeComplited;

        UpdateVisual();
    }

    private void DeliveryManageOnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManageOnRecipeComplited(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _recipeTemplate) continue;
                Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryOrders.Instance.GetWaitingRecepiSOLists())
        {
            Transform resipeTransform = Instantiate(_recipeTemplate, _container);

            resipeTransform.gameObject.SetActive(true);
            resipeTransform.GetComponent<DeliveriOrderSingleUI>().SetRecipeSOName(recipeSO );
        }
    }
}
