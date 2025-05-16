using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class btns : MonoBehaviour
{
   

    // تابع برای رفتن به سین Home
    public void GoToHome()
    {
        SceneManager.LoadScene("menue");
    }

    // تابع برای ری‌استارت کردن سین گیم‌پلی
    public void RestartGameplay()
    {
        SceneManager.LoadScene("game");
        Time.timeScale = 1 ;
    }
}
