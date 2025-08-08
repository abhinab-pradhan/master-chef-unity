using UnityEngine;

public class cuttingCounterVisual1 : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] private cuttingCounter cuttingCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        cuttingCounter.onCut += cuttingCounter_onCut;

    }


    void cuttingCounter_onCut(object sender, System.EventArgs e)
    {
         animator.SetTrigger(CUT);
    }
}
