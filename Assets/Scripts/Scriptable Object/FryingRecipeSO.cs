using UnityEngine;

[CreateAssetMenu(menuName = "Recepies/FryingRecipe", order = 52)]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO KitchenObjectSOInput;
    public KitchenObjectSO KitchenObjectSOOutput;

    public int FryingTimerMax;
}
