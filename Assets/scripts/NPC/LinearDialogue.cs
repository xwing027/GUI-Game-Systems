using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Soy Sauce/NPC Scripts/Linear Dialogue")]

public class LinearDialogue : MonoBehaviour
{
    #region Variables
    [Header("References")]
    //boolean to toggle if we can see a characters dialogue box
    public bool showDlg;
    //array for text for our dialogue
    public string[] dlgText;
    //index for our current line of dialogue
    public int index;
    //name of this specific npc
    public string charName;
    #endregion

    #region OnGUI
    private void OnGUI()
    {
        //if our dialogue can be seen on screen
        if (showDlg && dlgText.Length>0)
        {
            //dialogue box takes up bottom 1/3 of the screen and displays NPC name and dialogue
            GUI.Box(new Rect(0, IMGUIScript.scr.x*6, Screen.width, IMGUIScript.scr.x*3),charName+": "+dlgText[index]);

            //if not at the end of dialogue
            if (index < dlgText.Length-1)
            {
                //next button that allows us to select next dialogue
                if (GUI.Button(new Rect(IMGUIScript.scr.x * 15, IMGUIScript.scr.y*8.5f, IMGUIScript.scr.x, IMGUIScript.scr.y*0.5f),"Next"))
                {
                    index++;
                }
            }
            else
            {   
                //bye button shows up to end the dialogue
                if (GUI.Button(new Rect(IMGUIScript.scr.x * 15, IMGUIScript.scr.y * 8.5f, IMGUIScript.scr.x, IMGUIScript.scr.y * 0.5f), "Bye"))
                {
                    //close the dialogue box
                    showDlg = false;
                    //set index back to 0
                    index = 0;
                    //allow mouselook to be turned back on

                    //get the movement on the character and turn that back on
                    Time.timeScale = 1;
                    //lock the mouse cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    //set the cursor to being invisible
                    Cursor.visible = false;
                }
            }
        }
    }
    #endregion
}
