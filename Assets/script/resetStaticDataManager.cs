using UnityEngine;

public class resetStaticDataManager : MonoBehaviour
{
    void Awake()
    {
        cuttingCounter.resetStaticData();
        baseCounter.resetStaticData();
        trashCounter.resetStaticData();
    }
}
