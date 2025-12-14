using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FishMoving : MonoBehaviour
    {
        public enum FishHeadDirection
        {
            Right,
            Left,
            Up,
        }

        public FishHeadDirection headDirection = FishHeadDirection.Right;

        [Range(0f, 1000f)] public float detectionRadius = 300f;
        [Range(0f, 100000f)] public float acceleration = 5000f;
        [Range(0f, 1000f)] public float maxSpeed = 40f;
        [Range(0f, 100f)] public float rotationSpeed = 30f;

        [Range(0f, 100000f)] public float freeSwimAcceleration = 500f;
        [Range(0f, 1000f)] public float freeMaxSpeed = 40f;

        [Range(0f, 100f)] public float damage = 10f;

        private Vector2 _freeSwimTarget;
        private float _freeSwimTimeLeft;

        private float _originalFreeMaxSpeed;
        private float _originalMaxSpeed;

        private Rigidbody2D _rb;
        private Coroutine _stasisCoroutine;

        private Rigidbody2D _submarine;
        private SubmarineLife _submarineLife;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            var submarineGo = GameObject.FindGameObjectWithTag("Submarine");
            _submarine = submarineGo.GetComponent<Rigidbody2D>();
            _submarineLife = submarineGo.GetComponent<SubmarineLife>();

            PickNewFreeSwimTarget();
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
                _submarineLife.Damage(damage);
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
            var dir = (_submarine.position - _rb.position);
            if (dir.sqrMagnitude < 0.0001f) return;

            MoveAndFace(dir.normalized, acceleration, maxSpeed);
        }

        private void FreeSwim()
        {
            UpdateFreeSwimTarget();

            var toTarget = _freeSwimTarget - _rb.position;
            if (toTarget.sqrMagnitude < 0.0001f)
            {
                toTarget = Vector2.right;
            }

            MoveAndFace(toTarget.normalized, freeSwimAcceleration, freeMaxSpeed);
        }

        private void UpdateFreeSwimTarget()
        {
            _freeSwimTimeLeft -= Time.fixedDeltaTime;

            var closeEnough = Vector2.Distance(_rb.position, _freeSwimTarget) <= 10f;
            if (_freeSwimTimeLeft <= 0f || closeEnough)
            {
                PickNewFreeSwimTarget();
            }
        }

        private void PickNewFreeSwimTarget()
        {
            _freeSwimTimeLeft = Random.Range(3, 10);
            _freeSwimTarget = _rb.position + Random.insideUnitCircle * 1000;
        }

        private void MoveAndFace(Vector2 direction, float accel, float speedLimit)
        {
            if (direction.sqrMagnitude < 0.0001f) return;

            // Legacy behavior for Dynamic bodies.
            _rb.AddForce(direction.normalized * accel, ForceMode2D.Force);

            var desiredAngle = DirectionToAngleDeg(direction) + GetHeadOffsetDegrees();
            var currentAngle = _rb.rotation;
            var angleDiff = Mathf.DeltaAngle(currentAngle, desiredAngle);

            var targetRotation = _rb.rotation + angleDiff * rotationSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation,
                targetRotation,
                Time.fixedDeltaTime * rotationSpeed));

            if (_rb.linearVelocity.magnitude > speedLimit)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * speedLimit;
            }
        }

        private float GetHeadOffsetDegrees()
        {
            // Offsets so sprite head aligns with movement direction:
            // Right: 0, Left: 180, Up: -90 (so directionAngle=90 gives rotation=0)
            return headDirection switch
            {
                FishHeadDirection.Right => 0f,
                FishHeadDirection.Left => 180f,
                FishHeadDirection.Up => -90f,
                _ => 0f,
            };
        }

        private static float DirectionToAngleDeg(Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        private void SwapDirection()
        {
            var headWorldAngle = Normalize360(_rb.rotation + 90);

            var shouldBeFlipped = headWorldAngle > 180f;
            var scale = transform.localScale;

            if (shouldBeFlipped && scale.y > 0f)
            {
                transform.localScale = new Vector3(scale.x, -scale.y, scale.z);
            }
            else if (!shouldBeFlipped && scale.y < 0f)
            {
                transform.localScale = new Vector3(scale.x, -scale.y, scale.z);
            }
        }

        private static float Normalize360(float angleDeg)
        {
            var a = angleDeg % 360f;
            if (a < 0f) a += 360f;
            return a;
        }
    }
}