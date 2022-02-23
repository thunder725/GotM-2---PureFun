using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Movement Script for the camera: 
// Middle Mouse + Movement => Panning following the camera's plane
// Right Mouse + Movement => Rotation with a point on the ground close to the middle of the screen
// Scroll Wheel => Zoom in and out


// Why not use a Cinemachine? It has a way to move using inputs!
// Because I want the inputs to be only activated when pressing a button
// This is not possible with the new input system, even if we have what's called Modifiers

// Modifiers are "only register the input if I recieve both inputs", which is pretty useful for shortcut creation
// Example: Input "save" as S with Modifier Ctrl:   In code, the event "Save" will only get Invoked when Ctrl AND S are pressed at once

// Why can't I use it with the cinemachine then? Because the inputs then can "let pass" are limited to buttons and floats
// However I want to have Vector2 (mouse position delta), so I need to code the whole thing myself.

public class CameraController : MonoBehaviour
{
    [SerializeField] float panningSpeed, horizontalRotationSpeed, verticalRotationSpeed, zoomSpeed;
    Camera _camera;
    CameraInputs inputs;
    RaycastHit _hit;

    [SerializeField] Vector3 boundarySize;
    [SerializeField] LayerMask groundLayer;

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

            Panning();
        }
        if (IsRightMousePressed())
        {
            // ROTATION
            // Do a Rotate Around a point that will be placed

            Rotating();
        }
    }

    // ============ [CAMERA MOVEMENT METHODS] ==============

    void Panning()
    {
        // Process the Speeds
        float rightMovement = - MouseMovement().x * panningSpeed * Time.deltaTime;
        float upMovement = - MouseMovement().y * panningSpeed * Time.deltaTime;

        // Add the horizontal and vertical movements
        Vector3 newMovement = (rightMovement * transform.right) + (upMovement * transform.up);

        transform.position += newMovement;

        // Repositioning if outside of the Boundary:  the math is easy since everything is centered on (0,0,0)
        RepositionCamera();
    }


    void Rotating()
    {
        // Horizontal Rotating Around
        {
            // I'm going to use "Transform.RotateAround" because it does everything I want about it which is to rotate around a point in (0,0,0)
            // However it's a circular rotation around the point, so since the camera is above y=0, it's going to go downwards
            // To prevent this, I'm going to rotate around a point at the camera's height.
            // But since RotateAround modifies the rotation to look at the point, I'll re-align the camera afterwards

            // Find the point on the ground to rotate around
            Physics.Raycast(transform.position, transform.forward, out _hit, Mathf.Infinity, groundLayer);
            Vector3 rotationPoint = _hit.point;

            // Move it at the camera's height so the camera's height stays constant
            rotationPoint[1] = transform.position.y;
            
            // Process the Speed
            float horizontalRotationMovement = horizontalRotationSpeed * MouseMovement().x * Time.deltaTime;

            // Rotate
            transform.RotateAround(rotationPoint, Vector3.up, horizontalRotationMovement);

            // Realign myself
            rotationPoint[1] = 0;
            transform.LookAt(rotationPoint);
        }

        // Vertical Rotating
        {
            // Process the Speed
            float verticalRotationMovement = verticalRotationSpeed * -MouseMovement().y * Time.deltaTime;

            // Get the current rotation
            Vector3 currentRotation = transform.eulerAngles;

            currentRotation[0] = Mathf.Clamp(currentRotation.x + verticalRotationMovement, 15, 70);

            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, currentRotation.z);



        }

        RepositionCamera();
    }


    void Zoom(InputAction.CallbackContext c)
    {
        transform.position += transform.forward * zoomSpeed * ScrollValue();
        RepositionCamera();
    }


    void RepositionCamera()
    {
        // This is all because I did a rectangle instead of a sphere... there has to be another way of doing it
        // but to be honest it's like 1 in the morning so I'm too tired to know how to do it well

        // Check if the camera is under the floor
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y > boundarySize.y)
        {
            transform.position = new Vector3(transform.position.x, boundarySize.y, transform.position.z);
        }

        // Check if the camera is outside of the Boundary, and replace it
        if (transform.position.x > boundarySize.x / 2)
        {
            transform.position = new Vector3(boundarySize.x / 2, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -boundarySize.x / 2)
        {
            transform.position = new Vector3(- boundarySize.x / 2, transform.position.y, transform.position.z);
        }

        if (transform.position.z > boundarySize.z / 2)
        {
            transform.position =  new Vector3(transform.position.x, transform.position.y, boundarySize.z / 2);
        }
        else if (transform.position.z < - boundarySize.z / 2)
        {
            transform.position =  new Vector3(transform.position.x, transform.position.y, - boundarySize.z / 2);
        }
    }


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

    public float ScrollValue()
    { return Math.Sign(inputs.Default.ScrollWheel.ReadValue<float>()); }

    // ============= [INPUTS STUFF] =============

    void OnEnable()
    {
        inputs.Enable();
        inputs.Default.ScrollWheel.performed += Zoom;
    }

    void OnDisable()
    {
        inputs.Disable();
        inputs.Default.ScrollWheel.performed -= Zoom;
    }

    // ============ [MISC] ==============

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, boundarySize.y/2, 0), boundarySize);
    }
}
