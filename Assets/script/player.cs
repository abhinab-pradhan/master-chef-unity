using System;
using UnityEngine;
using UnityEngine.Rendering;

public class player : MonoBehaviour, IKitchenObjectParent
{
    // private static player instance;
    public static player Instance { get; private set; }

    public event EventHandler onPickSomething;



    public event EventHandler<onSelectedCounterChangeEventArgs> onSelectedCounterChange;

    public class onSelectedCounterChangeEventArgs : EventArgs
    {
        public baseCounter selectedCounter;
    }
    private bool isWalking;
    private Vector3 lastInteractDir;
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private gameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private baseCounter selectedCounter;
    private kitchenObject kitchenObject;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("more player instance ");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += gameInput_onInteractAction;
        gameInput.OnInteractAlternateAction += gameInput_onInteractAlternateAction;
    }
    private void gameInput_onInteractAlternateAction(object sender, EventArgs e)
    {
        if (!kitchenGameManager.instance.isGamePlaying()) return;
        //throw new NotImplementedException();
        if (selectedCounter != null)
        {
            selectedCounter.interActAlternate(this);
        }
    }
    private void gameInput_onInteractAction(object sender, System.EventArgs e)
    {
        if (!kitchenGameManager.instance.isGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.interAct(this);
        }
    }
    void Update()
    {
        handleMovement();
        handleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    void handleMovement()
    {
        Vector2 inputVector = gameInput.getMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float playerRadius = .7f;
        float playerHeight = 2;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove =moveDir.x!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove =moveDir.z!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    void handleInteraction()
    {
        Vector2 inputVector = gameInput.getMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2;
        Vector3 rayOrigin = transform.position + Vector3.up * 1f;

        if (Physics.Raycast(rayOrigin, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out baseCounter baseCounter))
            {
                //has clear counter
                if (baseCounter != selectedCounter)
                {
                    setSelectedCounter(baseCounter);
                }
            }
            else
            {
                setSelectedCounter(null);
            }
        }
        else
        {
            setSelectedCounter(null);
        }
        //  Debug.Log(selectedCounter);
    }

    void setSelectedCounter(baseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        onSelectedCounterChange?.Invoke(this, new onSelectedCounterChangeEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
     public Transform getKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void setKitchenObject(kitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            onPickSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public kitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void clearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool hasKitchenObject()
    {
        return kitchenObject != null;
    }
}


