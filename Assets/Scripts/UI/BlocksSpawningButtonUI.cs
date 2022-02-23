using UnityEngine;

// The buttons used to spawn the blocks
public class BlocksSpawningButtonUI : MonoBehaviour
{
    // The name of the Building Block I'll drag and drop that will be passed to all of the spawn managers
    [SerializeField] string thisBlocksName;

    BuildManager buildManager; MousePointerScript mousePointer;

    // The mesh of the block the button spawns. Used to spawn a transparent copy of it when drag & dropping
    Mesh thisBlocksMesh;
    [SerializeField] BlockPreviewScriptableObject blockPreviewSO;

    // =============== [GENERAL UNITY METHODS] ===============

    void Start()
    {
        // Singleton!
        buildManager = BuildManager._instance;
        mousePointer = MousePointerScript._instance;
        thisBlocksMesh = buildManager.GetPrefabFromName(thisBlocksName).GetComponent<MeshFilter>().sharedMesh;
    }
     


    // On Click, start the Drag & Drop sequence
    public void StartDragAndDrop()
    {
        buildManager.SwitchNewSelectedBlock(thisBlocksName);

        // Create an empty GameObject
        GameObject transparentCopy = new GameObject("BlockPreview");

        // Attack the good components
        var _blockMesh = transparentCopy.AddComponent<MeshFilter>();
        var _blockRenderer = transparentCopy.AddComponent<MeshRenderer>();
        var _blockScript = transparentCopy.AddComponent<BlocksDADPreview>();

        // Initialize everything
        _blockMesh.mesh = thisBlocksMesh;
        
        _blockScript.thisBlocksName = thisBlocksName;
        _blockScript._materials = blockPreviewSO._materials;
        _blockScript.nodeLayer = blockPreviewSO.nodeLayer;

        _blockRenderer.material = _blockScript._materials[0];
    }
}
