using UnityEngine;

public class BalloonPick : MonoBehaviour
{
    public void Start()
    {
        Level3Ocean level3Ocean = SavesManager.LoadConfig<Level3Ocean>("Level3Ocean");
        if (level3Ocean.isBalloonCollected)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            Level3Ocean level3Ocean = SavesManager.LoadConfig<Level3Ocean>("Level3Ocean");
            level3Ocean.isBalloonCollected = true;
            SavesManager.SaveConfig<Level3Ocean>(level3Ocean, "Level3Ocean");
            SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
            submarineConfig.balloonEquipped = true;
            SavesManager.SaveConfig<SubmarineConfig>(submarineConfig, "SubmarineConfig");
            SubmarineMoving submarineMoving = collision.GetComponent<SubmarineMoving>();
            submarineMoving.UpdateBalloon();
            Destroy(gameObject);
        }
    }
}
