using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class FishSpawn : MonoBehaviour
    {
        public List<GameObject> fishesPrefabs;
        [Range(1, 1000)] public int count = 10;

        [Range(0f, 10000f)] public float width = 3000f;
        [Range(0f, 10000f)] public float height = 200f;

        private void Awake()
        {
            for (int i = 0; i < count; i++)
            {
                var prefab = fishesPrefabs[Random.Range(0, fishesPrefabs.Count)];
                var fish = Instantiate(prefab, transform);
                fish.transform.localPosition = new Vector3(
                    Random.Range(-width, width),
                    Random.Range(-height, height),
                    0f
                );
                fish.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                fish.GetComponent<FishMoving>().freeMaxSpeed = Random.Range(10f, 50f);
                fish.GetComponent<FishMoving>().freeSwimAcceleration = Random.Range(-1000f, 1000f);
            }
        }
    }
}