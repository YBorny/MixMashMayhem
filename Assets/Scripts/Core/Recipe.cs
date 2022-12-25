using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "MixMash/newRecipe", order = 0)]
public class Recipe : ScriptableObject
{
    [SerializeField] private List<Ingredient> _ingredients;
    [SerializeField] private Ingredient _result;
    [SerializeField] private float _prepeartionTime;

    public List<Ingredient> Ingredients => _ingredients;
    public float PreperationTime => _prepeartionTime;
}
