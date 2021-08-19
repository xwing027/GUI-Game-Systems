using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Access Modifiers are public and private. Variables use camelCasing
    [Header("Player Health")]
    public float curHealth;
    public float maxHealth = 100;
    [Header("Lights")]
    public Light sun;
    [Range(-2, 2)]
    public float brightness = 2.5f;

    [Space(20)] // this creates space bewteen the components for ease of viewing

    #region Variables
    // Strings
    public string groupingOfCharacters = "alksdjflajfajlskfjaslkf";
    public bool trueOrFalse; //boolean
    public int wholeNumber;
    public float decimalNumber;

    #endregion

    //single line comment
    /* 
     paragraph comments
     are written like this 
     */

    public GameObject gO;
    public Transform t; // these two reference prebuilt components 
    
    //Struct
    public Vector2 xAndY; //2 floats
    public Vector3 xyAndZ; //3 floats
    public Quaternion xyzAndW; //4 floats - can be used with other functions, code knows that Quaternion means rotation

    [System.Serializable] //this makes it viewable in Unity
    public struct ExampleStruct 
    {
        public string name; // the white are like the xyandZ - names
        public int age;
        public float height;
        public bool isdead;
        // can also use
        public GameObject myBody;
        public Vector3 myPos;
    };

    //arrays (set list), lists (dynamic list) and dictionaries (dynamic list with a key)

    // Start is called before the first frame update
    void Start()
    {
        // Because we made sun public, we can reference it
        // float brightness = 2.5f; if not commented, this variable would be automatically private, as it is inside a container
        brightness = sun.intensity; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
