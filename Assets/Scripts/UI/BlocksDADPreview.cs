// using System.Diagnostics;
using UnityEngine;


// Blocks Drag And Drop Preview, the transparent meshes visible when previewing
public class BlocksDADPreview : MonoBehaviour
{
    // The name of the Building Block I'll drag and drop that will be passed to all of the spawn managers
    public string thisBlocksName;
    public LayerMask nodeLayer;
    public Material[] _materials; // 0 is green, 1 is red
    MeshRenderer _mr;
    public bool isInCorrectSpot;

    BuildManager buildManager; MousePointerScript mousePointer;

    // =============== [GENERAL UNITY METHODS] ===============

    void Start()
    {
        _mr = GetComponent<MeshRenderer>();

        // References to singlegons
        buildManager = BuildManager._instance;
        mousePointer = MousePointerScript._instance;

        // Sub to the event to check if the position is correct when moving around
        mousePointer.CurrentlySelectedCubeChanged += CheckIfPositionIsCorrect;
    }


    void Update()
    {
        // If the drag & drop is still active
        if (mousePointer.isLeftMousePressed())
        {
            // Prevent errors if somehow the user never had their mouse over the game area
            if (mousePointer.currentSelectedCube != null)
            {
                transform.position = mousePointer.currentSelectedCubeScript.GetLowestFreeSpacePoint();


                // I'm allowing up to 15 blocks in height
                // Each node has a max table of 16 just in case of an exception
                // And there's another debug method where something out of the 16 spaces returns -1 which is handled by the BuildManager
                // but I'm locking the 16th place here just to be sure, which is at height 30 and up (since index 0 is at 0)
                // Why not making this in the CheckIfPositionIsCorrect? Because it gets called BEFORE the block updates its position
                // So it thinks it's still at its previous position, so I can't check the y position because it'll be delayed by 1 block
                if (transform.position.y > 29)
                {
                    ChangeColor(false);
                }
            }
        }
        // Else try to spawn and destroy itself no matter what
        else
        {
            // If "isInCorrectSpot" only
            if (mousePointer.IsMouseInGamePosition() && isInCorrectSpot)
            {
                buildManager.SpawnNewBlock(mousePointer.currentSelectedCube, transform.eulerAngles.y);
            }

            // Unsubscribe from the event to prevent memory leaks
            mousePointer.CurrentlySelectedCubeChanged -= CheckIfPositionIsCorrect;
            Destroy(gameObject);
        }

        if (mousePointer.ScrollValue() != 0)
        {
            // Change the rotation by increments of 60 degrees
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + (60*mousePointer.ScrollValue()), 0);

            // If it is a block that goes over multiple spots, check if the spots are taken and if so make myself unplacable. 
            CheckIfPositionIsCorrect();
        }
    }

    // ============== [CHECK IF POSITION IS CORRECT] ===============

    // Called when the block preview rotates AND when the block preview changes position
    void CheckIfPositionIsCorrect()
    {
        // Debug.Log("Checking");

        // Create the bool
        bool isSpaceFree;

        // If I'm at the correct height, raycast for the postions
        RaycastHit hit = DoRaycastTowardsRotation();    

        // The blocks to check depends on the current block 
        switch (thisBlocksName)
        {
            case "BridgeTube":
                // Check if the space at this exact height is taken
                isSpaceFree = hit.transform.GetComponent<GridNodesScript>().IsThisSpaceFree(mousePointer.currentSelectedCubeScript.GetLowestFreeSpace());
            break;

            default:
                isSpaceFree = true;
            break;
        }

        ChangeColor(isSpaceFree);
    }

    void ChangeColor(bool isSpaceFree)
    {
        // If the space is free
        if (isSpaceFree)
        {
            // Appear green
            _mr.material = _materials[0];
            isInCorrectSpot = true;
        }
        else
        {
            // Appear red
            _mr.material = _materials[1];
            isInCorrectSpot = false;
        }
    }

    RaycastHit DoRaycastTowardsRotation()
    {
        float rotationAngle = transform.eulerAngles.y;

        // Create the vector in the direction of the bridge
        // -rotationAngle in sin because in my case, a bridge at 90° goes down, so at the equivalent of -90° in trigonometry
        // but the cos is the same one: start to the right. (I'm talking about a trigonometric circle)
        Vector3 raycastDirection = new Vector3(Mathf.Cos(rotationAngle * Mathf.Deg2Rad), 0, Mathf.Sin(- rotationAngle * Mathf.Deg2Rad)).normalized;

        // Don't start the raycast inside of the node or it'll hit itself, start it a bit outside
        Vector3 raycastStartPos = mousePointer.currentSelectedCube.transform.position + raycastDirection;

        // Visualize the ray's trajectory
        // Debug.DrawRay(raycastStartPos, raycastDirection * 10000, Color.blue, 99);

        RaycastHit _hit;

        // Do the raycast towards the good place
        Physics.Raycast(raycastStartPos, raycastDirection, out _hit, Mathf.Infinity, nodeLayer);

        return _hit;

    }
}
