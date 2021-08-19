using System;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement; //this is added to get the Unity scene management library
using UnityEngine.Audio;

public class IMGUIScript : MonoBehaviour
{
    [Header("Screen Display")]
    public static Vector2 scr;
    public bool showOptions;
    
    [Header("Audio")]
    [Tooltip("Reference to Unity's audio mixer")]
    public AudioMixer audi;
    [Range(-80,20)]
    public float volumeMaster, volumeMusic, volumeSFX;
    [Tooltip("Reference to our audio source prefab")]
    public GameObject music;
    public string mute;
    public string buttonName;
    public bool isMuted;

    [Header("Options Tabs")]
    public string[] idList;
    public int currentOption;

    [Header("Resolution")]
    public Resolution[] resolutions;
    public string[] resolutionName;
    public bool showResOptions;
    public string resDropDownLabel = "Resolution";
    public string fullScreenToggleName;
    public Vector2 scrollPosition = Vector2.zero;

  
    [Header("Keybind Setup")]
    public static Dictionary<string, KeyCode> inputKeys = new Dictionary<string, KeyCode>(); // this requires the system.collections.generic 
    [Serializable]
    public struct KeySetup
    {
        public string keyName;
        public string defaultKey;
        public string tempKey;
    }
    
    [Header("Keybinds")]
    public KeySetup[] keySetUp;
    [Tooltip("this doesn't get filled by us, it helps work out what key is selected")]
    public KeySetup currentKey;


    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Music"))
        {
            Instantiate(music);
        }

        #region Resolution
        // grab all the resolutions on our screen and add them to a list
        resolutions = Screen.resolutions;
        // set the size of all our array names to the length of our resoltion array
        resolutionName = new string[resolutions.Length];
        // for every resolution create the display name
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionName[i] = resolutions[i].width + " x " + resolutions[i].height;
        }
        #endregion

        #region KeyBinds
        //For loop to add the keys to the dictionary with sae or default depending on load
        for (int i = 0; i < keySetUp.Length; i++)    // for all the keys in our base set up array
        {
            // add key according to the saved string or default
            inputKeys.Add(keySetUp[i].keyName,(KeyCode)Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString(keySetUp[i].keyName,keySetUp[i].defaultKey)));
            //parse method is used to convert the string representation of 1 data type to another. turning into a string is easier than the reverse
            //Int32.Parse = int into its 32 bits
            //Enum.Parse = enum 

            Debug.Log(keySetUp[i].keyName + ": " + keySetUp[i].defaultKey);
            scrollPosition.x.ToString();
        }

        #endregion
    }

    void SaveKey()
    {
        foreach (var key in inputKeys)
        {
            PlayerPrefs.SetString(key.Key,key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    private void OnGUI() //renders gui elements
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        AspectRatioGrid(); //comment this out to turn off grid

        if (showOptions) //if showOptions is true
        {
            //display our options
            LayoutOptionsScreen();
        }
        else//if options is false
        {
            //display our menu
            LayoutMenuScreen();
        }
    }

    void AspectRatioGrid()
    {
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                //telling this 'I am a GUI element'
                //type Box
                //new Poz x,y new Size x,y
                //my content
                GUI.Box(new Rect(x * scr.x, y * scr.y, scr.x, scr.y),""); //this is a gui element
                
                GUI.Label(new Rect(x * scr.x, y * scr.y, scr.x, scr.y), ""); //replacing "" with x + "," + y will put the xy coords on each square
            }
        }
    }

    void LayoutMenuScreen()
    {
        //Background
        GUI.Box(new Rect(1*scr.x, 1*scr.y, 14*scr.x, 7*scr.y), "");

        //Title
        GUI.Box(new Rect(2*scr.x, 2*scr.y, 12*scr.x, 1*scr.y), "Title");

        //Play - this button allows us to start the game, changes scenes
        if(GUI.Button(new Rect(2.5f * scr.x, 4 * scr.y, 5*scr.x, 1*scr.y), "Play"))
        {
            SceneManager.LoadScene(1); //scenes can be loaded either with names (strings) or id numbers
        }

        //Options
        if(GUI.Button(new Rect(8.5f * scr.x, 4 * scr.y, 5*scr.x, 1*scr.y), "Options"))
        {
            showOptions = true;
        }

        //Exit
        if(GUI.Button(new Rect(5.5f * scr.x, 6 * scr.y, 5 * scr.x, 1 * scr.y), "Exit"))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //makes unity look like it closes - dev code
            #endif
            Application.Quit(); //this will not quit unity, but the application itself. so during testing it wont quit
        }

    }

    void LayoutOptionsScreen()
    {
        //Background
        GUI.Box(new Rect(1 * scr.x, 1 * scr.y, 14 * scr.x, 7 * scr.y), "");
        //Title
        GUI.Box(new Rect(2 * scr.x, 2 * scr.y, 12 * scr.x, 1 * scr.y), "Options");

        #region Forloop Buttons

        /* for loop explanation 
         
               for - iterates a set number of times 
                   - needs to know it size or amount of iterations
        
        int i = 0; this part is creating an iteration reference variable aka we can start at any number we want.
                   standard is 0 for counting up; we put the size max for counting down

            i < 0; this is the amoutn of iterations we can reach - this is our size or amount
                   counting up we say < max value
                   counting down we say > 0

               i++ go to next iteration - count up
               i-- go to the next iteration - count down

        */

        for (int buttonIndexNumber = 0; buttonIndexNumber < idList.Length; buttonIndexNumber++)
        {
            if(GUI.Button(new Rect(2*scr.x+(buttonIndexNumber*3*scr.x), 3*scr.y, 3*scr.x, 0.5f* scr.y),idList[buttonIndexNumber]))
            {
                currentOption = buttonIndexNumber;
                scrollPosition = Vector2.zero;
            }
        }
        switch (currentOption)
        {
            case 0:
                #region Audio
                audi.SetFloat("VolumeMaster", volumeMaster = GUI.HorizontalSlider(new Rect(2 * scr.x, 4 * scr.y, 2 * scr.x, 0.25f * scr.y), volumeMaster, -80, 20));
                audi.SetFloat("VolumeMusic", volumeMusic = GUI.HorizontalSlider(new Rect(2 * scr.x, 4.5f * scr.y, 2 * scr.x, 0.25f * scr.y), volumeMusic, -80, 20));
                audi.SetFloat("VolumeSFX", volumeSFX = GUI.HorizontalSlider(new Rect(2 * scr.x, 5 * scr.y, 2 * scr.x, 0.25f * scr.y), volumeSFX, -80, 20));
                //mute = GUI.Toggle(new Rect(6 * scr.x, 4 * scr.y, 2 * scr.x, 0.25f * scr.y), mute, "");
                //AudioListener.pause = mute;
                //mute button
                if (GUI.Button(new Rect(6 * scr.x, 4 * scr.y, 2 * scr.x, 0.25f * scr.y), buttonName)) 
                {
                    isMuted = !isMuted;
                    if (isMuted)
                    {
                        buttonName = "Unmute";
                        audi.SetFloat("VolumeMaster", -80);
                    }
                    else
                    {
                        buttonName = "Mute";
                        audi.SetFloat("VolumeMaster", 0);
                    }
                }
                #endregion
                break;
            case 1:
                #region Resolution Settings
                if (GUI.Button(new Rect(2*scr.x,3.5f*scr.y, 3*scr.x, 1*scr.y),resDropDownLabel))
                {
                    showResOptions = !showResOptions;
                    //if true, become false - if false, become true
                }
                if (showResOptions)
                {
                    //create a background
                    GUI.Box(new Rect(2 * scr.x, 4.5f * scr.y, 3 * scr.x, 2 * scr.y),"");
                    
                    //create a scroll view
                    scrollPosition = GUI.BeginScrollView(new Rect(2 * scr.x, 4.5f * scr.y, 3 * scr.x, 2 * scr.y),scrollPosition,new Rect(0,0,0,0.5f*scr.y*resolutions.Length),false,true);
                    
                    //fill scroll view with buttons
                    for (int i = 0; i < resolutions.Length; i++)
                    {
                        //for every element create a button according to our arrays
                        if (GUI.Button(new Rect(0,i*0.5f*scr.y, 3*scr.x, 0.75f*scr.y), resolutionName[i]))
                        {
                            //set our resolution to the selected resolution
                            Screen.SetResolution(resolutions[i].width, resolutions[i].height, Screen.fullScreen);
                            //close dropdown
                            showResOptions = false;
                        }
                    }
                    //end scroll view
                    GUI.EndScrollView();
                }
                //fullscreen button
                if (GUI.Button(new Rect(6 * scr.x, 4 * scr.y, 3 * scr.x, 1 * scr.y), fullScreenToggleName))
                {
                    Screen.fullScreen = !Screen.fullScreen;
                    if (Screen.fullScreen) //this just changes the button name to full screen or windowed
                    {
                        fullScreenToggleName = "Fullscreen";
                    }
                    else
                    {
                        fullScreenToggleName = "Windowed";
                    }
                }

                #endregion
                break;
            case 2:
                #region Keybindings
                GUI.Box(new Rect(6 * scr.x, 4 * scr.y, 3 * scr.x, 1 * scr.y), currentKey.keyName);

                for (int i = 0; i < keySetUp.Length; i++)//when we press the button...
                {
                    //the current key is then set to the key we pressed 
                    if (GUI.Button(new Rect(2 *scr.x, (3*scr.y) + (i *1* scr.y), 3*scr.x, 1 * scr.y), keySetUp[i].keyName + " " + inputKeys[keySetUp[i].keyName])) 
                    {
                        currentKey.keyName = keySetUp[i].keyName;

                        
                    }
                }
                if (currentKey.keyName != null) //now we have clicked a button, then it will run the keybinds 
                {
                    KeyBinds();
                }
                #endregion
                break;
            case 3:

                break;
            default:
                currentOption = 0;
                break;
        }
        #endregion

        if (GUI.Button(new Rect(5.5f * scr.x, 7 * scr.y, 5 * scr.x, 1 * scr.y), "Back"))
        {
            showOptions = false;
            SaveKey();
        }
    }

    void KeyBinds()
    {
        string newKey = "";
        Event e = Event.current;
        if(currentKey.keyName != null) //if you have pressed a key (if the key is not nothing)...
        {
            if (e.isKey) //the input should be changed 
            {
                newKey = e.keyCode.ToString();
            }
            if (Input.GetKey(KeyCode.LeftShift)) //these are here because left and right shift dont work naturally ??
            {
                newKey = "LeftShift";
            }
            if (Input.GetKey(KeyCode.RightShift))
            {
                newKey = "RightShift";
            }
            if (newKey != "") //if our key isn't empty
            {
                inputKeys[currentKey.keyName] = (KeyCode)Enum.Parse(typeof(KeyCode), newKey);
                //the above changes our key in the dictionary to the key we just pressed - it assings a new keycode
                
                Debug.Log(currentKey.keyName + ": " + newKey); // this tells us if it worked
                
                currentKey.keyName = null; //reset the key and wait
            }
        }
    }
}

