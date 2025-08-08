using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform getKitchenObjectFollowTransform();
    public void setKitchenObject(kitchenObject kitchenObject);

    public kitchenObject GetKitchenObject();

    public void clearKitchenObject();

    public bool hasKitchenObject();

}
