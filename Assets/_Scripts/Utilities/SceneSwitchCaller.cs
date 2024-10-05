using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchCaller : MonoBehaviour
{
    public void ChangeSceneButton(string sceneName)
    {
        SceneSwitcher.Instance.ChangeSceneButton(sceneName);
    }
}
