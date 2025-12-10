using UnityEngine;

public class ChangeToSubmarine : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.F;
    public string playerTag = "Player";
    public GameObject submarineCamera;

    private bool isPlayerInTrigger = false;
    private GameObject playerObject;

    private GameObject innerLight;
    private GameObject outerLight;  

    private SubmarineMoving submarineMovingScript;
    private StasisGun submarineStasis;
    private ParticleSystem ps;
    private SpriteRenderer sr;

    private GameObject submarineBackground;
    private GameObject interier;

    private bool inSubmarine = false;
    private bool stasisEquipped = false;

    void Start()
    {
        playerObject = transform.parent.Find("Player").gameObject;

        innerLight = transform.parent.Find("Submarine Inner Light").gameObject;
        outerLight = transform.parent.Find("Submarine Outer Light").gameObject;
        outerLight.SetActive(false);

        submarineMovingScript = transform.parent.GetComponent<SubmarineMoving>();
        submarineStasis = transform.parent.GetComponent<StasisGun>();
        UpdateStasis();
        submarineStasis.enabled = false;
        submarineMovingScript.enabled = false;

        ps = transform.parent.GetComponent<ParticleSystem>();
        sr = transform.parent.GetComponent<SpriteRenderer>();
        ps.Stop();
        sr.enabled = false;

        submarineBackground = transform.parent.Find("Submarine Background").gameObject;
        interier = transform.parent.Find("Interier").gameObject;
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(interactionKey))
        {
            SwapToSubmarine();
        } else if (inSubmarine && Input.GetKeyDown(interactionKey))
        {
            SwapToCharacter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            isPlayerInTrigger = true;
            playerObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            isPlayerInTrigger = false;
        }
    }

    public void UpdateStasis()
    {
        SubmarineConfig submarineConfig = SavesManager.LoadConfig<SubmarineConfig>("SubmarineConfig");
        stasisEquipped = submarineConfig.stasisGunEquipped;
        submarineStasis.enabled = stasisEquipped;
    }

    private void SwapToSubmarine()
    {
        Debug.Log("ToSub");
        playerObject.SetActive(false);

        submarineCamera.SetActive(true);
        submarineMovingScript.enabled = true;
        submarineStasis.enabled = stasisEquipped;

        outerLight.SetActive(true);
        innerLight.SetActive(false);

        ps.Play();
        sr.enabled = true;

        submarineBackground.SetActive(false);
        interier.SetActive(false);

        inSubmarine = true;
        isPlayerInTrigger = false;
    }

    private void SwapToCharacter()
    {
        Debug.Log("ToChar");
        submarineCamera.SetActive(false);
        submarineMovingScript.enabled = false;
        submarineStasis.enabled = false;

        submarineMovingScript.StopSubmarine();

        ps.Stop();
        sr.enabled = false;

        playerObject.SetActive(true);

        outerLight.SetActive(false);
        innerLight.SetActive(true);

        submarineBackground.SetActive(true);
        interier.SetActive(true);

        inSubmarine = false;
        isPlayerInTrigger = true;
    }
}