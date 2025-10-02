using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestVision : MonoBehaviour
{
    public float checkRadius = 20f;
    public float checkInterval = 0.1f;
    public LayerMask wallLayer;

    private List<SpriteRenderer> renderersToCheck = new List<SpriteRenderer>();

    void Start()
    {
        StartCoroutine(CheckVisibilityRoutine());
    }

    IEnumerator CheckVisibilityRoutine()
    {
        while (true)
        {
            renderersToCheck.Clear();

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius);
                
            foreach (Collider2D col in hits)
            {
                SpriteRenderer rend = col.GetComponent<SpriteRenderer>();
                if (rend != null && col.gameObject != gameObject && (((1 << col.gameObject.layer) & wallLayer) == 0))
                {
                    renderersToCheck.Add(rend);
                }    
            }

            foreach (SpriteRenderer rend in renderersToCheck)
            {
                Vector2 direction = rend.transform.position - transform.position;
                float distance = direction.magnitude;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, wallLayer);
                if (hit.collider == null || hit.collider.gameObject == rend.gameObject)
                    rend.enabled = true;
                else
                    rend.enabled = false;
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
