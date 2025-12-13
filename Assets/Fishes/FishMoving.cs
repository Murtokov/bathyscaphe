using System.Collections;
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
        [Range(0f, 100f)] public float damage = 10f;
        private float _originalFreeMaxSpeed;
        private float _originalMaxSpeed;
        private Rigidbody2D _rb;
        private Coroutine _stasisCoroutine;

        private Rigidbody2D _submarine;
        private SubmarineLife _submarineLife;

        private void Start()
        {
            _submarine = GameObject.FindGameObjectWithTag("Submarine").GetComponent<Rigidbody2D>();
            _submarineLife = GameObject.FindGameObjectWithTag("Submarine").GetComponent<SubmarineLife>();
            _rb = GetComponent<Rigidbody2D>();
            _originalMaxSpeed = maxSpeed;
            _originalFreeMaxSpeed = freeMaxSpeed;
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

        private void OnDestroy()
        {
            if (_stasisCoroutine != null)
            {
                StopCoroutine(_stasisCoroutine);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!enabled) return;

            if (other.gameObject.CompareTag("Submarine"))
            {
                // var direction = (_rb.position - _submarine.position).normalized;
                // _rb.AddForce(direction * collisionRebound, ForceMode2D.Impulse);
                // _submarineLife.Damage(damage);
            }
        }

        public void StasisStop(float stasisDuration)
        {
            if (_stasisCoroutine != null)
            {
                StopCoroutine(_stasisCoroutine);
            }

            _stasisCoroutine = StartCoroutine(StasisStopCoroutine(stasisDuration));
        }

        private IEnumerator StasisStopCoroutine(float stasisDuration)
        {
            maxSpeed = 0f;
            freeMaxSpeed = 0f;
            yield return new WaitForSeconds(stasisDuration);
            maxSpeed = _originalMaxSpeed;
            freeMaxSpeed = _originalFreeMaxSpeed;
            _stasisCoroutine = null;
        }


        private void FollowSubmarine()
        {
            var direction = (_submarine.position - _rb.position).normalized;
            _rb.AddForce(direction * acceleration);

            var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var currentAngle = _rb.rotation;
            var angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);

            var angularDamping = 0.5f;
            var torque = angleDiff * rotationSpeed - _rb.angularVelocity * angularDamping;

            _rb.AddTorque(torque, ForceMode2D.Force);
            // _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle, rotationSpeed * Time.fixedDeltaTime);

            if (_rb.linearVelocity.magnitude > maxSpeed)
            {
                _rb.AddForce(_rb.linearVelocity.normalized * maxSpeed);
            }
        }

        private void FreeSwim()
        {
            var direction = new Vector2(1, 0);
            _rb.AddForce(direction * freeSwimAcceleration, ForceMode2D.Force);

            var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var currentAngle = _rb.rotation;
            var angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);
            var angularDamping = 0.5f;
            var torque = angleDiff * rotationSpeed - _rb.angularVelocity * angularDamping;

            _rb.AddTorque(torque, ForceMode2D.Force);

            // if (_rb.linearVelocity.magnitude > freeMaxSpeed)
            // {
            //     _rb.linearVelocity = _rb.linearVelocity.normalized * freeMaxSpeed;
            // }
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