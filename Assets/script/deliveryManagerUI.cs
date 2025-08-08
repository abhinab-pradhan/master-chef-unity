using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class deliveryManagerUI : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        deliveryManager.instance.onRecipeSpawned += deliveryManager_onRecipeSpawned;
        deliveryManager.instance.onRecipeCompleted += deliveryManager_onRecipeCompleted;
        updateVisual();
    }

    void deliveryManager_onRecipeCompleted(object sender, System.EventArgs e)
    {
        updateVisual();
    }
    void deliveryManager_onRecipeSpawned(object sender, System.EventArgs e)
    {
        updateVisual();
    }
    void updateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (recipeSO recipeSO in deliveryManager.instance.getWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<deliveryManagerSingleUI>().setRecipeSO(recipeSO);
        }
    }
}
