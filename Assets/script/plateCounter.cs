using System;
using UnityEngine;

public class plateCounter : baseCounter
{
    public event EventHandler onPlateSpawned;
    public event EventHandler onPlateRemoved;

    [SerializeField] private kitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimeMax = 4;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;
    void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimeMax)
        {
            spawnPlateTimer = 0;


            //kitchenObject.spawnKitchenObject(plateKitchenObjectSO, this);

            if (kitchenGameManager.instance.isGamePlaying() && plateSpawnAmount < plateSpawnAmountMax)
            {
                plateSpawnAmount++;
                onPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void interAct(player player)
    {
        //base.interAct(player);
        if (!player.hasKitchenObject())
        {
            //empty handed
            if (plateSpawnAmount > 0)
            {
                plateSpawnAmount--;
                kitchenObject.spawnKitchenObject(plateKitchenObjectSO, player);

                onPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    }

