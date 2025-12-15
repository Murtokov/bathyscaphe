using DefaultNamespace;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StasisProjectile : MonoBehaviour
{
    public float stasisDuration = 3f;
    public float intensityMultiplier = 2f;
    public float radiusMultiplier = 2f;
    public float lifeDuration = 10f;

    private Light2D light2D;
    private float baseIntensity;
    private float baseMagnitude;
    private float baseOuterRadius;
    private float baseInnerRadius;
    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
        light2D = GetComponentInChildren<Light2D>();

        if (light2D != null)
        {
            baseIntensity = light2D.intensity;
            baseOuterRadius = light2D.pointLightOuterRadius;
            baseInnerRadius = light2D.pointLightInnerRadius;
        }

        baseMagnitude = transform.localScale.magnitude;
        spawnTime = Time.time;
    }

    private void Update()
    {
        float magnitude = transform.localScale.magnitude;
        float scaleMultiplier = magnitude / baseMagnitude;

        light2D.intensity = scaleMultiplier * intensityMultiplier * baseIntensity;
        light2D.pointLightOuterRadius = scaleMultiplier * radiusMultiplier * baseOuterRadius;
        light2D.pointLightInnerRadius = scaleMultiplier * radiusMultiplier * baseInnerRadius;

        if (Time.time > spawnTime + lifeDuration)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FishMoving fish = collision.gameObject.GetComponent<FishMoving>();

        if (fish != null)
        {
            fish.StasisStop(stasisDuration);
        }

        Debug.Log(collision.transform.name);

        Destroy(gameObject);
    }
}
