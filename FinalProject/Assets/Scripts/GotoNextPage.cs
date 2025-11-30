using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNextPage : MonoBehaviour
{
    public void GoToNextMap1()
    {
        SceneManager.LoadScene("GameplayScene"); 
    }
    public void GoToNextMap2()
    {
        SceneManager.LoadScene("GameplayScene2"); 
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
