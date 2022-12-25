using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "MixMash/newIngredient", order = 1)]
public class Ingredient : ScriptableObject
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Ingredient _sliced;
    [SerializeField] private Ingredient _cooked;

    public string Name => name;
}