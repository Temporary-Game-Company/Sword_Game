using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour
{
    public static Vector2 screen_middle;

    void Start() {
        screen_middle = new Vector2(Screen.width/2, Screen.height/2);
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }
    public void Victory() {
        SceneManager.LoadScene("Victory");
    }
}
