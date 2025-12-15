using DefaultNamespace;
using UnityEngine;

public class SupportController : FishMoving
{
    public int stasisToKill = 3;
    private int beingStasised = 0;
    public override void StasisStop(float stasisDuration)
    {
        beingStasised++;
        if (beingStasised >= stasisToKill)
        {
            FishHealth fishHealth = GetComponent<FishHealth>();
            if (fishHealth != null)
            {
                fishHealth.Die();
            }
        }
        if (_stasisCoroutine != null)
        {
            StopCoroutine(_stasisCoroutine);
        }
        Debug.Log("Stopped");

        _stasisCoroutine = StartCoroutine(StasisStopCoroutine(stasisDuration));
    }
}
