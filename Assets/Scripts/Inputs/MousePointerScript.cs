using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MousePointerScript : MonoBehaviour
{
    MouseInputs inputs;
    // This number indicates if the mouse position is over the left panel with the buttons or not using mouse position
    [SerializeField] float leftPanelThreshold;
    [SerializeField] LayerMask MouseHitLayer/*6*/, VisualizerCubesLayer /*7*/, BlocksLayer /*8*/;
    [SerializeField] bool showDebugSphere;
    GameObject previousCubeReferenced, previousSelectedBlock;
    public GameObject currentSelectedCube, currentSelectedBlock; 
    public GridNodesScript currentSelectedCubeScript;
    [SerializeField] GameObject DebugSphere; GameObject _sphere;
    public static MousePointerScript _instance;
    GraphicRaycaster raycaster; EventSystem eventSystem; PointerEventData pointerEvent;
    public UnityAction CurrentlySelectedCubeChanged;
    

    List<RaycastResult> results = new List<RaycastResult>();


    //  =========== [GENERAL UNITY METHODS] ================
    private void Awake()
    {
        inputs = new MouseInputs(); 
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        // Get References
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Start()
    {
        if (showDebugSphere)
        {
            _sphere = Instantiate(DebugSphere, Vector3.zero, Quaternion.identity);
            _sphere.transform.localScale = Vector3.one * 4; // 4 because the SphereCast RADIUS is 2 so the scale (diameter) of this one is 4}
        }

    }

    void Update()
    {
        // Light up the closest cube, the name gives it pretty well
        if (!PlayMode.isPlayMode){LightUpClosestCubeInUpdate();}
    }


    // ================ [POINTER METHODS] =================

    void LightUpClosestCubeInUpdate()
    {
        // Create a ray perpendicular to the camera to "output the mouse"
        Ray ray = Camera.main.ScreenPointToRay(MousePositionOnScreen());

        // Do a raycast that hits the ground where the visualizer cubes are placed
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, MouseHitLayer))
        {
            // Debug.Log("HIT THE GROUND");

            if (showDebugSphere)
            {_sphere.transform.position = raycastHit.point;}

            // Check the closest cubes. They're on an equilateral grid so the fartest point away is the center of the triangles
            // The center of the triangle is at almost 2 units of distance from the top... not entirely but this should touch from 1 to 3 triangles maximum
            var _cubes = Physics.OverlapSphere(raycastHit.point, 2, VisualizerCubesLayer);
            if (_cubes.Length != 0)
            {
                // Debug.Log("FOUND CUBES");

                currentSelectedCube = TransformPlus.ReturnClosest(_cubes, raycastHit.point);
                // Got the closest cube

                currentSelectedCubeScript = currentSelectedCube.GetComponent<GridNodesScript>();
                currentSelectedCubeScript.ChangeColor(Color.red);

                // Remove the color from the last one 
                if (currentSelectedCube != previousCubeReferenced)
                {
                    // This is only called if the currently selected cube is different from the previous one
                    // So I invoke the action for the block preview to check if it is in a correct spot.
                    // It is to prevent doing a raycast check every frame, only when moving around.
                    if (CurrentlySelectedCubeChanged != null)
                    {CurrentlySelectedCubeChanged.Invoke();}

                    if (previousCubeReferenced != null) // only used for the first time so it doesn't give an error
                    {previousCubeReferenced.GetComponent<GridNodesScript>().ChangeColor(Color.black);}

                    // Debug.Log("UNREDED CUBE");

                    previousCubeReferenced = currentSelectedCube;
                }
            }
        }
    }    

    void EnteringPlayMode(bool value)
    {
        if (value)
        {
            if (currentSelectedBlock != null)
            {
                // Toggle off the Outline
                currentSelectedBlock.GetComponent<Outline>().enabled = false;

                // Previous
                previousSelectedBlock = currentSelectedBlock;
                previousCubeReferenced = currentSelectedCube;

                // Removing the references
                currentSelectedCube = null;
                currentSelectedBlock = null;
            }
        }
        else
        {

        }
    }
    

    // =========== [PRESSING AND SCROLLING METHODS] =============

    void Scrolling(InputAction.CallbackContext c)
    {
        float _scrollValue = ScrollValue();
    }

    // This is pressed the frame the mouse is clicked
    void Clicked(InputAction.CallbackContext c)
    {
        // Values in pixel from 0:0 (bottom left) to 1920:1080 (top right)
        Vector2 mousePositionOnScreen = MousePositionOnScreen();

        // If the mouse is on the panel
        if (!IsMouseInGamePosition())
        {
            // Create a new pointer event
            pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.position = mousePositionOnScreen;

            // Empty the list of results
            results.Clear();

            // Check if we're clicking on a Block Button to start the drag & drop
            raycaster.Raycast(pointerEvent, results);

            if (results.Count != 0)
            {
                foreach(RaycastResult _r in results)
                {
                    // Debug.Log(_r.gameObject.name);

                    // Check for the one that we actually want
                    if (_r.gameObject.name.Contains("ButtonImage"))
                    {
                        // Get the script and launch the drag & drop
                        var _script = _r.gameObject.GetComponent<BlocksSpawningButtonUI>();
                        _script.StartDragAndDrop();
                        return;
                    }
                }
            }

        }
        else
        {
            // Raycast over the scene from the mouse, try to find a block under the mouse, and make appear a HUD to delete or rotate the block

            // Debug.Log("Scanning");

            Ray ray = Camera.main.ScreenPointToRay(MousePositionOnScreen());

            // Do a raycast that hits the visible block it encounters
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, BlocksLayer))
            {
                // Special case if Bridge Block (of course they do)
                // Because it needs to have rotated colliders, which means having them in children
                if (raycastHit.transform.gameObject.name.Contains("Bridge"))
                {
                    currentSelectedBlock = raycastHit.transform.parent.gameObject;
                }
                else 
                {
                    // Save the block 
                    currentSelectedBlock = raycastHit.transform.gameObject;
                }

                // Toggle on the Outline
                currentSelectedBlock.GetComponent<Outline>().enabled = true;                

                // Unselect previous block
                if (previousSelectedBlock != currentSelectedBlock)
                {
                    // Prevent errors
                    if (previousSelectedBlock != null)
                    {
                        // Toggle off the Outline
                        previousSelectedBlock.GetComponent<Outline>().enabled = false;    
                    }
                    
                    // Replace reference
                    previousSelectedBlock = currentSelectedBlock;
                }
            }
        }
        // Debug.Log("CLICKED");
    }

    // =========== [INPUTS ACCESSORS] ============

    public bool IsMouseInGamePosition()
    {
        // Values in pixel from 0:0 (bottom left) to 1920:1080 (top right)
        Vector2 mousePositionOnScreen = MousePositionOnScreen();

        // This returns the mouse's horizontal position from 0 (leftmost position) to 1 (rightmost position)
        float currentHorizontalPercentagePosition = mousePositionOnScreen.x / Camera.main.pixelWidth;

        // If the mouse is to the right of the panel, the mouse is in the game view, otherwise it is on the panel
        return currentHorizontalPercentagePosition >= leftPanelThreshold;
    }

    public Vector2 MousePositionOnScreen()
    {
        return inputs.Default.MousePos.ReadValue<Vector2>();
    }

    public bool isLeftMousePressed()
    {
        return inputs.Default.MouseLeftClick.ReadValue<float>() != 0;
    }

    public float ScrollValue()
    {
        // We ever only need if it's going up or down, so return just the sign
        // I'm using Math instead of Mathf so that if the value is 0 it will return 0 instead of 1 (like Mathf does)
        return Math.Sign(inputs.Default.ScrollWheel.ReadValue<float>());
    }


    // ============ [INPUTS STUFF] ==============
    private void OnEnable()
    { 
        inputs.Enable(); 
        inputs.Default.MouseLeftClick.started += Clicked;
        inputs.Default.ScrollWheel.started += Scrolling;

        PlayMode.PlayModeEvent += EnteringPlayMode;
    }
    private void OnDisable()
    { 
        inputs.Disable(); 
        inputs.Default.MouseLeftClick.started -= Clicked;
        inputs.Default.ScrollWheel.started -= Scrolling;

        PlayMode.PlayModeEvent -= EnteringPlayMode;
    }
}
