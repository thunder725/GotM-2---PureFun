using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    [SerializeField] GameObject cubeVisualizerPrefab;
    [SerializeField] [Range(1, 35)] int distFromZero;
    [SerializeField] float gizmoSize, cubeSize;


    void Awake()
    {
        if (transform.childCount != 0)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
        StartVisualization();
    }

    void StartVisualization()
    {
        // For a hexagonal grid, if you want to show one away from the center,
        // In indexgrid form it's 1 and -1 in every x and y position, removing some sums
        // Just remove some things when the sum is too far... it works and shows a hexagon

        for (int i = -distFromZero; i <= distFromZero; i++)
        {
            for (int j = -distFromZero; j <= distFromZero; j++)
            {
                // Keep only the wanted points to keep just a hexagon 
                if (Mathf.Abs(i + j) <= distFromZero)
                {
                    var _pos = TriangleGrid.IndexToGrid(new Vector2(i, j));
                    _pos[1] = .1f;
                    var _c = Instantiate(cubeVisualizerPrefab, _pos, Quaternion.identity, transform);
                    _c.transform.localScale = Vector3.one * cubeSize;
                }
            }
        }
    }


    // GizmoVersion
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = -distFromZero; i <= distFromZero; i++)
        {
            for (int j = -distFromZero; j <= distFromZero; j++)
            {
                // Keep only the wanted points to keep just a hexagon 
                if (Mathf.Abs(i + j) <= distFromZero)
                {
                    Gizmos.DrawCube(TriangleGrid.IndexToGrid(new Vector2(i, j)), Vector3.one * gizmoSize);
                }
            }
        }
    }
}
