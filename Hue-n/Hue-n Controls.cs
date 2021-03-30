// GENERATED AUTOMATICALLY FROM 'Assets/Developers/Hue-n/Hue-n Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @HuenControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @HuenControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Hue-n Controls"",
    ""maps"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""id"": ""6c938be9-433e-439e-a247-b1dd04fbdeff"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""11f83f9f-f5c6-493a-ac6c-2cc37ec2501e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4167a603-c8df-4401-91e8-440b2c22e829"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slam"",
                    ""type"": ""Button"",
                    ""id"": ""c83bbc6b-b926-429b-9d7e-64750c02a5f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""c6b7665d-5f3a-4718-92ff-a31b20e53cd9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""78efe64c-d10f-4fb7-a286-d5a5d1421f94"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f0e31276-df69-44f1-965a-00265e0ad3e2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e8a9a07e-a985-40c2-8e28-8d42f6e2e402"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8541308a-3900-4eba-9c06-c6f894d492e9"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Keyboard & Mouse
        m_KeyboardMouse = asset.FindActionMap("Keyboard & Mouse", throwIfNotFound: true);
        m_KeyboardMouse_Movement = m_KeyboardMouse.FindAction("Movement", throwIfNotFound: true);
        m_KeyboardMouse_Jump = m_KeyboardMouse.FindAction("Jump", throwIfNotFound: true);
        m_KeyboardMouse_Slam = m_KeyboardMouse.FindAction("Slam", throwIfNotFound: true);
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

    // Keyboard & Mouse
    private readonly InputActionMap m_KeyboardMouse;
    private IKeyboardMouseActions m_KeyboardMouseActionsCallbackInterface;
    private readonly InputAction m_KeyboardMouse_Movement;
    private readonly InputAction m_KeyboardMouse_Jump;
    private readonly InputAction m_KeyboardMouse_Slam;
    public struct KeyboardMouseActions
    {
        private @HuenControls m_Wrapper;
        public KeyboardMouseActions(@HuenControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_KeyboardMouse_Movement;
        public InputAction @Jump => m_Wrapper.m_KeyboardMouse_Jump;
        public InputAction @Slam => m_Wrapper.m_KeyboardMouse_Slam;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardMouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardMouseActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardMouseActions instance)
        {
            if (m_Wrapper.m_KeyboardMouseActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJump;
                @Slam.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSlam;
                @Slam.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSlam;
                @Slam.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSlam;
            }
            m_Wrapper.m_KeyboardMouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Slam.started += instance.OnSlam;
                @Slam.performed += instance.OnSlam;
                @Slam.canceled += instance.OnSlam;
            }
        }
    }
    public KeyboardMouseActions @KeyboardMouse => new KeyboardMouseActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IKeyboardMouseActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSlam(InputAction.CallbackContext context);
    }
}
