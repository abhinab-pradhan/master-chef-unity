using UnityEngine;

public class stoveBurningUI : MonoBehaviour
{
    [SerializeField] private stoveCounter stoveCounter;

    void Start()
    {
        stoveCounter.onProgressChange += stoveCounter_onProgressChange;
        hide();
    }

    void stoveCounter_onProgressChange(object sender, iHasProgress.onProgressChangeEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.isFried() && e.progressNormalized >= burnShowProgressAmount;

        if (show)
        {
            Show();
        }
        else
        {
            hide();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }
    void hide()
    {
        gameObject.SetActive(false);
    }
}
