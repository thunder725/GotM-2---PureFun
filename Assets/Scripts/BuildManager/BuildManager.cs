using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager _instance;
    MousePointerScript mousePointer;
    string selectedBlockName;
    [SerializeField] LayerMask nodeLayer;
    [SerializeField] NameToBlocksDictionnary dictionnary;
    [SerializeField] AudioSource PlacingBlockAudio;

    // ============= [GENERAL UNITY METHODS] ================

    void Awake()
    {
        // Singleton !
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Debug.Log(dictionnary.GetPrefab("DefaultTube").name);
        mousePointer = MousePointerScript._instance;
    }

    // =========== [GENERAL BUILDING MANAGER METHODS] ============

    public GameObject GetPrefabFromName(string blockName)
    {
        return dictionnary.GetPrefab(blockName);
    }

    public void SwitchNewSelectedBlock(string newBlockName)
    {
        selectedBlockName = newBlockName;
    }

    public void SpawnNewBlock(GameObject cubeNode, float rotationAngle)
    {
        // Get the prefab you want to spawn with its name
        GameObject blockPrefab = GetPrefabFromName(selectedBlockName);

        // Get the node's script
        var _nodeScript = cubeNode.GetComponent<GridNodesScript>();

        // Save the height INDEX at which we'll build. (index so 0, 1, 2, 3 instead of 0, 2, 4, 6, etc.)
        var buildHeight = _nodeScript.GetLowestFreeSpace();

        // null exception
        if (buildHeight == -1)
        {Debug.LogError("Tried to input -1 to build: BuildManager line 55."); return;}

        // Creating the Building Block at the right position
        Instantiate(blockPrefab, _nodeScript.GetLowestFreeSpacePoint(), Quaternion.Euler(0, rotationAngle, 0));

        // Debug.Log("Build block at height " +_nodeScript.GetLowestFreeSpace());

        // Say to the node that I created another block on top of it
        _nodeScript.FillSpace(buildHeight);

        // Play sound with random pitch
        PlacingBlockAudio.pitch = Random.Range(.6f, 1.4f);
        PlacingBlockAudio.Play();

        // Check if the block is a particular one for node blocking reasons
        if (selectedBlockName == "BridgeTube")
        {
            // Create the vector in the direction of the bridge
            // -rotationAngle in sin because in my case, a bridge at 90° goes down, so at the equivalent of -90° in trigonometry
            // but the cos is the same one: start to the right. (I'm talking about a trigonometric circle)
            Vector3 raycastDirection = new Vector3(Mathf.Cos(rotationAngle * Mathf.Deg2Rad), 0, Mathf.Sin(- rotationAngle * Mathf.Deg2Rad)).normalized;

            // Don't start the raycast inside of the node or it'll hit itself, start it a bit outside
            Vector3 raycastStartPos = cubeNode.transform.position + raycastDirection;

            // Visualize the ray's trajectory
            // Debug.DrawRay(raycastStartPos, raycastDirection * 10000, Color.blue, 99);

            RaycastHit hit;
            // Do the raycast towards the good place
            Physics.Raycast(raycastStartPos, raycastDirection,out hit, Mathf.Infinity, nodeLayer);

            // Debug.Log("Hit a cube at a distance of " + hit.distance + " units");

            // Say that we filled this space in the other script
            hit.transform.GetComponent<GridNodesScript>().FillSpace(buildHeight);
        }
    }

}
