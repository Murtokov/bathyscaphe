using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Level2Ocean
{
    public bool isRamCollected = false;
    public bool isGunCollected = false;
    public bool isWalrusDefeated = false;
    public Vector3 doorPosition = new Vector3(0, 0, 0);
}