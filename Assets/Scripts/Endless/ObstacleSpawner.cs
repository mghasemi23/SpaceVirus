using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Endless
{
    public class ObstacleSpawner : MonoBehaviour
    {
        #region variables

        public float ObstacleSpeed = 0.02f;
        public float SpeedCoefficient = 1.1f;
        public float DelayCoefficient = 0.9f;
        public int DifficultyNumber = 1;
        public float SpawnDelay;
        public float MaxSpeed = 0.05f;
        public float MinDelay = 2;
        public GameObject[] ObstaclePrefabs;

        private int _difficulty;

        #endregion


        private void Start()
        {
            Invoke("Spawn", 1);
        }


        private void Spawn()
        {
            _difficulty++;
            var rnd = Random.Range(0, ObstaclePrefabs.Length);
            var obs = Instantiate(ObstaclePrefabs[rnd]);
            obs.transform.parent = transform;
            obs.GetComponent<ObstacleMover>().Speed = ObstacleSpeed;
            if (_difficulty == DifficultyNumber)
            {
                _difficulty = 0;
                ObstacleSpeed *= SpeedCoefficient;
                SpawnDelay *= DelayCoefficient;
                if (SpawnDelay < MinDelay)
                {
                    SpawnDelay = MinDelay;
                }

                if (ObstacleSpeed > MaxSpeed)
                {
                    ObstacleSpeed = MaxSpeed;
                }
            }
            Invoke("Spawn", SpawnDelay);
        }
    }
}
