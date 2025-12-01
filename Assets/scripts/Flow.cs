using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Flow : MonoBehaviour
{
    public float groundForce = 200f;
    public float topForce = 600f;

    private BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Submarine"))
        {
            float part = Mathf.InverseLerp(
                boxCollider.bounds.min.y,
                boxCollider.bounds.max.y,
                collision.transform.position.y
            );

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            float force = Mathf.Lerp(groundForce, topForce, part);

            rb.AddForce(force * Vector2.down);
        }
    }
}
