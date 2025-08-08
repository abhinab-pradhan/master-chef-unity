using System;
using System.Collections.Generic;
using UnityEngine;

public class plateCompleteVisual : MonoBehaviour
{[Serializable]
    public struct kitchenObjectSO_gameObject
    {
        public kitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private plateKitchenObject plateKitchenObject;
    [SerializeField] private List<kitchenObjectSO_gameObject> kitchenObjectSOGameObjectList;
    void Start()
    {
        plateKitchenObject.onIngridentAdded += plateKitchenObject_onIngridentAdded;

    }
    void plateKitchenObject_onIngridentAdded(object sender, plateKitchenObject.onIngridentAddedEventArgs e)
    {
        foreach (kitchenObjectSO_gameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
