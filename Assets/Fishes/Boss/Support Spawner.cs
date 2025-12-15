using System.Collections;
using UnityEngine;

public class SupportSpawner : MonoBehaviour
{
    public Transform currentPosition;
    public float abilityCooldown = 30f;
    public float spawnDelay = 1f;
    public float supportCount = 3f;
    public GameObject supportPrefab;

    private float previousAbilityUse;
    private void Start()
    {
        previousAbilityUse = Time.time;
    }
    void Update()
    {
        if (previousAbilityUse + abilityCooldown < Time.time)
        {
            previousAbilityUse = Time.time;
            StartCoroutine(SpawnCoroutine(supportCount));
        }
    }

    private IEnumerator SpawnCoroutine(float count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 globalPosition = currentPosition.TransformPoint(currentPosition.localPosition);
            Instantiate(supportPrefab, globalPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
