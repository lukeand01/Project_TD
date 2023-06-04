using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public static PlayerHandler instance;

    [HideInInspector] public PlayerController controller;
    [HideInInspector] public PlayerResource resource;
    public PlayerHQ hq;

    private void Awake()
    {
        instance = this;
    }



}
