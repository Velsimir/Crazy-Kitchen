using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class LoadingScreen
{
    private static Scene _targetScene;

    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene
    }

    public static void Load(Scene targetScene)
    {
        _targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(_targetScene.ToString());

    }
}
