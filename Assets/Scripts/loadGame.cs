
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadGame : MonoBehaviour
{
    public void QuitButton()
    {
        Application.Quit();


    }



    public void StartGame()
    {
        SceneManager.LoadScene("levelOne");
    }

}
