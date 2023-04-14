using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        }    
        else {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LevelCompleted() {
        print("You completed the level!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame() {
        SceneManager.LoadScene("Level1");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
