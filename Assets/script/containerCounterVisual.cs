using UnityEngine;

public class containerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private containerCounter containerCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        containerCounter.onPlayerGrabbedObject += containerCounter_onPlayerGrabbedObject;
    }
    void containerCounter_onPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
