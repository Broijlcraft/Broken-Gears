// GENERATED AUTOMATICALLY FROM 'Assets/Input Actions/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""65a67abc-9863-4a48-920a-77f0c7360ce3"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""05c68ae5-6914-4210-af0e-52e9e4eb8f98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotationDeltaX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ce1a7574-c675-40b0-a06f-a476135a28fe"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotationDeltaY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7abfef7e-cc2d-4a5c-b51f-f6157c90469b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""4a9d7eae-f2c3-4a24-be86-211ced5a56f1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""20644213-d4ba-4c34-954c-acc47cdaae77"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5ae602e5-ecbb-40f1-8c60-a223d46b8078"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3f204b4f-4372-440a-8737-e6c76fa6e9d4"",
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
                    ""id"": ""c780849f-db95-4d4d-b3d0-7d139d16e13b"",
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
                    ""id"": ""c9c32ce2-1c4c-4f46-bc24-bdc112332831"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationDeltaX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59443a96-2b3f-4224-9d5d-8cc3fb0ae49b"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotationDeltaY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Movement = m_PlayerControls.FindAction("Movement", throwIfNotFound: true);
        m_PlayerControls_RotationDeltaX = m_PlayerControls.FindAction("RotationDeltaX", throwIfNotFound: true);
        m_PlayerControls_RotationDeltaY = m_PlayerControls.FindAction("RotationDeltaY", throwIfNotFound: true);
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

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Movement;
    private readonly InputAction m_PlayerControls_RotationDeltaX;
    private readonly InputAction m_PlayerControls_RotationDeltaY;
    public struct PlayerControlsActions
    {
        private @Inputs m_Wrapper;
        public PlayerControlsActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerControls_Movement;
        public InputAction @RotationDeltaX => m_Wrapper.m_PlayerControls_RotationDeltaX;
        public InputAction @RotationDeltaY => m_Wrapper.m_PlayerControls_RotationDeltaY;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @RotationDeltaX.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotationDeltaX;
                @RotationDeltaX.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotationDeltaX;
                @RotationDeltaX.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotationDeltaX;
                @RotationDeltaY.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotationDeltaY;
                @RotationDeltaY.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotationDeltaY;
                @RotationDeltaY.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRotationDeltaY;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @RotationDeltaX.started += instance.OnRotationDeltaX;
                @RotationDeltaX.performed += instance.OnRotationDeltaX;
                @RotationDeltaX.canceled += instance.OnRotationDeltaX;
                @RotationDeltaY.started += instance.OnRotationDeltaY;
                @RotationDeltaY.performed += instance.OnRotationDeltaY;
                @RotationDeltaY.canceled += instance.OnRotationDeltaY;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    public interface IPlayerControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRotationDeltaX(InputAction.CallbackContext context);
        void OnRotationDeltaY(InputAction.CallbackContext context);
    }
}