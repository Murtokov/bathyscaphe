using UnityEngine;

[System.Serializable]
public class SubmarineConfig
{
    public float health = 100;
    public float speed = 1;
    public bool ramEquipped = false;
    public bool balloonEquipped = false;
    public bool stasisGunEquipped = false;
    public Vector3 lastPosition = new Vector3(118, -88, 0);
}
