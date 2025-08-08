using UnityEngine;

public class kitchenObject : MonoBehaviour
{
    [SerializeField] private kitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public kitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void setKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.clearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.hasKitchenObject())
        {
            Debug.LogError("already kitchen object");
        }
        kitchenObjectParent.setKitchenObject(this);
        transform.parent = kitchenObjectParent.getKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void destroySelf()
    {
        kitchenObjectParent.clearKitchenObject();
        Destroy(gameObject);
    }

    public static kitchenObject spawnKitchenObject(kitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObject kitchenObject = kitchenObjectTransform.GetComponent<kitchenObject>();
        kitchenObject.setKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

    public bool tryGetPlate(out plateKitchenObject plateKitchenObject)
    {
        if (this is plateKitchenObject)
        {
            plateKitchenObject = this as plateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
}
