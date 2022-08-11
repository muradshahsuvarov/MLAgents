using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OpenBallBalaner()
    {
        SceneManager.LoadScene("BallBalancer");
    }

    public void OpenBallFollower()
    {
        SceneManager.LoadScene("BallFollower");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
