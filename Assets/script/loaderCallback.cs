using UnityEngine;

public class loaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            loader.loaderCallback();
        }
    }
}
