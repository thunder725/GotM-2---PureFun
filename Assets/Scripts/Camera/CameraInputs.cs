// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Camera/CameraInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CameraInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraInputs"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""ac16da1b-adfc-403d-b052-7ddc0de058dd"",
            ""actions"": [
                {
                    ""name"": ""MouseRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""f8c48968-c311-4a18-bf10-6c0320d1683d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseMiddleClick"",
                    ""type"": ""Button"",
                    ""id"": ""c109cf08-54d1-433d-a1ef-6bbfb9c50f03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseMovement"",
                    ""type"": ""Value"",
                    ""id"": ""8c76e36f-d9f1-4142-a579-2105e981a86f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""Value"",
                    ""id"": ""865fc623-c419-40a7-a3c0-2dacfeea2150"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""af06a95e-7668-42e5-8f5d-bdef8f1ba0fb"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e91e0996-4302-40a9-b5ca-87e8f738aea4"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d61e9667-285b-4b7b-a771-097419c2314a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b18efdbb-7f6b-4a65-8a7a-85ea2405644d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_MouseRightClick = m_Default.FindAction("MouseRightClick", throwIfNotFound: true);
        m_Default_MouseMiddleClick = m_Default.FindAction("MouseMiddleClick", throwIfNotFound: true);
        m_Default_MouseMovement = m_Default.FindAction("MouseMovement", throwIfNotFound: true);
        m_Default_ScrollWheel = m_Default.FindAction("ScrollWheel", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_MouseRightClick;
    private readonly InputAction m_Default_MouseMiddleClick;
    private readonly InputAction m_Default_MouseMovement;
    private readonly InputAction m_Default_ScrollWheel;
    public struct DefaultActions
    {
        private @CameraInputs m_Wrapper;
        public DefaultActions(@CameraInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseRightClick => m_Wrapper.m_Default_MouseRightClick;
        public InputAction @MouseMiddleClick => m_Wrapper.m_Default_MouseMiddleClick;
        public InputAction @MouseMovement => m_Wrapper.m_Default_MouseMovement;
        public InputAction @ScrollWheel => m_Wrapper.m_Default_ScrollWheel;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @MouseRightClick.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseRightClick;
                @MouseRightClick.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseRightClick;
                @MouseRightClick.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseRightClick;
                @MouseMiddleClick.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseMiddleClick;
                @MouseMiddleClick.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseMiddleClick;
                @MouseMiddleClick.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseMiddleClick;
                @MouseMovement.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseMovement;
                @MouseMovement.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseMovement;
                @MouseMovement.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseMovement;
                @ScrollWheel.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseRightClick.started += instance.OnMouseRightClick;
                @MouseRightClick.performed += instance.OnMouseRightClick;
                @MouseRightClick.canceled += instance.OnMouseRightClick;
                @MouseMiddleClick.started += instance.OnMouseMiddleClick;
                @MouseMiddleClick.performed += instance.OnMouseMiddleClick;
                @MouseMiddleClick.canceled += instance.OnMouseMiddleClick;
                @MouseMovement.started += instance.OnMouseMovement;
                @MouseMovement.performed += instance.OnMouseMovement;
                @MouseMovement.canceled += instance.OnMouseMovement;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    public interface IDefaultActions
    {
        void OnMouseRightClick(InputAction.CallbackContext context);
        void OnMouseMiddleClick(InputAction.CallbackContext context);
        void OnMouseMovement(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
    }
}
