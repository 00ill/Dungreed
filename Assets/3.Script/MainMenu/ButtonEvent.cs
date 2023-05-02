using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonEvent : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
