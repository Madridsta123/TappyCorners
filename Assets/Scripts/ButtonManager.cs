using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public string nextScene;
    public Button playButton;
    public Button quitButton;

    private void Start()
    {
        playButton.GetComponent<Button>().onClick.AddListener(PlayGame);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    public void PlayGame()
    {
        ChangeScene.LoadScene(nextScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
