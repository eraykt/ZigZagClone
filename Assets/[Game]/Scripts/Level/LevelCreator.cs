using System.Collections.Generic;
using UnityEngine;
using ZigZagClone.Controllers;

namespace ZigZagClone.Level
{
    public class LevelCreator : MonoBehaviour
    {
        [Header("Prefabs")] public PlatformController startPlatformPrefab;
        public PlatformController endPlatformPrefab;
        public CubeController cubePrefab;

        [Space] public List<LevelInformation> levels = new List<LevelInformation>();


        private void Start()
        {
            CreateLevelObjects(0);
        }

        public void CreateLevelObjects(int levelIndex)
        {
            Queue<CubeController> cubes = new Queue<CubeController>();

            var startPlatform = Instantiate(startPlatformPrefab, Vector3.zero, startPlatformPrefab.transform.rotation);
            cubes.Enqueue(startPlatform);

            for (int i = 0; i < levels[levelIndex].cubeCount; i++)
            {
                var cube = Instantiate(cubePrefab, Vector3.zero, cubePrefab.transform.rotation);
                cubes.Enqueue(cube);
            }

            var endingPlatform = Instantiate(endPlatformPrefab, Vector3.zero, endPlatformPrefab.transform.rotation);
            cubes.Enqueue(endingPlatform);

            PathDesigner(cubes, levelIndex);
        }

        private void PathDesigner(Queue<CubeController> cubes, int levelIndex)
        {
            var startingPlatform = cubes.Dequeue();
            startingPlatform.transform.position = Vector3.zero;
            var lastPos = new Vector3(3, 0, 2);

            bool isRight = true;

            for (int i = 1; i <= levels[levelIndex].cubeCount; i++)
            {
                var random = Random.Range(0, 101);
                var zigZag = random <= levels[levelIndex].zigZagRatio;


                var cube = cubes.Dequeue();

                switch (zigZag)
                {
                    case true:
                        if (isRight)
                            cube.transform.position = new Vector3(lastPos.x, lastPos.y, lastPos.z + 1);

                        else
                            cube.transform.position = new Vector3(lastPos.x + 1, lastPos.y, lastPos.z);

                        isRight = !isRight;

                        break;

                    case false:
                        if (isRight)
                            cube.transform.position = new Vector3(lastPos.x + 1, 0, lastPos.z);

                        else
                            cube.transform.position = new Vector3(lastPos.x, 0, lastPos.z + 1);

                        break;
                }

                lastPos = cube.transform.position;
            }

            var endingPlatform = cubes.Dequeue();
            endingPlatform.transform.position =
                new Vector3(lastPos.x + 4, 0, lastPos.z + 2);
        }
    }

    [System.Serializable]
    public class LevelInformation
    {
        [Space] public int cubeCount;
        [Range(0, 100)] public int zigZagRatio;
        public int coinCount;
        public int diamondCount;
    }
}