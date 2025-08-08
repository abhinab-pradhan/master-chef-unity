using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class stoveBurnFlashingBarUI : MonoBehaviour
{
    private Animator animator;
    private const string is_flashing = "isFlashing";
    [SerializeField] private stoveCounter stoveCounter;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        stoveCounter.onProgressChange += stoveCounter_onProgressChange;
        animator.SetBool(is_flashing, false);
    }

    void stoveCounter_onProgressChange(object sender, iHasProgress.onProgressChangeEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = stoveCounter.isFried() && e.progressNormalized >= burnShowProgressAmount;

        animator.SetBool(is_flashing, show);
    }

}
