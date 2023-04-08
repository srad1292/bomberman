using UnityEngine;

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
    }
}
