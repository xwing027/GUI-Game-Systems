using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
