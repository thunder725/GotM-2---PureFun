using UnityEngine;


[CreateAssetMenu(fileName = "Block Preview", menuName = "Scriptable Objects/Create Block Preview ScriptableObject")]

// Used to store the data used in BlocksDADPreviews, instead of making a prefab each time;
public class BlockPreviewScriptableObject : ScriptableObject
{
    // 0 = green semi-transparent
    // 1 = red semi-transparent
    public Material[] _materials;

    public LayerMask nodeLayer;


}
