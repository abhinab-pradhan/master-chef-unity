//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class progressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGameObject;
    private iHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<iHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError("game object "+ hasProgressGameObject+"doesnt have a component that implement ihasprogress");
        }
        hasProgress.onProgressChange += hasProgress_onProgressChange;
        barImage.fillAmount = 0f;
        hide();
    }
    void hasProgress_onProgressChange(object sender, iHasProgress.onProgressChangeEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            hide();
        }
        else
        {
            show();
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
