using UnityEngine;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;
    public GameObject contextHint;
    public string fieldName;
    private bool inTrigger = false, switched = false;

    void Start()
    {
        contextHint = transform.GetChild(0).gameObject;
        contextHint.SetActive(false);
        string sceneName = SceneManager.GetActiveScene().name;
        object config = SavesManager.LoadConfigForScene(sceneName);
        if (config != null)
        {
            var field = config.GetType().GetField(fieldName);
            if (field != null)
            {
                switched = (bool)field.GetValue(config);
            }
        }

        if (switched)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteOff;
            GameObject lever = GameObject.FindGameObjectWithTag("Light");
            lever.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (!switched)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteOn;
            GameObject lever = GameObject.FindGameObjectWithTag("Light");
            lever.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!switched)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteOff;
                    switched = true;
                    GameObject lever = GameObject.FindGameObjectWithTag("Light");
                    lever.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = spriteOn;
                    switched = false;
                    GameObject lever = GameObject.FindGameObjectWithTag("Light");
                    lever.transform.GetChild(0).gameObject.SetActive(false);
                }

                string sceneName = SceneManager.GetActiveScene().name;
                object config = SavesManager.LoadConfigForScene(sceneName);
                if (config != null)
                {
                    var field = config.GetType().GetField(fieldName);
                    if (field != null)
                    {
                        field.SetValue(config, switched);
                        var method = typeof(SavesManager).GetMethod("SaveConfig")
                            .MakeGenericMethod(config.GetType());
                        method.Invoke(null, new object[] { config, sceneName });
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = true;
            contextHint.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            contextHint.SetActive(false);
        }
    }
}