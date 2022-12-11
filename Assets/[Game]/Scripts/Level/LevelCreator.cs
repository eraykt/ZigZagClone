using System.Collections.Generic;
using UnityEngine;
using ZigZagClone.Controllers;
using Random = UnityEngine.Random;

namespace ZigZagClone.Level
{
    public class LevelCreator : kechigames.Singleton.Singleton<LevelCreator>
    {
        [Header("Prefabs")] public PlatformController startPlatformPrefab;
        public PlatformController endPlatformPrefab;
        public CubeController cubePrefab;

        [Space] public List<LevelInformation> levels = new List<LevelInformation>();

        public Queue<CubeController> Cubes = new Queue<CubeController>();
        private Queue<CubeController> recycledCubes = new Queue<CubeController>();

        [HideInInspector] public int currentLevelIndex = -2;

        private void Start()
        {
            CreateLevelObjects();
        }


        private void CreateLevelObjects()
        {
            var startingPlatform =
                Instantiate(startPlatformPrefab, Vector3.zero, startPlatformPrefab.transform.rotation);
            Cubes.Enqueue(startingPlatform);

            for (int i = 0; i < 20; i++)
            {
                var cube = Instantiate(cubePrefab, Vector3.zero, cubePrefab.transform.rotation);
                Cubes.Enqueue(cube);
            }

            var endingPlatform = Instantiate(endPlatformPrefab, Vector3.zero, endPlatformPrefab.transform.rotation);
            Cubes.Enqueue(endingPlatform);

            CreateLevel(0);
        }

        public void CreateLevel(int levelIndex)
        {
            currentLevelIndex++;
            var startingPlatform = Cubes.Dequeue();

            startingPlatform.transform.position = Vector3.zero;

            var lastPos = new Vector3(2, 0, 2);

            bool isRight = true;

            GameObject p = new GameObject("Level " + levelIndex)
            {
                transform = { parent = transform }
            };

            var count = Cubes.Count;

            startingPlatform.transform.parent = p.transform;

            for (int i = 1; i < count; i++)
            {
                var random = Random.Range(0, 101);
                var zigZag = random <= levels[levelIndex].zigZagRatio;


                var cube = Cubes.Dequeue();

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

                cube.transform.parent = p.transform;
                lastPos = cube.transform.position;
            }

            var endingPlatform = Cubes.Dequeue();
            endingPlatform.transform.position =
                new Vector3(lastPos.x + 3, 0, lastPos.z + 2);

            endingPlatform.transform.parent = p.transform;
        }
        
        public void RecycleCube(CubeController cube, Rigidbody rig)
        {
            if (!cube.gameObject.tag.Equals("Cube")) return;
                
            recycledCubes.Enqueue(cube);
            rig.useGravity = false;
            rig.isKinematic = true;
            rig.velocity = Vector3.zero;
        }
        
        public void ResetAllCubes()
        {
            Cubes.Clear();
            foreach (Transform cube in transform.GetChild(transform.childCount - 1))
            {
                cube.GetComponent<CubeController>().StopAllCoroutines();
                ResetCube(cube.gameObject.GetComponent<CubeController>(), cube.GetComponent<Rigidbody>());
            }
        }
        
        private void ResetCube(CubeController cube, Rigidbody rig)
        {
            Cubes.Enqueue(cube);
            rig.useGravity = false;
            rig.isKinematic = true;
            rig.velocity = Vector3.zero;
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