//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class plateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void setKitchenObjectSO(kitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
