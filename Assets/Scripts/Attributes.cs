using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Attributes : MonoBehaviour
{
    #region Structs
    [Serializable]
    public struct Attribute
    {
        //qualities each attribute will need to have 
        public string name;
        public float currentValue;
        public float maxValue;
        public float regenValue;
        public Image displayImage;
    }
    #endregion

    #region Variables
    //all creatures start with 3 attributes e.g. health, stamina and mana
    public Attribute[] attributes = new Attribute[3];
    #endregion
}
