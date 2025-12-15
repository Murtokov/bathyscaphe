using UnityEngine;
using UnityEngine.UI;

public class BossManager : FishHealth
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip defaultMusic;
    public GameObject musicBox;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private Rigidbody2D submarine, rb;
    [SerializeField] private float detectionRadius = 300f;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool prevVal = false;

    private void Awake()
    {
        maxHealth = health;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        submarine = GameObject.FindGameObjectWithTag("Submarine").GetComponent<Rigidbody2D>();
        musicManager = musicBox.GetComponent<MusicManager>();
        defaultMusic = musicManager.BackgroundMusic.clip;
    }

    private void FixedUpdate()
    {
        bool isChasing = Vector2.Distance(submarine.position, rb.position) < detectionRadius;
        if (isChasing && isChasing != prevVal)
        {
            Debug.Log(isChasing);
            prevVal = isChasing;
            slider.gameObject.SetActive(true);
            musicManager.BackgroundMusic.clip = bossMusic;
            musicManager.BackgroundMusic.loop = true;
            MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
            musicManager.BackgroundMusic.volume = mainConfig.musicVolume;
            musicManager.BackgroundMusic.Play();
        }
        else if (!isChasing && isChasing != prevVal)
        {
            prevVal = isChasing;
            slider.gameObject.SetActive(false);
            musicManager.BackgroundMusic.clip = defaultMusic;
            musicManager.BackgroundMusic.loop = true;
            MainConfig mainConfig = SavesManager.LoadConfig<MainConfig>("MainConfig");
            musicManager.BackgroundMusic.volume = mainConfig.musicVolume;
            musicManager.BackgroundMusic.Play();
        }
    }

    public override void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        slider.value = health / maxHealth;
    }

    public override void Die()
    {
        slider.gameObject.SetActive(false);
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
