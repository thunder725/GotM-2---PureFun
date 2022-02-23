using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGrid : MonoBehaviour
{
    public static Vector3 IndexToGrid(Vector2 indexPos) // Input the index on the coordinate grid / output the real-world position with y=0
    {
        // This method transforms a regular integer coordinate grid (0,0 / 2,8 / -4,5)
        // into the correct coordinates on my triangular grid where everything should be spaced by 3
        // See this diagram to understand better the goal: https://media.discordapp.net/attachments/837367995755921438/940711709386096640/EquilateralGrid.png

        // Setup the returned vector
        Vector3 tempPos = Vector3.zero;

        // The x is 3*xIndex but also offset by 1.5 for each zIndex
        tempPos[0] = (3 * indexPos.x) + (1.5f * indexPos.y);

        // The z is calculated with the height of the equilateral triangle of size 3
        // SOH =>   Sin(angle) = Opposite / Hypothenuse   => Sin(60) = height / 3
        // <=> height = Sin(60) * 3  which is around 2.598 but I'm going to give the exact formula for precision's sake
        tempPos[2] = Mathf.Sin(Mathf.Deg2Rad * 60) * 3 * indexPos.y;

        // Return
        return tempPos;
    }

    // The other way around
    public static Vector2 GridToIndex(Vector3 gridPos)
    {
        Vector2 tempIndex = Vector2.zero;

        // Reverse engineer the positions
        tempIndex[1] = Mathf.Round(gridPos.z / ( 3 * Mathf.Sin(Mathf.Deg2Rad * 60)));

        tempIndex[0] = Mathf.Round((gridPos.x - 1.5f*tempIndex.y) / 3);

        return tempIndex;
    }

    public static void DebugTryout()
    {
        // A debug function to see if these grid conversions work
        // Pro tips: they work

        //   \n allows to get to the next line
        string _string = "Debugging grid with index values (1,-4), (13,17) and (-43, 36) : \n";

        string computeOne(Vector2 indexPos)
        {
            string __s = "Starting Index: " + indexPos;
            Vector3 _pos = IndexToGrid(indexPos);
            __s += "  / Grid position: " + _pos;
            __s += "  / Index positions from grid positions: " + GridToIndex(_pos);

            __s += " \n";
            return __s;
        }

        _string += computeOne(new Vector2(1, -4));
        _string += computeOne(new Vector2(13, 17));
        _string += computeOne(new Vector2(-43, 36));

        Debug.Log(_string);
    }

}
