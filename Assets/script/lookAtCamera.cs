using UnityEngine;

public class lookAtCamera : MonoBehaviour
{

    private enum Mode
    {
        LookAt,
        lookAtInverted,
    }
    [SerializeField] private Mode mode;
    void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.lookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
        }
        
    }
}
