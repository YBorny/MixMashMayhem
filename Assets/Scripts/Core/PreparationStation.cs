using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationStation : MonoBehaviour
{
    [SerializeField] private Recipe[] _prepareableRecipes;
    private List<Ingredient> _containedIngredients = new List<Ingredient>();

    public bool AddIngredient(Ingredient ingredient)
    {
        Recipe[] recipes = DeterminePrepareableRecipes();
        bool succesFull = false;

        foreach (var recipe in recipes)
        {
            if (recipe.Ingredients.Contains(ingredient))
            {
                succesFull = true;
                break;
            }
        }

        if (!succesFull) 
            return false;

        _containedIngredients.Add(ingredient);

        return true;
    }

    private Recipe[] DeterminePrepareableRecipes()
    {
        List<Recipe> recipes = new List<Recipe>();
        recipes.AddRange(_prepareableRecipes);

        if (_containedIngredients.Count <= 0) return recipes.ToArray();

        foreach (var recipe in _prepareableRecipes)
        {
            bool prepareable = false;
            foreach (var ingredient in _containedIngredients)
            {
                if(recipe.Ingredients.Contains(ingredient))
                {
                    prepareable = true;
                    break;
                }
            }

            if (!prepareable)
                recipes.Remove(recipe);
        }

        return recipes.ToArray();
    }

    public Ingredient RemoveIngredient()
    {
        Ingredient ingredient = _containedIngredients[_containedIngredients.Count - 1];
        _containedIngredients.Remove(ingredient);
        return ingredient;
    }

    private void StartPreparing()
    {

    }
}
