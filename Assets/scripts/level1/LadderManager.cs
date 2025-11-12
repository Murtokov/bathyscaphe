using UnityEngine;
using UnityEngine.SceneManagement;

public class LadderManager : MonoBehaviour
{
    public bool isFixed = false;
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        object config = SavesManager.LoadConfigForScene(sceneName);
        if (config != null)
        {
            var field = config.GetType().GetField("isLadderFixed");
            if (field != null)
            {
                isFixed = (bool)field.GetValue(config);
            }
        }
        if (isFixed)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
