using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class Level1MainBase
{
    public bool isLightOn = false;
    public bool isLadderFixed = false;
    public bool isPlateCollected = false;
    public bool isSubmarineUsed = false;
}

[System.Serializable]
public class Level1ParkourRoom
{
    public bool isLightOn = false;
    public bool isPlateCollected = false;
}

[System.Serializable]
public class Level1UpperBaseLeftSide
{
    public bool isPlateCollected = false;
}

[System.Serializable]
public class Level1UpperBaseRightSide
{
    public bool areLogsCollected = false;
}

[System.Serializable]
public class Level1Ocean
{
    public bool isOctopusDefeated = false;
    public bool isDoorToLevel2Opened = false;
    public Vector3 lastPosition = new Vector3(391, -327, 0);
}
