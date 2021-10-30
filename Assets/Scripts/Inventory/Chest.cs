using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool showChest;
    public int[] itemsToSpawn;
    public List<Item> chestInv = new List<Item>();
    public Item selectedChestItem;

    public void Start()
    {
        itemsToSpawn = new int[Random.Range(1,11)];
        for (int i = 0; i < itemsToSpawn.Length; i++)
        {
            itemsToSpawn[i] = Random.Range(0,801);
            chestInv.Add(ItemData.CreateItem(itemsToSpawn[i]));
        }
    }
    private void OnGUI()
    {
        if (showChest)
        {
            for (int i = 0; i < chestInv.Count; i++)
            {
                if (GUI.Button(new Rect(4 * IMGUIScript.scr.x, IMGUIScript.scr.y * 4.25f +i*(IMGUIScript.scr.y*1f), IMGUIScript.scr.x * 2, IMGUIScript.scr.y * 1),chestInv[i].Name))
                {
                    selectedChestItem = chestInv[i];
                }
            }
            if (selectedChestItem != null)
            {
                if (GUI.Button(new Rect(6.5f * IMGUIScript.scr.x, IMGUIScript.scr.y * 4.25f, IMGUIScript.scr.x * 1.5f, IMGUIScript.scr.y * 1), "Take"))
                {
                    Inventory.inv.Add(ItemData.CreateItem(selectedChestItem.ID));
                    chestInv.Remove(selectedChestItem);
                    selectedChestItem = null;
                }
            }
        }
    }
}
