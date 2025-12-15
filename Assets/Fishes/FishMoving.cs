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
<<<<<<< Updated upstream

        private Vector2 _freeSwimTarget;
        private float _freeSwimTimeLeft;

        private float _originalFreeMaxSpeed;
        private float _originalMaxSpeed;

        private Rigidbody2D _rb;
        private Coroutine _stasisCoroutine;
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private Rigidbody2D _rb;
=======
        public bool needFlip = true;
>>>>>>> Stashed changes

        protected Vector2 _freeSwimTarget;
        protected float _freeSwimTimeLeft;

<<<<<<< Updated upstream
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

=======
        protected Rigidbody2D _rb;
        protected Coroutine _stasisCoroutine;

        protected Rigidbody2D _submarine;
        protected SubmarineLife _submarineLife;
>>>>>>> Stashed changes

        protected bool _inStasis = false;

<<<<<<< Updated upstream
>>>>>>> Stashed changes
        private void Start()
=======
=======
        public bool needFlip = true;

        protected Vector2 _freeSwimTarget;
        protected float _freeSwimTimeLeft;

        protected Rigidbody2D _rb;
        protected Coroutine _stasisCoroutine;

        protected Rigidbody2D _submarine;
        protected SubmarineLife _submarineLife;

        protected bool _inStasis = false;

>>>>>>> Stashed changes
=======
        public bool needFlip = true;

        protected Vector2 _freeSwimTarget;
        protected float _freeSwimTimeLeft;

        protected Rigidbody2D _rb;
        protected Coroutine _stasisCoroutine;

        protected Rigidbody2D _submarine;
        protected SubmarineLife _submarineLife;

        protected bool _inStasis = false;

>>>>>>> Stashed changes
        protected void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        protected void Start()
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        {
            var submarineGo = GameObject.FindGameObjectWithTag("Submarine");
            _submarine = submarineGo.GetComponent<Rigidbody2D>();
            _submarineLife = submarineGo.GetComponent<SubmarineLife>();

            PickNewFreeSwimTarget();
        }

        protected void FixedUpdate()
        {
            if (_inStasis)
            {
                _rb.linearVelocity = Vector2.zero;
                return;
            }
<<<<<<< Updated upstream
<<<<<<< Updated upstream

            var distanceToSubmarine = Vector2.Distance(_submarine.position, _rb.position);
<<<<<<< Updated upstream

=======
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
=======
=======
>>>>>>> Stashed changes

            var distanceToSubmarine = Vector2.Distance(_submarine.position, _rb.position);


<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
            if (distanceToSubmarine < detectionRadius)
            {
                FollowSubmarine();
            }
            else
            {
                FreeSwim();
            }

            if (needFlip)
            {
                SwapDirection();
            }
        }

<<<<<<< Updated upstream
        private void OnDestroy()
        {
            if (_stasisCoroutine != null)
            {
                StopCoroutine(_stasisCoroutine);
            }
        }

=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
        private void OnCollisionEnter2D(Collision2D other)
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        protected void OnDestroy()
        {
            if (_stasisCoroutine != null)
            {
                StopCoroutine(_stasisCoroutine);
            }
        }

        protected void OnCollisionEnter2D(Collision2D other)
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        {
            if (!enabled) return;

            if (other.gameObject.CompareTag("Submarine"))
            {
                _submarineLife.Damage(damage);
            }
        }

<<<<<<< Updated upstream
        public void StasisStop(float stasisDuration)
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        public virtual void StasisStop(float stasisDuration)
>>>>>>> Stashed changes
        {
            if (_stasisCoroutine != null)
            {
                StopCoroutine(_stasisCoroutine);
            }
<<<<<<< Updated upstream

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

=======
            Debug.Log("Stopped");
>>>>>>> Stashed changes

            _stasisCoroutine = StartCoroutine(StasisStopCoroutine(stasisDuration));
        }

        protected IEnumerator StasisStopCoroutine(float stasisDuration)
        {
            _inStasis = true;

            yield return new WaitForSeconds(stasisDuration);

            _inStasis = false;
        }

>>>>>>> Stashed changes

        protected void FollowSubmarine()
        {
            var dir = (_submarine.position - _rb.position);
            if (dir.sqrMagnitude < 0.0001f) return;

<<<<<<< Updated upstream
            MoveAndFace(dir.normalized, acceleration, maxSpeed);
=======
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle, rotationSpeed * Time.fixedDeltaTime);

<<<<<<< Updated upstream
<<<<<<< Updated upstream
            if (_rb.linearVelocity.magnitude > maxSpeed)
=======
=======
>>>>>>> Stashed changes
        protected void FreeSwim()
        {
            UpdateFreeSwimTarget();

            var toTarget = _freeSwimTarget - _rb.position;
            if (toTarget.sqrMagnitude < 0.0001f)
>>>>>>> Stashed changes
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
            }
>>>>>>> Stashed changes
        }

        protected void FreeSwim()
        {
            UpdateFreeSwimTarget();

            var toTarget = _freeSwimTarget - _rb.position;
            if (toTarget.sqrMagnitude < 0.0001f)
            {
<<<<<<< Updated upstream
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
=======
<<<<<<< Updated upstream
                _rb.linearVelocity = _rb.linearVelocity.normalized * freeMaxSpeed;
            }
        }

=======
                toTarget = Vector2.right;
            }
>>>>>>> Stashed changes

            MoveAndFace(toTarget.normalized, freeSwimAcceleration, freeMaxSpeed);
        }

        protected void UpdateFreeSwimTarget()
        {
            _freeSwimTimeLeft -= Time.fixedDeltaTime;

            var closeEnough = Vector2.Distance(_rb.position, _freeSwimTarget) <= 10f;
            if (_freeSwimTimeLeft <= 0f || closeEnough)
            {
                PickNewFreeSwimTarget();
            }
        }

        protected void PickNewFreeSwimTarget()
        {
            _freeSwimTimeLeft = Random.Range(3, 10);
            _freeSwimTarget = _rb.position + Random.insideUnitCircle * 1000;
        }

        protected void MoveAndFace(Vector2 direction, float accel, float speedLimit)
        {
            if (direction.sqrMagnitude < 0.0001f) return;

            // Legacy behavior for Dynamic bodies.
            _rb.linearVelocity += direction.normalized * accel * Time.fixedDeltaTime;

            var desiredAngle = DirectionToAngleDeg(direction) + GetHeadOffsetDegrees();
            var currentAngle = _rb.rotation;
            var angleDiff = Mathf.DeltaAngle(currentAngle, desiredAngle);

            var targetRotation = _rb.rotation + angleDiff * rotationSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation,
                targetRotation,
                Time.fixedDeltaTime * rotationSpeed));

            _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, speedLimit);
        }

        protected float GetHeadOffsetDegrees()
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

        protected static float DirectionToAngleDeg(Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
>>>>>>> Stashed changes

        protected void SwapDirection()
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

<<<<<<< Updated upstream
        private static float Normalize360(float angleDeg)
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private float GetAngle()
=======
        protected static float Normalize360(float angleDeg)
>>>>>>> Stashed changes
=======
        protected static float Normalize360(float angleDeg)
>>>>>>> Stashed changes
=======
        protected static float Normalize360(float angleDeg)
>>>>>>> Stashed changes
>>>>>>> Stashed changes
        {
            var a = angleDeg % 360f;
            if (a < 0f) a += 360f;
            return a;
        }
    }
}