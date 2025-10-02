using UnityEngine;

namespace DefaultNamespace
{
    public class SimpleDoor : MonoBehaviour
    {
        [Range(0.5f, 5f)] public double openDistance = 3.0;
        private Animator _animator;
        private GameObject _player;
        private Transform _self;

        private void Start()
        {
            _self = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            var distanceX = Mathf.Abs(_self.position.x - _player.transform.position.x);
            _animator.SetBool("Opened", distanceX < openDistance);
        }
    }
}