using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayinAlive : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject); //gameObjects refers to self, when not referring to anything else
    }
}
