using UnityEngine;

// Homemade Mathf extention for other mathematical equations and constants
public class TransformPlus : MonoBehaviour
{
    /// <summary> Returns the closest GameObject from the Position. </summary>
    /// <param name ="GOList"> List of Game Objects to search into. </param>
    /// <param name ="Position"> Position to calculate the smallest distance from. </param>
    public static GameObject ReturnClosest(GameObject[] GOList, Vector3 Position)
    {
        // Create the variables to store the closest object
        float smallestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        // Loop for each given GameObject
        foreach (GameObject _go in GOList)
        {
            // Compute the distance to it
            // Computing the Square Magnitude is faster than computing the Magnitude, and the order doesn't change
            // So this code will still return the closest one
            float distToObject = (Position - _go.transform.position).sqrMagnitude;
            
            // If calculated distance is smaller than the one stored
            if (distToObject < smallestDistance)
            {
                smallestDistance = distToObject;
                closestObject = _go;
            }
        }   
        // At the end of the loop, return the remaining one.
        return closestObject;
    }

    /// <summary> Returns the closest Transform from the Position. </summary>
    /// <param name ="TRList"> List of Transforms to search into. </param>
    /// <param name ="Position"> Position to calculate the smallest distance from. </param>
    public static Transform ReturnClosest(Transform[] TRList, Vector3 Position)
    {
        // Create the variables to store the closest object
        float smallestDistance = Mathf.Infinity;
        Transform closestObject = null;

        // Loop for each given GameObject
        foreach (Transform _tr in TRList)
        {
            // Compute the distance to it
            // Computing the Square Magnitude is faster than computing the Magnitude, and the order doesn't change
            // So this code will still return the closest one
            float distToObject = (Position - _tr.position).sqrMagnitude;
            
            // If calculated distance is smaller than the one stored
            if (distToObject < smallestDistance)
            {
                smallestDistance = distToObject;
                closestObject = _tr;
            }
        }   
        // At the end of the loop, return the remaining one.
        return closestObject;
    }

    /// <summary> Returns the closest Point from the Position. </summary>
    /// <param name ="VecList"> List of Points to search into. </param>
    /// <param name ="Position"> Position to calculate the smallest distance from. </param>
    /// <param name ="index"> Index of the vector within the VecList. </param>
    public static Vector3 ReturnClosest(Vector3[] VecList, Vector3 Position, out int index)
    {
        // Create the variables to store the closest object
        float smallestDistance = Mathf.Infinity;
        // Instead of storing a reference to the Transform or the GameObject using the ID,
        // I'll return the index of the Vector within the list
        int closestIndex = -1;
        // Or the Vector3 directly
        Vector3 closestVector = Vector3.zero;

        // Loop for each given GameObject

        for (int i = 0; i < VecList.Length; i++)
        {
            // Compute the distance to it
            // Computing the Square Magnitude is faster than computing the Magnitude, and the order doesn't change
            // So this code will still return the closest one
            float distToObject = (Position - VecList[i]).sqrMagnitude;
            
            // If calculated distance is smaller than the one stored
            if (distToObject < smallestDistance)
            {
                smallestDistance = distToObject;
                closestIndex = i;
                closestVector = VecList[i];
            }
        }
        
        // Return in the Out the index
        index = closestIndex;

        // At the end of the loop, return the remaining one.
        return closestVector;
    }

    /// <summary> Returns the closest Point from the Position. </summary>
    /// <param name ="VecList"> List of Points to search into. </param>
    /// <param name ="Position"> Position to calculate the smallest distance from. </param>
    /// <param name ="index"> Index of the vector within the VecList. </param>
    public static Vector3 ReturnClosest(Vector3[] VecList, Vector3 Position)
    {
        // Version without the out index if unnecessary

        // Create the variables to store the closest object
        float smallestDistance = Mathf.Infinity;
        // Or the Vector3 directly
        Vector3 closestVector = Vector3.zero;

        // Loop for each given GameObject

        // Loop for each given GameObject
        foreach (Vector3 _vec in VecList)
        {
            // Compute the distance to it
            // Computing the Square Magnitude is faster than computing the Magnitude, and the order doesn't change
            // So this code will still return the closest one
            float distToObject = (Position - _vec).sqrMagnitude;
            
            // If calculated distance is smaller than the one stored
            if (distToObject < smallestDistance)
            {
                smallestDistance = distToObject;
                closestVector = _vec;
            }
        }  

        // At the end of the loop, return the remaining one.
        return closestVector;
    }

    /// <summary> Returns the closest GameObject from the Position. </summary>
    /// <param name ="CollList"> List of Colliders to search into. </param>
    /// <param name ="Position"> Position to calculate the smallest distance from. </param>
    public static GameObject ReturnClosest(Collider[] CollList, Vector3 Position)
    {
        // Version usable with Physics.OverlapX that checks with Colliders

        // Create the variables to store the closest object
        float smallestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        // Loop for each given GameObject
        foreach (Collider _col in CollList)
        {
            // Compute the distance to it
            // Computing the Square Magnitude is faster than computing the Magnitude, and the order doesn't change
            // So this code will still return the closest one
            float distToObject = (Position - _col.transform.position).sqrMagnitude;
            
            // If calculated distance is smaller than the one stored
            if (distToObject < smallestDistance)
            {
                smallestDistance = distToObject;
                closestObject = _col.transform.gameObject;
            }
        }   
        // At the end of the loop, return the remaining one.
        return closestObject;
    }
}
