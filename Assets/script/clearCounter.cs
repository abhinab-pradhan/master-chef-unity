using UnityEngine;

public class clearCounter : baseCounter
{

    [SerializeField] private kitchenObjectSO kitchenObjectSO;
    // [SerializeField] private Transform counterTopPoint;


    // private kitchenObject kitchenObject;

    public override void interAct(player player)
    {
        if (!hasKitchenObject())
        {
            //no kitchen object here
            if (player.hasKitchenObject())
            {
                //player carrying
                player.GetKitchenObject().setKitchenObjectParent(this);
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
                else
                {
                    //player is not carrying plate
                    if (GetKitchenObject().tryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.tryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().destroySelf();
                        }
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

}
