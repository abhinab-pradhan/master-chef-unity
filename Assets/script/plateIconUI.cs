using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class plateIconUI : MonoBehaviour
{
    [SerializeField] private plateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        plateKitchenObject.onIngridentAdded += plateKitchenObject_onIngridentAdded;
    }
    void plateKitchenObject_onIngridentAdded(object sender, plateKitchenObject.onIngridentAddedEventArgs e)
    {
        updateVisual();
    }
    void updateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (kitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<plateIconSingleUI>().setKitchenObjectSO(kitchenObjectSO);
        }
    }
}
