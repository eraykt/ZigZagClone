using System.Collections.Generic;
using UnityEngine;
using ZigZagClone.Controllers;
using kechigames.Singleton;

namespace ZigZagClone.Level
{
    public class LevelCreator : Singleton<LevelCreator>
    {
        #region Prefabs

        [Header("Prefabs")] public PlatformController startPlatformPrefab;
        public PlatformController endPlatformPrefab;
        public CubeController cubePrefab;

        #endregion

        [Space] public List<LevelInformation> levels = new List<LevelInformation>();

        private Queue<CubeController> cubes = new Queue<CubeController>();
        private Queue<CubeController> recycledCubes = new Queue<CubeController>();

        private PlatformController startPlatform, endPlatform;

        private CubePlacer cubePlacer;

        private int firstLevelsCubeCount;

        protected override void Awake()
        {
            base.Awake();
            cubePlacer = new CubePlacer(levels[GameManager.Instance.LevelIndex].cubeCount);
        }

        private void Start()
        {
            firstLevelsCubeCount = levels[0].cubeCount;
            CreateLevelObjects();
        }


        private void CreateLevelObjects()
        {
            startPlatform =
                Instantiate(startPlatformPrefab, Vector3.zero, startPlatformPrefab.transform.rotation);

            for (int i = 0; i < 20; i++)
            {
                var cube = Instantiate(cubePrefab, Vector3.zero, cubePrefab.transform.rotation);
                cubes.Enqueue(cube);
            }

            endPlatform = Instantiate(endPlatformPrefab, Vector3.zero, endPlatformPrefab.transform.rotation);
            endPlatform.gameObject.SetActive(false);
            CreateLevel(0);
        }

        public void CreateLevel(int levelIndex)
        {
            GameObject p = new GameObject("Level " + levelIndex)
            {
                transform = { parent = transform }
            };

            cubePlacer.PlaceStartingPlatform(startPlatform, p.transform);

            var count = cubes.Count;
            var ratio = levels[levelIndex].zigZagRatio;

            for (int i = 0; i < count; i++)
            {
                cubePlacer.PlaceCube(cubes.Dequeue(), ratio, p.transform);
            }

            cubePlacer.TryToPlaceEndingPlatform(endPlatform, p.transform);
        }

        public void RecycleCube(CubeController cube, Rigidbody rig)
        {
            if (!cube.CompareTag("Cube")) return;

            recycledCubes.Enqueue(cube);
            rig.useGravity = false;
            rig.isKinematic = true;
            rig.velocity = Vector3.zero;

            var parent = transform.GetChild(transform.childCount - 1);


            cubePlacer.TryToPlaceCube(recycledCubes.Dequeue(), levels[GameManager.Instance.LevelIndex].zigZagRatio,
                parent);

            cubePlacer.TryToPlaceEndingPlatform(endPlatform, parent);
        }

        public void ResetAllCubes()
        {
            cubes.Clear();
            foreach (Transform cube in transform.GetChild(transform.childCount - 1))
            {
                cube.GetComponent<CubeController>().StopAllCoroutines();
                ResetCube(cube.gameObject.GetComponent<CubeController>(), cube.GetComponent<Rigidbody>());
            }

            cubePlacer.Reset(levels[GameManager.Instance.LevelIndex].cubeCount);
        }

        private void ResetCube(CubeController cube, Rigidbody rig)
        {
            if (cube.CompareTag("Cube"))
                cubes.Enqueue(cube);

            if (cube.CompareTag("Finish"))
                cube.gameObject.SetActive(false);

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