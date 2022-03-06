using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I could've done a "rotate" button but on top of actually doing all of the raycast stuff to get to the node,
// it also would've to check all 5 positions to check the first one available probably with recursive methods... it's too complex for me right now
public class BlocksInteractionButtonsUI : MonoBehaviour
{
    GameObject currentlyReferencedBlock;
    [SerializeField] LayerMask nodeLayer;
    [SerializeField] AudioSource DestroyBlockAudio;

    public void DeleteBlock()
    {
        if (MousePointerScript._instance.currentSelectedBlock == null)
        { return; }

        // Get reference to the block
        currentlyReferencedBlock = MousePointerScript._instance.currentSelectedBlock;

        // Clear the spaces it took:

        // Raycast to get reference to the node we'll free spaces from
        // The thing is... To facilitate the building process with snaps, the pivot point of each block is on its bottom face
        // So if I want to get access to the node (which is at this point), I need to start it a big higher
        RaycastHit _hit;
        Physics.Raycast(currentlyReferencedBlock.transform.position + Vector3.up, Vector3.down, out _hit, Mathf.Infinity, nodeLayer);

        // Get the height of the block in index
        int blockHeight = (int)(currentlyReferencedBlock.transform.position.y / 2);

        // Clear the spaces
        _hit.transform.GetComponent<GridNodesScript>().FreeSpace(blockHeight);

        if (currentlyReferencedBlock.name.Contains("Bridge"))
        {
            // If it's a bridge it should clear the one next to it
            // So it's time to do all of those raycasts in reverse
            // Code copy-pasted from the Build Manager: BuildBlock() section "Bridge Tube"
            Vector3 nodePosition = _hit.transform.position;

            float yRotation = currentlyReferencedBlock.transform.eulerAngles.y;
            Vector3 raycastDirection = new Vector3(Mathf.Cos(yRotation * Mathf.Deg2Rad), 0, Mathf.Sin(- yRotation * Mathf.Deg2Rad)).normalized;
            Vector3 raycastStartPos = nodePosition + raycastDirection;
            Physics.Raycast(raycastStartPos, raycastDirection,out _hit, Mathf.Infinity, nodeLayer);
            _hit.transform.GetComponent<GridNodesScript>().FreeSpace(blockHeight);
        }

        // Destroy the block
        Destroy(currentlyReferencedBlock);

        // Play the sound
        DestroyBlockAudio.pitch = Random.Range(.5f, 1.2f);
        DestroyBlockAudio.Play();
    }

}
