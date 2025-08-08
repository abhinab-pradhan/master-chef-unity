using UnityEngine;

public class selectedCounterVisual : MonoBehaviour
{
    [SerializeField] private baseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    void Start()
    {
        player.Instance.onSelectedCounterChange += Player_OnSelectedCounterChanged;
    }
    private void Player_OnSelectedCounterChanged(object sender, player.onSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
        
    }
    void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
        
    }
}
