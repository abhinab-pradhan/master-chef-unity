using System;
using UnityEditor.Analytics;
using UnityEngine;

public class trashCounter : baseCounter
{
    public static event EventHandler onANyObjectTrashed;

   new public static void resetStaticData()
    {
        onANyObjectTrashed = null;
    }
    public override void interAct(player player)
    {
        if (player.hasKitchenObject())
        {
            player.GetKitchenObject().destroySelf();
            onANyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
