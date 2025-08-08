using UnityEngine;

public class stoveCounterVisual : MonoBehaviour
{
    [SerializeField] private stoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particleOnGameOnject;

    void Start()
    {
        stoveCounter.onStateChange += stoveCounter_onStateChange;
    }
    void stoveCounter_onStateChange(object sender, stoveCounter.onStateChangeEventArgs e)
    {
        bool showVisual = e.state == stoveCounter.State.Frying || e.state == stoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particleOnGameOnject.SetActive(showVisual);
    }
}
