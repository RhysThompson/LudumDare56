using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchCaller : MonoBehaviour
{
    public void ChangeSceneButton(string sceneName)
    {
        SceneSwitcher.Instance.ChangeSceneButton(sceneName);
    }
    public void RestartSceneButton()
    {
        SceneSwitcher.Instance.RestartScene();
    }
    public void MainMenuButton()
    {
        SceneSwitcher.Instance.ReturnToMenu();
    }
}
