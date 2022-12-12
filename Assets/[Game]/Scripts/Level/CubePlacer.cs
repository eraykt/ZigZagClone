using DG.Tweening;
using UnityEngine;
using ZigZagClone.Controllers;

public class CubePlacer
{
    private Vector3 lastCubePosition;
    private bool isPlacedToRight;

    private int cubesToBePlaced;

    private bool isEndingPlatformPlaced;

    public CubePlacer(int cubesToBePlaced)
    {
        Reset(cubesToBePlaced);
    }

    public void PlaceCube(CubeController cube, int ratio, Transform parent)
    {
        var random = Random.Range(0, 101);
        var zigZag = random <= ratio;

        switch (zigZag)
        {
            case true:
                if (isPlacedToRight)
                    cube.transform.position =
                        new Vector3(lastCubePosition.x, lastCubePosition.y, lastCubePosition.z + 1);

                else
                    cube.transform.position =
                        new Vector3(lastCubePosition.x + 1, lastCubePosition.y, lastCubePosition.z);

                isPlacedToRight = !isPlacedToRight;

                break;

            case false:
                if (isPlacedToRight)
                    cube.transform.position = new Vector3(lastCubePosition.x + 1, 0, lastCubePosition.z);

                else
                    cube.transform.position = new Vector3(lastCubePosition.x, 0, lastCubePosition.z + 1);

                break;
        }

        cube.transform.parent = parent.transform;
        lastCubePosition = cube.transform.position;
        cubesToBePlaced--;
    }

    public void PlaceStartingPlatform(PlatformController platform, Transform parent)
    {
        platform.transform.position = Vector3.zero;
        platform.transform.parent = parent;
    }

    public void TryToPlaceEndingPlatform(PlatformController platform, Transform parent)
    {
        if (cubesToBePlaced != 0) return;
        if (isEndingPlatformPlaced) return;

        platform.transform.position = new Vector3(lastCubePosition.x + 3, 10, lastCubePosition.z + 2);
        platform.gameObject.SetActive(true);
        platform.transform.parent = parent;

        platform.transform.DOMoveY(0f, 0.25f);
        isEndingPlatformPlaced = true;
    }

    public void TryToPlaceCube(CubeController cube, int ratio, Transform parent)
    {
        if (cubesToBePlaced == 0)
        {
            cube.GetComponent<Rigidbody>().useGravity = true;
            cube.GetComponent<Rigidbody>().isKinematic = false;
            return;
        }

        if (GameManager.Instance.IsGameEnded) return;

        PlaceCube(cube, ratio, parent);

        cube.transform.position = new Vector3(cube.transform.position.x, 10f, cube.transform.position.z);

        cube.transform.DOMoveY(0f, 0.25f);
    }


    public void Reset(int countCubesToBePlaced)
    {
        lastCubePosition = new Vector3(2, 0, 2);
        isPlacedToRight = true;
        cubesToBePlaced = countCubesToBePlaced;
        isEndingPlatformPlaced = false;
    }
}