using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");   // ชื่อ scene เกมของคุณ
    }

    public void HowToPlay()
    {
        Debug.Log("How To Play clicked");  // เอาไว้ debug ก่อน
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
    }
}
