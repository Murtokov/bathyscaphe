using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class OctopusTentacleEnd : MonoBehaviour
{
    [Tooltip("Сила евреев")] [SerializeField]
    private float pushForce = 3f;

    private Rigidbody2D _rb;
    private float _timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if (_timer >= 3f)
        {
            PushRandom();
            _timer = 0f;
        }
    }

    private void PushRandom()
    {
        var randomDirection = Random.insideUnitCircle.normalized;
        _rb.AddForce(randomDirection * pushForce, ForceMode2D.Impulse);
    }
}