using UnityEngine;

[CreateAssetMenu(menuName = "Recepies/BurningRecipe", order = 52)]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO KitchenObjectSOInput;
    public KitchenObjectSO KitchenObjectSOOutput;

    public int BurningTimerMax;
}
