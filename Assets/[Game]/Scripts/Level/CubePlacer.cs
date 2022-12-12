using UnityEngine;
using ZigZagClone.Controllers;

public class CubePlacer
{
    private Vector3 lastCubePosition;
    private bool isPlacedToRight;

    private int cubesToBePlaced;

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

        platform.transform.position = new Vector3(lastCubePosition.x + 3, 0, lastCubePosition.z + 2);
        platform.gameObject.SetActive(true);
        platform.transform.parent = parent;
    }

    public void TryToPlaceCube(CubeController cube, int ratio, Transform parent)
    {
        if (cubesToBePlaced == 0) return;
        
        PlaceCube(cube, ratio, parent);
    }


    public void Reset(int countCubesToBePlaced)
    {
        lastCubePosition = new Vector3(2, 0, 2);
        isPlacedToRight = true;
        cubesToBePlaced = countCubesToBePlaced;
    }
}