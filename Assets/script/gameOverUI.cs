using System.Threading;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class gameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeDeliveredText;


    void Start()
    {
        kitchenGameManager.instance.onStateChange += kitchenGameManager_onStateChange;
        hide();
    }
    void kitchenGameManager_onStateChange(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.instance.isGameOver())
        {
            show();
            recipeDeliveredText.text = deliveryManager.instance.getSuccessfulRecipeAmount().ToString();

        }
        else
        {
            hide();
        }
    }

    void Update()
    {
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
