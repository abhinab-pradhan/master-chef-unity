using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class stoveCounterSound : MonoBehaviour
{
    [SerializeField] private stoveCounter stoveCounter;
    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        stoveCounter.onStateChange += stoveCounter_onStateChange;
        stoveCounter.onProgressChange += stoveCounter_onProgressChange;
    }

    void stoveCounter_onProgressChange(object sender, iHasProgress.onProgressChangeEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        playWarningSound = stoveCounter.isFried() && e.progressNormalized >= burnShowProgressAmount;
    }
    void stoveCounter_onStateChange(object sender, stoveCounter.onStateChangeEventArgs e)
    {
        bool playeSound = e.state == stoveCounter.State.Frying || e.state == stoveCounter.State.Fried;
        if (playeSound)
        {
            audioSource.Play();

        }
        else
        {
            audioSource.Pause();
        }


    }

    void Update()
    {
        if (playWarningSound)
        {


            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                soundManager.instance.playWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
