using UnityEngine;

[System.Serializable]
public class Level1MainBase
{
    public bool isLightOn = false;
    public bool isLadderFixed = false;
    public bool isPlateCollected = false;
    public bool isRepairKitCollected = false;
}

[System.Serializable]
public class Level1ParkourRoom
{
    public bool isLightOn = false;
    public bool isPlateCollected = false;
    public bool isScrewCollected = false;
}

[System.Serializable]
public class Level1UpperBaseLeftSide
{
    public bool isScrewCollected = false;
}

[System.Serializable]
public class Level1UpperBaseRightSide
{
    public bool areLogsCollected = false;
    public bool isRepairKitCollected = false;
}

[System.Serializable]
public class Level1Ocean
{
    public bool isOctopusDefeated = false;
    public bool isDoorToLevel2Opened = false;
    public Vector3 lastPosition = new Vector3(95.5f,-93.8f,0);
}
