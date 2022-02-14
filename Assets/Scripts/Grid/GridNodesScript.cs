using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GridNodesScript : MonoBehaviour
{
    MeshRenderer mr;
    
    // An array to know which spaces are blocked above the node
    // I can't just say "I built 3 blocks" because bridges can take space on a node with free space in between
    // For example: space 1 is taken, space 2 is free and space 3 is taken by a bridge from an adjascent room.
    // There, saying "I built 2 blocks" taken is false because I have a free space at 2, but the space 3 is taken.
    // I need to have an array of free spaces available
    bool[] isSpaceFree;

    // =============== [GENERAL UNITY METHOD] =============
    void Awake()
    {
        mr = GetComponent<MeshRenderer>();

        // Initialize the array with 16 "Free Space" available
        isSpaceFree = new bool[16];
        for (int i = 0; i < isSpaceFree.Length; i++)
        {
            isSpaceFree[i] = true;
        }
    }

    // ============= [COLOR CHANGING METHOD] ==============

    public void ChangeColor(Color _color)
    {
        mr.material.color = _color;
    }

    // =========== [BLOCKS METHODS] ===========
    public int GetLowestFreeSpace()
    {
        // Loop through all of the spaces from 0 to 1
        for (int i = 0; i < isSpaceFree.Length; i++)
        {
            // If there's a free space
            if (isSpaceFree[i])
            {
                // Return the according height
                return i;
            }
        }
        return -1;
    }
    public float GetLowestFreeSpaceHeight()
    {
        // Returns the current height of the free space to build on, each block is 2 units in height
        return GetLowestFreeSpace() * 2;
    }

    public Vector3 GetLowestFreeSpacePoint()
    {
        // Returns the world position of the lowest free space to build on
        Vector3 pos = transform.position;
        pos[1] = GetLowestFreeSpaceHeight();
        return pos;
    }

    public void FillSpace(int i)
    {
        // Mark the space as unbuildable
        isSpaceFree[i] = false;
    }

    public void FreeSpace(int i )
    {
        // Mark the space as free
        isSpaceFree[i] = true;
    }
}
