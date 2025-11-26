using UnityEngine;

[System.Serializable]
public class SubmarineConfig
{
    public int maxHealth = 100;
    public int health = 100;
    public float speed = 1;
    public bool ramEquipped = false;
    public bool balloonEquipped = false;
    public bool stasisGunEquipped = false;
    public Vector3 lastPosition = new Vector3(118, -88, 0);
}
