using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Thanks to https://gamedev.stackexchange.com/questions/74393/how-to-edit-key-value-pairs-like-a-dictionary-in-unitys-inspector
[CreateAssetMenu(fileName = "Dictionnary", menuName = "Create Name to Blocks Dictionnary")]
public class NameToBlocksDictionnary : ScriptableObject
{
    [System.Serializable]
    public class NameToBlocksEntry
    {
        public string name;
        public GameObject blockPrefab;
    }
    public NameToBlocksEntry[] entryList;

    public GameObject GetPrefab(string _name)
    {
        foreach (NameToBlocksEntry entry in entryList)
        {
            if (entry.name == _name)
            {
                return entry.blockPrefab;
            }
        }

        Debug.LogWarning("Trying to access an unexisting block:  " + _name);
        return null;
    }

}
