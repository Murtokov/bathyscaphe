using UnityEngine;

public class RamPick : MonoBehaviour
{
    public void Start()
    {
        Level2Ocean level2Ocean = SavesManager.LoadConfig<Level2Ocean>("Level2Ocean");
        level2Ocean.isRamCollected = true;
        gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            Level2Ocean level2Ocean = SavesManager.LoadConfig<Level2Ocean>("Level2Ocean");
            level2Ocean.isRamCollected = true;
            SavesManager.SaveConfig<Level2Ocean>(level2Ocean, "Level2Ocean");
            SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
            submarineConfig.ramEquipped = true;
            SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
            SubmarineRam submarineRam = collision.GetComponent<SubmarineRam>();
            submarineRam.UpdateRam();
            GameObject fishes = GameObject.FindGameObjectWithTag("TunnelFish");
            fishes.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
