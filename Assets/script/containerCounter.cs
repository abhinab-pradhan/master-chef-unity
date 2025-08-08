using System;
using UnityEngine;

public class containerCounter : baseCounter
{
    public event EventHandler onPlayerGrabbedObject;
    [SerializeField] private kitchenObjectSO kitchenObjectSO;
    //[SerializeField] private Transform counterTopPoint;


    //private kitchenObject kitchenObject;
    public override void interAct(player player)
    {
        if (!player.hasKitchenObject())
        {
            //player not carrying anything
            kitchenObject.spawnKitchenObject(kitchenObjectSO, player);

            onPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        // Debug.Log("interact");
        
        
    }

}
