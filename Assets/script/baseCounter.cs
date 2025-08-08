using System;
using UnityEngine;

public class baseCounter : MonoBehaviour, IKitchenObjectParent
{

    //[SerializeField] private kitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public static event EventHandler onAnyObjectPlacedHere;
    public static void resetStaticData()
    {
        onAnyObjectPlacedHere = null;
    }


    private kitchenObject kitchenObject;
    public virtual void interAct(player player)
    {
        Debug.Log("basecounter.interAct()");
    }
        public virtual void interActAlternate(player player)
    {
        Debug.Log("basecounter.interActAlternate()");
    }
            public Transform getKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void setKitchenObject(kitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            onAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }
    public kitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void clearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool hasKitchenObject()
    {
        return kitchenObject != null;
    }
}
