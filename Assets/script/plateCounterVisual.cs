using System.Collections.Generic;
using UnityEngine;

public class plateCounterVisual : MonoBehaviour
{
    [SerializeField] private plateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    private List<GameObject> plateVisualGameObjectList;

    void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    void Start()
    {
        plateCounter.onPlateSpawned += plateCounter_onPlateSpawned;
        plateCounter.onPlateRemoved += plateCounter_onPlateRemoved;
    }
    void plateCounter_onPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
    void plateCounter_onPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }
}
