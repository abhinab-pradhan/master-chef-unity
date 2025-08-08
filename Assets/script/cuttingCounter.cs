using System;
using UnityEngine;

public class cuttingCounter : baseCounter,iHasProgress
{
    //[SerializeField] private kitchenObjectSO cutKitchenObjectSO;
    [SerializeField] private cuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    public event EventHandler<iHasProgress.onProgressChangeEventArgs> onProgressChange;

    public event EventHandler onCut;
    public static event EventHandler onAnyCut;

    new public static void resetStaticData()
    {
        onAnyCut = null;
    }
    public override void interAct(player player)
    {
        //base.interAct(player);
        if (!hasKitchenObject())
        {
            //no kitchen object here
            if (player.hasKitchenObject())
            {
                //player carrying something
                if (hasRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying something that can be cut
                    player.GetKitchenObject().setKitchenObjectParent(this);
                    cuttingProgress = 0;
                    cuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    onProgressChange?.Invoke(this, new iHasProgress.onProgressChangeEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });

                }

            }
            else
            {
                //player has nothing

            }
        }
        else
        {
            if (player.hasKitchenObject())
            {
                //player carrying something
                if (player.GetKitchenObject().tryGetPlate(out plateKitchenObject plateKitchenObject))
                {
                    //player holding a plate
                    //plateKitchenObject plateKitchenObject = player.GetKitchenObject() as plateKitchenObject;
                    if (plateKitchenObject.tryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().destroySelf();

                    }
                }
            }
            else
            {
                //player not carrying anything
                GetKitchenObject().setKitchenObjectParent(player);
            }
        }
    }
    public override void interActAlternate(player player)
    {
        //base.interActAlternate(player);
        if (hasKitchenObject() && hasRecipeInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;

            onCut?.Invoke(this, EventArgs.Empty);
            onAnyCut?.Invoke(this, EventArgs.Empty);
            cuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            onProgressChange?.Invoke(this, new iHasProgress.onProgressChangeEventArgs
                    {
                        progressNormalized=(float)cuttingProgress/cuttingRecipeSO.cuttingProgressMax
                    });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {


                //has kitchen object and can be cut
                kitchenObjectSO outputKitchenObjectSO = getOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().destroySelf();
                kitchenObject.spawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
    private kitchenObjectSO getOutputForInput(kitchenObjectSO inputkitchenObjectSO)
    {
        cuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputkitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;

        }
    }

    private bool hasRecipeInput(kitchenObjectSO inputkitchenObjectSO)
    {
        cuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputkitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private cuttingRecipeSO GetCuttingRecipeSOWithInput(kitchenObjectSO InputkitchenObjectSO)
    {
        foreach (cuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == InputkitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
