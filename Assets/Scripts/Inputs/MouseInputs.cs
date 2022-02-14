// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/MouseInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MouseInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MouseInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MouseInputs"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""1874d768-0597-4ffe-9f98-01b1a8925f04"",
            ""actions"": [
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""5542005e-7a64-4294-af5b-1b52fc93e791"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""3a175d6f-ff6a-4b22-89d4-9e1cf6def995"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""Value"",
                    ""id"": ""58c8e6bb-897e-4274-9052-ba79f954c9d4"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9ca03025-d476-447d-a49f-078e2037b6e9"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""671c5961-1701-4232-9bb6-2e7d8d5581c0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""219d8670-bf43-4bb7-ac81-35e6bd709106"",
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
        m_Default_MousePos = m_Default.FindAction("MousePos", throwIfNotFound: true);
        m_Default_MouseLeftClick = m_Default.FindAction("MouseLeftClick", throwIfNotFound: true);
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
    private readonly InputAction m_Default_MousePos;
    private readonly InputAction m_Default_MouseLeftClick;
    private readonly InputAction m_Default_ScrollWheel;
    public struct DefaultActions
    {
        private @MouseInputs m_Wrapper;
        public DefaultActions(@MouseInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePos => m_Wrapper.m_Default_MousePos;
        public InputAction @MouseLeftClick => m_Wrapper.m_Default_MouseLeftClick;
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
                @MousePos.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMousePos;
                @MouseLeftClick.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseLeftClick;
                @MouseLeftClick.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseLeftClick;
                @MouseLeftClick.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnMouseLeftClick;
                @ScrollWheel.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
                @MouseLeftClick.started += instance.OnMouseLeftClick;
                @MouseLeftClick.performed += instance.OnMouseLeftClick;
                @MouseLeftClick.canceled += instance.OnMouseLeftClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    public interface IDefaultActions
    {
        void OnMousePos(InputAction.CallbackContext context);
        void OnMouseLeftClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
    }
}
