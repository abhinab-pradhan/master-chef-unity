using System;
using System.Collections.Generic;
using UnityEngine;

public class deliveryManager : MonoBehaviour
{

    public event EventHandler onRecipeSpawned;
    public event EventHandler onRecipeCompleted;

    public event EventHandler onRecipeSuccess;
    public event EventHandler onRecipeFailed;
    public static deliveryManager instance { get; private set; }
    [SerializeField] private recipeListSO recipeListSO;
    private List<recipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private int successfulRecipeAmount;
    void Awake()
    {
        instance = this;
        waitingRecipeSOList = new List<recipeSO>();
    }
    void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (kitchenGameManager.instance.isGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax)
            {
                recipeSO waitingRecipeSO = recipeListSO.recipeListSOList[UnityEngine.Random.Range(0, recipeListSO.recipeListSOList.Count)];
                //Debug.Log(waitingRecipeSO.recpieName);
                waitingRecipeSOList.Add(waitingRecipeSO);

                onRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void deliveryRecipe(plateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            recipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //same number of ingredients
                bool plateContentMathcesRecipe = true;

                foreach (kitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //cycle through in recipe
                    bool ingredientFound = false;
                    foreach (kitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //cycle through in plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //recipe ingredient not found
                        plateContentMathcesRecipe = false;
                    }
                }
                if (plateContentMathcesRecipe)
                {
                    successfulRecipeAmount++;
                    //Debug.Log("delivered correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    onRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    onRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // Debug.Log("not delivered correct recipe");
        onRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<recipeSO> getWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int getSuccessfulRecipeAmount()
    {
        return successfulRecipeAmount;
    }
}
