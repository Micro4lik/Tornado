using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float xStart, zStart;

    [SerializeField] private int columnLength, rowLenght;
    [SerializeField] private int xSpace, zSpace;
    [SerializeField] private GameObject prefab;

    [SerializeField] private Material meshMaterial;

    private void Start()
    {
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for (int i = 0; i < columnLength * rowLenght; i++)
        {
            var cube = Instantiate(prefab,
                new Vector3(xStart + (xSpace * (i % columnLength)), 0, zStart + (-zSpace * (i / columnLength))),
                Quaternion.identity, transform);

            cube.GetComponent<MeshRenderer>().material = new Material(meshMaterial);
        }
    }
}