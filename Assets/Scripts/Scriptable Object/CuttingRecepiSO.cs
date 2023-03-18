using UnityEngine;

[CreateAssetMenu(menuName = "Recepies/CuttingRecipe", order = 52)]
public class CuttingRecepiSO : ScriptableObject
{
    public KitchenObjectSO KitchenObjectSOInput;
    public KitchenObjectSO KitchenObjectSOOutput;

    public int CuttingProgressMax;
}
