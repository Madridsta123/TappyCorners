using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainScene : MonoBehaviour
{
    public string sceneName;

    private void Awake()
    {
        ChangeScene.LoadScene(sceneName);
    }
}
