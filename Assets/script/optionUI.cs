using TMPro;
using Unity.Mathematics;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class optionUI : MonoBehaviour
{
    public static optionUI instance { get; private set; }
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button interactAlternateButton;


    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private Transform pressToRebindKeyTreansform;


    void Awake()
    {
        instance = this;
        soundEffectButton.onClick.AddListener(() =>
        {
            soundManager.instance.changeVolume();
            updateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            musicManager.instance.changeVolume();
            updateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            hide();
        });

        moveUpButton.onClick.AddListener(() =>
        {
            rebinding(gameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
       {
           rebinding(gameInput.Binding.Move_Down);
       });
        moveLeftButton.onClick.AddListener(() =>
       {
           rebinding(gameInput.Binding.Move_Left);
       });
        moveRightButton.onClick.AddListener(() =>
       {
           rebinding(gameInput.Binding.Move_Right);
       });
        interactButton.onClick.AddListener(() =>
       {
           rebinding(gameInput.Binding.Interact);
       });
        interactAlternateButton.onClick.AddListener(() =>
       {
           rebinding(gameInput.Binding.InteractAlternate);
       });
        pauseButton.onClick.AddListener(() =>
        {
            rebinding(gameInput.Binding.Pause);
        });
        
    }
    void Start()
    {
        kitchenGameManager.instance.onGameUnpause += kitchenGameManager_onGameUnpause;
        updateVisual();
        hidePressToRebindKey();
        hide();
    }

    void kitchenGameManager_onGameUnpause(object sender, System.EventArgs e)
    {
        hide();
    }

    void updateVisual()
    {
        soundEffectText.text = "Sound : " + Mathf.Round(soundManager.instance.getVolume() * 10);
        musicText.text = "Music : " + Mathf.Round(musicManager.instance.getVolume() * 10);

        moveUpText.text = gameInput.instance.getBindingText(gameInput.Binding.Move_Up);
        moveDownText.text = gameInput.instance.getBindingText(gameInput.Binding.Move_Down);
        moveLeftText.text = gameInput.instance.getBindingText(gameInput.Binding.Move_Left);
        moveRightText.text = gameInput.instance.getBindingText(gameInput.Binding.Move_Right);
        interactText.text = gameInput.instance.getBindingText(gameInput.Binding.Interact);
        interactAlternateText.text = gameInput.instance.getBindingText(gameInput.Binding.InteractAlternate);
        pauseText.text = gameInput.instance.getBindingText(gameInput.Binding.Pause);
    }

    public void show()
    {
        gameObject.SetActive(true);
    }

    private void hide()
    {
        gameObject.SetActive(false);
    }

    private void showPressToRebindKey()
    {
        pressToRebindKeyTreansform.gameObject.SetActive(true);
    }

    private void hidePressToRebindKey()
    {
        pressToRebindKeyTreansform.gameObject.SetActive(false);
    }

    void rebinding(gameInput.Binding binding)
    {
        showPressToRebindKey();
        gameInput.instance.rebinding(binding,() =>
        {
            hidePressToRebindKey();
            updateVisual();
        } );
    }
}
