using UnityEngine;
using UnityEngine.SceneManagement;

public static class loader
{
    public enum Scene
    {
        mainMenuScence,
        GameScence,
        loadingScence
    }
    private static Scene targetScene;

    public static void load(Scene targetScene)
    {
        loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.loadingScence.ToString());
        
    }

    public static void loaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
