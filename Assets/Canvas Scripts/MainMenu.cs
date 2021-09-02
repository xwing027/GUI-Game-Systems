using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace CanvasExample
{ 
    public class MainMenu : MonoBehaviour
    {
        public void ChangeScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void ExitGameToDesktop()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        }
    }
}
