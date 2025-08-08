using TMPro;
using UnityEngine;

public class tutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyMoveInteractText;
    [SerializeField] private TextMeshProUGUI keyMoveInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyPauseText;

    void Start()
    {
        gameInput.instance.onBindingRebind += gameInput_onBindingRebind;
        kitchenGameManager.instance.onStateChange += kitchenGameManager_onStateChange;
        updateVisual();
        show();
    }

    void kitchenGameManager_onStateChange(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.instance.isCountDownToStartActive())
        {
            hide();
        }
    }

    void gameInput_onBindingRebind(object sender, System.EventArgs e)
    {
        updateVisual();
    }

    void updateVisual()
    {
        keyMoveUpText.text = gameInput.instance.getBindingText(gameInput.Binding.Move_Up);
        keyMoveDownText.text = gameInput.instance.getBindingText(gameInput.Binding.Move_Down);
        keyMoveLeftText.text = gameInput.instance.getBindingText(gameInput.Binding.Move_Left);
        keyMoveRightText.text = gameInput.instance.getBindingText(gameInput.Binding.Move_Right);
        keyMoveInteractText.text = gameInput.instance.getBindingText(gameInput.Binding.Interact);
        keyMoveInteractAlternateText.text = gameInput.instance.getBindingText(gameInput.Binding.InteractAlternate);
        keyPauseText.text = gameInput.instance.getBindingText(gameInput.Binding.Pause);

    }

    void show()
    {
        gameObject.SetActive(true);
    }
    void hide()
    {
        gameObject.SetActive(false);
    }

}
