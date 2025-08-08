using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            //SceneManager.LoadScene(1);
            loader.load(loader.Scene.GameScence);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1;
    }

    
}
