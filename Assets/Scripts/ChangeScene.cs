using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ChangeScene 
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void LoadScene(string index)
    {
        SceneManager.LoadScene(index);
    }
}
