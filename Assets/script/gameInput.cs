using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class gameInput : MonoBehaviour
{

    private const string Player_pref_binding = "InputBinding";

    public static gameInput instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputAction playerInputActions;
    public event EventHandler onPauseAction;
    public event EventHandler onBindingRebind;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
    }
    void Awake()
    {
        instance = this;
        playerInputActions = new PlayerInputAction();

        if (PlayerPrefs.HasKey(Player_pref_binding))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Player_pref_binding));
        }
        playerInputActions.player.Enable();
        playerInputActions.player.interact.performed += Interact_performed;
        playerInputActions.player.interactAlternate.performed += InteractAlternate_performed;
        playerInputActions.player.pause.performed += pause_performed;

        //Debug.Log(getBindingText(Binding.Move_Up));


    }

    void OnDestroy()
    {
        playerInputActions.player.interact.performed -= Interact_performed;
        playerInputActions.player.interactAlternate.performed -= InteractAlternate_performed;
        playerInputActions.player.pause.performed -= pause_performed;

        playerInputActions.Dispose();
    }

    void pause_performed(InputAction.CallbackContext obj)
    {
        onPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        //throw new NotImplementedException();
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        // throw new System.NotImplementedException();
        //Debug.Log(obj);
        if (OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        }

    }



    public Vector2 getMovementVectorNormalized()
    {

        Vector2 inputVector = playerInputActions.player.move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        //Debug.Log(inputVector);
        return inputVector;
    }

    public string getBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Interact:
                return playerInputActions.player.interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputActions.player.interactAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.player.pause.bindings[0].ToDisplayString();
            case Binding.Move_Up:
                return playerInputActions.player.move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.player.move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.player.move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.player.move.bindings[4].ToDisplayString();

        }
    }

    public void rebinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.player.Disable();
        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.player.move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.player.move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.player.move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.player.move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.player.interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.player.interactAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.player.pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
        .OnComplete(callback =>
        {

            callback.Dispose();
            playerInputActions.player.Enable();
            onActionRebound();

            //playerInputActions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(Player_pref_binding, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            onBindingRebind?.Invoke(this, EventArgs.Empty);
        })
        .Start();
    }
}
