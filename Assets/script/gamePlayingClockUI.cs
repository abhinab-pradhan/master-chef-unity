using UnityEngine;
using UnityEngine.UI;

public class gamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    void Update()
    {
        timerImage.fillAmount = kitchenGameManager.instance.getGamePlayingTimerNormalize();
    }
}
