using UnityEngine;

public class StasisGunPick : MonoBehaviour
{
    public void Start()
    {
        Level2Ocean level2Ocean = SavesManager.LoadConfig<Level2Ocean>("Level2Ocean");
        if (level2Ocean.isGunCollected)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            Level2Ocean level2Ocean = SavesManager.LoadConfig<Level2Ocean>("Level2Ocean");
            level2Ocean.isGunCollected = true;
            SavesManager.SaveConfig<Level2Ocean>(level2Ocean, "Level2Ocean");
            SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
            submarineConfig.stasisGunEquipped = true;
            SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
            SubmarineRam submarineRam = collision.GetComponent<SubmarineRam>();
            // submarineRam.UpdateRam();
            Destroy(gameObject);
        }
    }
}
