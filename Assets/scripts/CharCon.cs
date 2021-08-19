using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharCon : MonoBehaviour
{
    public Vector2 scr;
    private void OnGUI() //renders gui elements
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        if (GUI.Button(new Rect(1f * scr.x, 1 * scr.y, 1 * scr.x, 1 * scr.y), "Back"))
        {
            SceneManager.LoadScene(0);
        }

        if(GUI.Button(new Rect(14 * scr.x, 1 * scr.y, 1 * scr.x, 1 * scr.y), "Next"))
        {
            SceneManager.LoadScene(2);
        }
    }
}
