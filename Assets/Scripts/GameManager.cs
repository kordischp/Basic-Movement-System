using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A singleton that gives an easy access to player reference without making it public 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player;

}
