using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gamePauseUI : MonoBehaviour
{
    //public static optionUI instance { get; private set; }
    [SerializeField] private Button mainMenu;
    [SerializeField] private Button resume;
    [SerializeField] private Button optionButton;

    void Awake()
    {
        //instance = this;
        resume.onClick.AddListener(() =>
        {
            kitchenGameManager.instance.pauseGame();
        });
        mainMenu.onClick.AddListener(() =>
        {
            loader.load(loader.Scene.mainMenuScence);
        });
        optionButton.onClick.AddListener(() =>
        {
            optionUI.instance.show();
        });
    }

    void Start()
    {
        kitchenGameManager.instance.onGamePause += kitchenGameManager_onGamePause;
        kitchenGameManager.instance.onGameUnpause += kitchenGameManager_onGameUnpause;
        hide();
    }

    void kitchenGameManager_onGamePause(object sender, System.EventArgs e)
    {
        show();
    }
    void kitchenGameManager_onGameUnpause(object sender, System.EventArgs e) {
        hide();
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
