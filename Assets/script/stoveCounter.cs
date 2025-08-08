using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class stoveCounter : baseCounter, iHasProgress
{
    public event EventHandler<iHasProgress.onProgressChangeEventArgs> onProgressChange;
    public event EventHandler<onStateChangeEventArgs> onStateChange;
    public class onStateChangeEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    private State state;
    [SerializeField] private fryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private burningRecipeSO[] burningRecipeSOArray;
    private float fryingTimer;
    private float burningTimer;

    private fryingRecipeSO fryingRecipeSO;
    private burningRecipeSO burningRecipeSO;

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        if (hasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    onProgressChange?.Invoke(this, new iHasProgress.onProgressChangeEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        // fryingTimer = 0;
                        GetKitchenObject().destroySelf();

                        kitchenObject.spawnKitchenObject(fryingRecipeSO.output, this);
                        UnityEngine.Debug.Log("fried");
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        onStateChange?.Invoke(this, new onStateChangeEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    onProgressChange?.Invoke(this, new iHasProgress.onProgressChangeEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        // fryingTimer = 0;
                        GetKitchenObject().destroySelf();

                        kitchenObject.spawnKitchenObject(burningRecipeSO.output, this);
                        UnityEngine.Debug.Log("burned");
                        state = State.Burned;
                        onStateChange?.Invoke(this, new onStateChangeEventArgs
                        {
                            state = state
                        });

                        onProgressChange?.Invoke(this, new iHasProgress.onProgressChangeEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
            UnityEngine.Debug.Log(state);
        }
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
                    //player carrying something that can be fried
                    player.GetKitchenObject().setKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;

                    onStateChange?.Invoke(this, new onStateChangeEventArgs
                    {
                        state = state
                    });

                    onProgressChange?.Invoke(this, new iHasProgress.onProgressChangeEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
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
                        state = State.Idle;

                        onStateChange?.Invoke(this, new onStateChangeEventArgs
                        {
                            state = state
                        });

                        onProgressChange?.Invoke(this, new iHasProgress.onProgressChangeEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //player not carrying anything
                GetKitchenObject().setKitchenObjectParent(player);

                state = State.Idle;

                onStateChange?.Invoke(this, new onStateChangeEventArgs
                {
                    state = state
                });

                onProgressChange?.Invoke(this, new iHasProgress.onProgressChangeEventArgs
                {
                    progressNormalized = 0f
                });

            }
        }
    }

    private kitchenObjectSO getOutputForInput(kitchenObjectSO inputkitchenObjectSO)
    {
        fryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputkitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;

        }
    }

    private bool hasRecipeInput(kitchenObjectSO inputkitchenObjectSO)
    {
        fryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputkitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private fryingRecipeSO GetFryingRecipeSOWithInput(kitchenObjectSO InputkitchenObjectSO)
    {
        foreach (fryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == InputkitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private burningRecipeSO GetBurningRecipeSOWithInput(kitchenObjectSO InputkitchenObjectSO)
    {
        foreach (burningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == InputkitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

    public bool isFried()
    {
        return state == State.Fried;
    }
}
