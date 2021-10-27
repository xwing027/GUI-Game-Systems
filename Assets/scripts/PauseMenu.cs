using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour
{
#if UNITY_EDITOR
    [Serializable]
    public struct KeySetup
    {
        public string keyName;
        public string defaultKey;
        public string tempKey;
    }
    [Header("Keybinds")]
    public KeySetup[] keySetUp;
#endif

    public static bool isPaused;

    void Awake()
    {
#if UNITY_EDITOR
        //set up for this scene the scr reference for 16:9
        IMGUIScript.scr.x = Screen.width / 16;
        IMGUIScript.scr.y = Screen.height / 9;

        //if we dont have an entry in our inputKeys dictionary
        if (IMGUIScript.inputKeys.Count <= 0)
        {
            //loop through and set up our keys acoording to baseSetup
            for (int i = 0; i < keySetUp.Length; i++)    // for all the keys in our base set up array
            {
                IMGUIScript.inputKeys.Add(keySetUp[i].keyName, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keySetUp[i].keyName, keySetUp[i].defaultKey)));

                Debug.Log(keySetUp[i].keyName + ": " + keySetUp[i].defaultKey);
            }
        }
        //our dictionary doesnt have a key for forward

        //for loop to add the keys to the dictionary with our save or default depending on load
        //for all the keys in our base set up an array
        //IMGUIScript.KeySetup[] currentKey;
        //add key according to the saved string or default
#endif
        UnPaused();
    }

    void Paused() //when Paused is triggered
    {
        //stop time
        Time.timeScale = 0;
        //free our cursor
        Cursor.lockState = CursorLockMode.Confined;
        //see our cursor
        Cursor.visible = true;
    }

    public void UnPaused() //when UnPaused is triggered
    {
        //start time
        Time.timeScale = 1;
        //lock our cursor 
        Cursor.lockState = CursorLockMode.Locked;
        //hide our cursor
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Paused();
                isPaused = true;
            }
            else
            {
                if (Inventory.showInv)
                {
                    UnPaused();
                }
                isPaused = false;
            }

        }
        
    }

    private void OnGUI()
    {
        if (isPaused)
        {
            MenuLayout();
        }
    }

    void MenuLayout()
    {
        //background
        GUI.Box(new Rect(1 * IMGUIScript.scr.x, 1 * IMGUIScript.scr.y, 14 * IMGUIScript.scr.x, 7 * IMGUIScript.scr.y), "");
        
        //title
        GUI.Box(new Rect(2 * IMGUIScript.scr.x, 2 * IMGUIScript.scr.y, 12 * IMGUIScript.scr.x, 1 * IMGUIScript.scr.y), "Paused");

        //return if gui button on screen is pressed
        if (GUI.Button(new Rect(2.5f * IMGUIScript.scr.x, 4 * IMGUIScript.scr.y, 5 * IMGUIScript.scr.x, 1 * IMGUIScript.scr.y), "Return"))
        {
            if (!Inventory.showInv)
            {
                UnPaused();
            }
            isPaused = false;
        }
        
        //main menu
        if (GUI.Button(new Rect(8.5f * IMGUIScript.scr.x, 4 * IMGUIScript.scr.y, 5 * IMGUIScript.scr.x, 1 * IMGUIScript.scr.y), "Main Menu"))
        {
            Time.timeScale = 1f;
            isPaused = false;
            //change scene
            SceneManager.LoadScene(0);
        }
        
        //exit
        if (GUI.Button(new Rect(5.5f * IMGUIScript.scr.x, 6 * IMGUIScript.scr.y, 5 * IMGUIScript.scr.x, 1 * IMGUIScript.scr.y), "Exit"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //makes unity look like it closes - dev code
#endif
            Application.Quit(); //this will not quit unity, but the application itself. so during testing it wont quit
        }
    }
}
