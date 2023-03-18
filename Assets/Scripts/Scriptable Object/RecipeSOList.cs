using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recepies/ListRecipes", order = 52)]
public class RecipeSOList : ScriptableObject
{
    public List<RecipeSO> _recepiSOList;

    public List<RecipeSO> GetRecepiSoList()
    {
        return _recepiSOList;
    }
}