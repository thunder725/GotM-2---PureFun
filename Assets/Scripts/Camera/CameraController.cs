using UnityEngine;
using UnityEngine.InputSystem;

// Movement Script for the camera: 
// Middle Mouse + Movement => Panning following the camera's plane
// Right Mouse + Movement => Rotation with a point on the ground close to the middle of the screen

public class CameraController : MonoBehaviour
{
    [SerializeField] float panningSpeed, rotationSpeed;
    Camera _camera;
    CameraInputs inputs;

    // ============== [GENERAL UNITY METHODS] ================

    void Awake()
    {
        inputs = new CameraInputs();
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (IsMiddleMousePressed())
        {
            // PANNING
            // Use Transform.Up and Transform.Right instead of a general Vector so it's following the camera's plane
        }
        if (IsRightMousePressed())
        {
            // ROTATION
            // Do a Rotate Around a point that will be placed
        }
    }

    // ============ [CAMERA MOVEMENT METHODS] ==============

    // ============= [INPUTS ACCESSORS] =================

    // Returns true if the Middle Mouse Button is pressed (the scroll wheel)
    public bool IsMiddleMousePressed()
    { return inputs.Default.MouseMiddleClick.ReadValue<float>() != 0; }

    // Returns true if the Right Mouse Button is pressed
    public bool IsRightMousePressed()
    { return inputs.Default.MouseRightClick.ReadValue<float>() != 0; }

    // Returns the Mouse movement
    public Vector2 MouseMovement()
    { return inputs.Default.MouseMovement.ReadValue<Vector2>(); }


    // ============= [INPUTS STUFF] =============

    void OnEnable()
    {
        inputs.Enable();
    }

    void OnDisable()
    {
        inputs.Disable();
    }

}
