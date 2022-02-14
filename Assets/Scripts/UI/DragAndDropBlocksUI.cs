// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;

//     // OBSOLETE SCRIPT.
//     // DO NOT USE

// // https://www.youtube.com/watch?v=XCoDKZqa-PE
// public class DragAndDropBlocksUI : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
// {
//     // The name of the Building Block I'll drag and drop that will be passed to all of the spawn managers
//     [SerializeField] string thisBlocksName;

//     RectTransform rectTransform;
//     BuildManager buildManager; MousePointerScript mousePointer;
//     Image image;
//     Color defaultColor, transparentColor;
//     Vector2 startingPosition;

//     // =============== [GENERAL UNITY METHODS] ===============
//     void Awake()
//     {
//         // This script is obsolete so this needs to be displayed
//         Debug.LogError("You are using DragAndDropBlocksUI on an image, which is an obsolete script. Use BlocksButtonUI on a button instead.");

//         // Get references
//         rectTransform = transform as RectTransform;
//         image = GetComponent<Image>();

//         // Create the color references
//         defaultColor = image.color;
//         transparentColor = defaultColor;
//         transparentColor.a = .3f;
//     }

//     void Start()
//     {
//         // Singleton!
//         buildManager = BuildManager._instance;
//         mousePointer = MousePointerScript._instance;

//         // Get the position to return to when releasing the drag & drop
//         startingPosition = rectTransform.position;
//     }
     

//     // =========== [DRAG AND DROP METHODS] ============
    
//     //// TO-DO: Show a transparent version of the item drag & dropping

//     // Basically "Update" while drag and dropping, pre-built by Unity
//     public void OnDrag(PointerEventData eventData)
//     {
//         // I don't even know what this is, but it works, thanks again to Samyam
//         if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
//         {
//             // Move the image around
//             rectTransform.position = globalMousePosition;
//         }
//     }

//     // On the frame the drag & drop starts
//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         buildManager.SwitchNewSelectedBlock(thisBlocksName);
//         image.color = transparentColor;
//     }


//     // On the frame the drag & drop stops
//     public void OnEndDrag(PointerEventData eventData)
//     {
//         // Return to the old position
//         rectTransform.position = startingPosition;
//         image.color = defaultColor;

//         if (mousePointer.IsMouseInGamePosition())
//         {
//             // Build the block if the mouse is in the game view
//             buildManager.SpawnNewBlock(mousePointer.currentSelectedCube.transform.position, 0);
//         }
//     }
// }
