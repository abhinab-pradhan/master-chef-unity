using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class deliveryResultUI : MonoBehaviour
{

    private const string POPUP = "popUp";
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        deliveryManager.instance.onRecipeSuccess += deliveryManager_onRecipeSuccess;
        deliveryManager.instance.onRecipeFailed += deliveryManager_onRecipeFailed;

        gameObject.SetActive(false);
    }

    void deliveryManager_onRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }
    void deliveryManager_onRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failColor;
        iconImage.sprite = failSprite;
        messageText.text = "DELIVERY\nFAILED";
    }
}
