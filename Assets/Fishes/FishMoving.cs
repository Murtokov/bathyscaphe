using UnityEngine;

namespace DefaultNamespace
{
    public class FishMoving : MonoBehaviour
    {
        [Range(0f, 1000f)] public float detectionRadius = 300f;
        [Range(-10000f, 10000f)] public float freeSwimAcceleration = 500f;

        [Range(0f, 100000f)] public float acceleration = 5000f;
        [Range(0f, 100000f)] public float collisionRebound = 60000f;

        [Range(0f, 10f)] public float rotationSpeed = 1f;
        [Range(0f, 1000f)] public float maxSpeed = 40f;
        [Range(0f, 1000f)] public float freeMaxSpeed = 40f;
        private Rigidbody2D _rb;

        private Rigidbody2D _submarine;

        private void Start()
        {
            _submarine = GameObject.FindGameObjectWithTag("Submarine").GetComponent<Rigidbody2D>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var distanceToSubmarine = Vector2.Distance(_submarine.position, _rb.position);
            if (distanceToSubmarine < detectionRadius)
            {
                FollowSubmarine();
            }
            else
            {
                FreeSwim();
            }

            SwapDirection();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Submarine"))
            {
                var direction = (_rb.position - _submarine.position).normalized;
                _rb.AddForce(direction * collisionRebound, ForceMode2D.Impulse);
            }
        }


        private void FollowSubmarine()
        {
            var direction = (_submarine.position - _rb.position).normalized;
            _rb.AddForce(direction * acceleration);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle, rotationSpeed * Time.fixedDeltaTime);

            if (_rb.linearVelocity.magnitude > maxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
            }
        }

        private void FreeSwim()
        {
            var direction = new Vector2(1, 0);
            _rb.AddForce(direction * freeSwimAcceleration);
            var rotation = 90f - 90f * Mathf.Sign(freeSwimAcceleration);
            _rb.rotation = Mathf.LerpAngle(_rb.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);

            if (_rb.linearVelocity.magnitude > freeMaxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * freeMaxSpeed;
            }
        }


        private void SwapDirection()
        {
            if (GetAngle() > 180f && transform.localScale.y < 0f)
            {
                transform.localScale =
                    new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
            else if (GetAngle() < 180f && transform.localScale.y > 0f)
            {
                transform.localScale =
                    new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
        }

        private float GetAngle()
        {
            return (_rb.rotation % 360f + 360f + 270f) % 360f;
        }
    }
}