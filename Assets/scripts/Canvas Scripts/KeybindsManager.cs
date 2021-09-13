using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasExample
{
    public class KeybindsManager : MonoBehaviour
    {
        public static Dictionary<string, KeyCode> inputKeys = new Dictionary<string, KeyCode>();
        [Serializable]
        public struct KeyUISetup
        {
            public string keyName;
            public Text keyDisplayText;
            public string defaultKey;
            //can add a temp key, but figure out how to do it yourself
        }

        public KeyUISetup[] keySetup;
        public GameObject currentKey;
        public Color32 changedKey = new Color32(39, 171, 249, 255); //these two are debug colours, not necessary for the game
        public Color32 selectedKey = new Color32(239, 116, 36, 255);

        void Start()
        {
            if (inputKeys.Count <= 0)//if we have our keys, skip this step
            {
                for (int i = 0; i < keySetup.Length; i++)
                {
                    inputKeys.Add(keySetup[i].keyName, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keySetup[i].keyName, keySetup[i].defaultKey)));
                    keySetup[i].keyDisplayText.text = inputKeys[keySetup[i].keyName].ToString();
                }
            }
            
        }
        public void SaveKeys()
        {
            foreach (var key in inputKeys) //save key to player prefs
            {
                PlayerPrefs.SetString(key.Key, key.Value.ToString());
            }
            PlayerPrefs.Save();
        }

        public void ChangeKey(GameObject clickedKey)
        {
            currentKey = clickedKey; //urrent key is the clicked key
            if (clickedKey != null) //if we have clicked the key and it is selected
            {
                currentKey.GetComponent<Image>().color = selectedKey;
            }
        }

        public void OnGUI()
        {
            string newKey = "";
            Event e = Event.current;
            if (currentKey != null)
            {
                if (e.isKey)
                {
                    newKey = e.keyCode.ToString();
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    newKey = "LeftShift";
                }
                if (Input.GetKey(KeyCode.RightShift))
                {
                    newKey = "RightShift";
                }
                if (newKey != "") //if we have recorded a key
                {
                    inputKeys[currentKey.name] = (KeyCode)Enum.Parse(typeof(KeyCode), newKey);
                    // ^ changes out the key in the dictionary to the one we pressed
                    currentKey.GetComponentInChildren<Text>().text = newKey;
                    // ^ changes the display text to match teh new key
                    currentKey.GetComponent<Image>().color = changedKey; //colour change to show we changed it - debug
                    currentKey = null; //reset and wait
                }
            }
        }
    }
}
