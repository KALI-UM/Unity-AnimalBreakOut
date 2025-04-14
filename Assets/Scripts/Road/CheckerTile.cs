using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckerTile : MonoBehaviour
{
    public Mesh[] meshes;
    [SerializeField]
    private MeshFilter[] tileMeshFilters;

    public Vector3 TileSize
    {
        get => tileSize;
    }
    private Vector3 tileSize = new(1f, 0f, 1f);

    [ContextMenu("Update TilePosition")]
    public void UpdateTilePosition()
    {
        for (int i = 0; i < tileMeshFilters.Length; i++)
        {
            tileMeshFilters[i].transform.position = new Vector3((i % 3) * tileSize.x - tileSize.x, 0, (i / 3) * tileSize.z);
        }
    }

    public Transform GetTileTrasnform(int colIndex, int rowIndex)
    {
        int index = colIndex * 3 + rowIndex;
        return tileMeshFilters[index].transform;
    }

    [ContextMenu("Update TileMesh")]
    public void UpdateTileMesh()
    {
        for (int i = 0; i < tileMeshFilters.Length; i++)
        {
            tileMeshFilters[i].mesh = meshes[i % 2];
        }
    }

    public Vector3 GetTileSize()
    {
        return tileSize;
    }

    public int GetTileVerticalCount()
    {
        return tileMeshFilters.Count() / 3;
    }
}
