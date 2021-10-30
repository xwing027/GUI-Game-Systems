using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Soy Sauce/Player Scripts/Player Interaction")]

public class Interact : MonoBehaviour
{
    void Update()
    {
        //if our interact is pressed
        if (Input.GetKeyDown(IMGUIScript.inputKeys["Interact"]))
        {
            //create ray
            Ray interactRay; //this is our line, at this point it has purpose (origin or direction)
            
            //assigning origin
            interactRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            // ^ this ray is shooting out from the main camera's screen point centre of screen

            //create hit info
            RaycastHit hitInfo;
            
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interactRay, out hitInfo, 10))
            {
                #region NPC
                //if the collider we hit is tagged NPC
                if (hitInfo.collider.tag == "NPC")
                {
                    //debug that we hit an NPC
                    Debug.Log("NPC");
                    if (hitInfo.collider.gameObject.GetComponent<LinearDialogue>())
                    {
                        hitInfo.collider.gameObject.GetComponent<LinearDialogue>().showDlg = true;
                    }
                }
                #endregion

                #region Item
                //if the collider we hit is tagged Item
                if (hitInfo.collider.CompareTag("Item")) //this does the same as the above, just a different way to write it
                {
                    //debug that we hit an Item
                    Debug.Log("Item");
                    ItemHandler handler = hitInfo.transform.GetComponent<ItemHandler>();
                    if (handler != null)
                    {
                        handler.OnCollection();
                    }
                }
                #endregion

                #region Chest
                //if the collider we hit is tagged Item
                if (hitInfo.collider.CompareTag("Chest")) //this does the same as the above, just a different way to write it
                {
                    //debug that we hit an Item
                    Debug.Log("Item");
                    Chest currentChest = hitInfo.transform.GetComponent<Chest>();
                    if (currentChest != null)
                    {
                        currentChest.showChest = !currentChest.showChest;
                    }
                }
                #endregion
            }
        }
    }
}
