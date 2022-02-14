using UnityEngine;


// Blocks Drag And Drop Preview, the transparent meshes visible when previewing
public class BlocksDADPreview : MonoBehaviour
{
    // The name of the Building Block I'll drag and drop that will be passed to all of the spawn managers
    public string thisBlocksName;

    BuildManager buildManager; MousePointerScript mousePointer;

    // =============== [GENERAL UNITY METHODS] ===============

    void Start()
    {
        // References to singlegons
        buildManager = BuildManager._instance;
        mousePointer = MousePointerScript._instance;
    }


    void Update()
    {
        // If the drag & drop is still active
        if (mousePointer.isLeftMousePressed())
        {
            transform.position = mousePointer.currentSelectedCubeScript.GetLowestFreeSpacePoint();
        }
        // Else try to spawn and destroy itself no matter what
        else
        {
            if (mousePointer.IsMouseInGamePosition())
            {
                buildManager.SpawnNewBlock(mousePointer.currentSelectedCube, transform.eulerAngles.y);
            }

            Destroy(gameObject);
        }

        if (mousePointer.ScrollValue() != 0)
        {
            // Change the rotation by increments of 60 degrees
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + (60*mousePointer.ScrollValue()), 0);
        }
    }
}
