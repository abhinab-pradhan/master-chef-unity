using TMPro;
using UnityEditor.Search;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class gameStartCountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    private Animator animator;
    private int prevCountDownNumber;

    private const string number_popUp = "numberPopUp";
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        kitchenGameManager.instance.onStateChange += kitchenGameManager_onStateChange;
        hide();
    }

    void kitchenGameManager_onStateChange(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.instance.isCountDownToStartActive())
        {
            show();
        }
        else
        {
            hide();
        }
    }

    void Update()
    {
        int countDown = Mathf.CeilToInt(kitchenGameManager.instance.getCountDownToStartTimer());
        countDownText.text = countDown.ToString();

        if (prevCountDownNumber != countDown)
        {
            prevCountDownNumber = countDown;
            animator.SetTrigger(number_popUp);
            soundManager.instance.playCountDownSound();
        }
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
