using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine.UI;

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
        
        #region Audio
        public AudioMixer masterAudio; //link to our mixer
        public string sliderName; //channel name we're changing

        public void ChangeVolumeName(string name)
        {
            sliderName = name;
        }

        public void ChangeVolumeValue(float volumeValue)
        {
            masterAudio.SetFloat(sliderName,volumeValue);
        }
        #endregion

        public void Quality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        #region Resolution
        public Resolution[] resOfComp;
        public Dropdown resDropdown;
        public void Start() //sets up the dropdown to display the computers resolutions
        {
            if (resDropdown != null)
            {
                resOfComp = Screen.resolutions;
                resDropdown.ClearOptions();

                List<string> resOptions = new List<string>();
                int currentScreenRes = 0;

                for (int i = 0; i < resOfComp.Length; i++)
                {
                    string option = resOfComp[i].width + "x" + resOfComp[i].height;
                    resOptions.Add(option);
                    if (resOfComp[i].width == Screen.currentResolution.width && resOfComp[i].height == Screen.currentResolution.height)
                    {
                        currentScreenRes = i;
                    }
                }
                resDropdown.AddOptions(resOptions);
                resDropdown.value = currentScreenRes;
                resDropdown.RefreshShownValue();
            }
            else
            {
                Debug.LogWarning("Attach the dropdown fool :/");
            }
        }
        
        public void SetResolution(int resolutionIndex)
        {
            Resolution res = resOfComp[resolutionIndex];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }
        #endregion

        #region Cursor
        public Texture2D[] cursor;
        private void ChangeCursor(int selectedCursor)
        {
            Cursor.SetCursor(cursor[selectedCursor],Vector2.zero,CursorMode.Auto);
        }
        #endregion
    }
}
