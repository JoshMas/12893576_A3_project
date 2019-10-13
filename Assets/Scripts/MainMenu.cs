using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadTetris()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadBossFight()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
