using System;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class plateKitchenObject : kitchenObject
{
    public event EventHandler<onIngridentAddedEventArgs> onIngridentAdded;
    public class onIngridentAddedEventArgs : EventArgs
    {
        public kitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] private List<kitchenObjectSO> validKitchenObjectSOList;
    private List<kitchenObjectSO> kitchenObjectSOList;
    void Awake()
    {
        kitchenObjectSOList = new List<kitchenObjectSO>();
    }
    public bool tryAddIngredient(kitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {

            kitchenObjectSOList.Add(kitchenObjectSO);
            onIngridentAdded?.Invoke(this, new onIngridentAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;

        }
    }

    public List<kitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
