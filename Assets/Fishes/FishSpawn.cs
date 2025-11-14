using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class FishSpawn : MonoBehaviour
    {
        public List<GameObject> fishesPrefabs;
        public List<int> fishesCounts;

        [Range(0f, 10000f)] public float width = 3000f;
        [Range(0f, 10000f)] public float height = 200f;

        private void Awake()
        {
            for (int i = 0; i < fishesPrefabs.Count; i++)
            {
                for (int j = 0; j < fishesCounts[i]; j++)
                {
                    var prefab = fishesPrefabs[i];
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
}