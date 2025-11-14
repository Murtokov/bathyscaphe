using UnityEngine;

namespace DefaultNamespace
{
    public class SimpleDoor : MonoBehaviour
    {
        [Range(0.5f, 5f)] public double openDistance = 3.0;
        [Range(0.5f, 50f)] public double maxY = 5.0;
        [Range(0.1f, 5f)] public double openingSpeed = 1.0;

        private Animator _animator;
        private double _nowY = 0.0;
        private GameObject _player;
        private Transform _self;

        private void Start()
        {
            _self = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void FixedUpdate()
        {
            var distanceX = Mathf.Abs(_self.position.x - _player.transform.position.x);
            if (distanceX < openDistance && _nowY < maxY)
            {
                var moveY = openingSpeed * Time.fixedDeltaTime;
                if (_nowY + moveY > maxY)
                {
                    moveY = maxY - _nowY;
                }

                _self.position = new Vector3(_self.position.x, (float)(_self.position.y + moveY), _self.position.z);
                _nowY += moveY;
            }
            else if (distanceX >= openDistance && _nowY > 0.0)
            {
                var moveY = openingSpeed * Time.fixedDeltaTime;
                if (_nowY - moveY < 0.0)
                {
                    moveY = _nowY;
                }

                _self.position = new Vector3(_self.position.x, (float)(_self.position.y - moveY), _self.position.z);
                _nowY -= moveY;
            }
        }
    }
}