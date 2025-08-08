using UnityEngine;

public class deliveryCounter : baseCounter
{
    public static deliveryCounter instance{ get; private set; }

    void Awake()
    {
        instance = this;
    }
    public override void interAct(player player)
    {


        //base.interAct(player);
        if (player.hasKitchenObject())
        {
            if (player.GetKitchenObject().tryGetPlate(out plateKitchenObject plateKitchenObject))
            {

                deliveryManager.instance.deliveryRecipe(plateKitchenObject);
                player.GetKitchenObject().destroySelf();
            }
        }
    }
}
