//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/MoveController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Arkanoid
{
    public partial class @MoveContrl : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MoveContrl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MoveController"",
    ""maps"": [
        {
            ""name"": ""PlayerController"",
            ""id"": ""c2684685-cabc-4156-81d8-bf287b3b8729"",
            ""actions"": [
                {
                    ""name"": ""PlayerFirstMove"",
                    ""type"": ""Value"",
                    ""id"": ""dc7f62af-d75b-4650-8109-01f87e394d71"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PlayerFirstShoot"",
                    ""type"": ""Button"",
                    ""id"": ""87469b32-b17b-4c70-bd54-f9f3904347c4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlayerSecondMove"",
                    ""type"": ""Value"",
                    ""id"": ""fdab6564-5b6b-4e92-9187-6f3fd8fc0652"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PlayerSecondShoot"",
                    ""type"": ""Button"",
                    ""id"": ""5331feb0-e7ab-476b-9cbf-0f642d525e36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""PlayerFirst"",
                    ""id"": ""0474970d-821a-45d9-9ae5-704f0c2d5d47"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerFirstMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b9c33258-6e59-4cd7-9271-381330ab20ec"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerFirstMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""83854f25-776b-4257-8665-c818a4471fcc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerFirstMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b6c4bcfe-1b2d-46bf-a7b3-4abe12504b2b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerFirstMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6db1769c-780a-4b7e-9a91-f758f87bb01f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerFirstMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""317e5fa1-c5c8-4015-af92-7526c49ea605"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerFirstShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""PlayerSecond"",
                    ""id"": ""c8882a26-fcb2-4cde-8f59-2299388adcf6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerSecondMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7c7956e7-f954-4771-a742-dafbf552207f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerSecondMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""02a46517-ed7a-491b-b4aa-f73acce286e4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerSecondMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""469083f1-5082-42cb-8abf-f70772168873"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerSecondMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""664bf9c7-eadf-487e-9a4e-768778bfe37f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerSecondMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fb5dea97-4eec-4596-98e6-11a4da00b2b5"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerSecondShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PlayerController
            m_PlayerController = asset.FindActionMap("PlayerController", throwIfNotFound: true);
            m_PlayerController_PlayerFirstMove = m_PlayerController.FindAction("PlayerFirstMove", throwIfNotFound: true);
            m_PlayerController_PlayerFirstShoot = m_PlayerController.FindAction("PlayerFirstShoot", throwIfNotFound: true);
            m_PlayerController_PlayerSecondMove = m_PlayerController.FindAction("PlayerSecondMove", throwIfNotFound: true);
            m_PlayerController_PlayerSecondShoot = m_PlayerController.FindAction("PlayerSecondShoot", throwIfNotFound: true);
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
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // PlayerController
        private readonly InputActionMap m_PlayerController;
        private IPlayerControllerActions m_PlayerControllerActionsCallbackInterface;
        private readonly InputAction m_PlayerController_PlayerFirstMove;
        private readonly InputAction m_PlayerController_PlayerFirstShoot;
        private readonly InputAction m_PlayerController_PlayerSecondMove;
        private readonly InputAction m_PlayerController_PlayerSecondShoot;
        public struct PlayerControllerActions
        {
            private @MoveContrl m_Wrapper;
            public PlayerControllerActions(@MoveContrl wrapper) { m_Wrapper = wrapper; }
            public InputAction @PlayerFirstMove => m_Wrapper.m_PlayerController_PlayerFirstMove;
            public InputAction @PlayerFirstShoot => m_Wrapper.m_PlayerController_PlayerFirstShoot;
            public InputAction @PlayerSecondMove => m_Wrapper.m_PlayerController_PlayerSecondMove;
            public InputAction @PlayerSecondShoot => m_Wrapper.m_PlayerController_PlayerSecondShoot;
            public InputActionMap Get() { return m_Wrapper.m_PlayerController; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerControllerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerControllerActions instance)
            {
                if (m_Wrapper.m_PlayerControllerActionsCallbackInterface != null)
                {
                    @PlayerFirstMove.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerFirstMove;
                    @PlayerFirstMove.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerFirstMove;
                    @PlayerFirstMove.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerFirstMove;
                    @PlayerFirstShoot.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerFirstShoot;
                    @PlayerFirstShoot.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerFirstShoot;
                    @PlayerFirstShoot.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerFirstShoot;
                    @PlayerSecondMove.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerSecondMove;
                    @PlayerSecondMove.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerSecondMove;
                    @PlayerSecondMove.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerSecondMove;
                    @PlayerSecondShoot.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerSecondShoot;
                    @PlayerSecondShoot.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerSecondShoot;
                    @PlayerSecondShoot.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnPlayerSecondShoot;
                }
                m_Wrapper.m_PlayerControllerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PlayerFirstMove.started += instance.OnPlayerFirstMove;
                    @PlayerFirstMove.performed += instance.OnPlayerFirstMove;
                    @PlayerFirstMove.canceled += instance.OnPlayerFirstMove;
                    @PlayerFirstShoot.started += instance.OnPlayerFirstShoot;
                    @PlayerFirstShoot.performed += instance.OnPlayerFirstShoot;
                    @PlayerFirstShoot.canceled += instance.OnPlayerFirstShoot;
                    @PlayerSecondMove.started += instance.OnPlayerSecondMove;
                    @PlayerSecondMove.performed += instance.OnPlayerSecondMove;
                    @PlayerSecondMove.canceled += instance.OnPlayerSecondMove;
                    @PlayerSecondShoot.started += instance.OnPlayerSecondShoot;
                    @PlayerSecondShoot.performed += instance.OnPlayerSecondShoot;
                    @PlayerSecondShoot.canceled += instance.OnPlayerSecondShoot;
                }
            }
        }
        public PlayerControllerActions @PlayerController => new PlayerControllerActions(this);
        public interface IPlayerControllerActions
        {
            void OnPlayerFirstMove(InputAction.CallbackContext context);
            void OnPlayerFirstShoot(InputAction.CallbackContext context);
            void OnPlayerSecondMove(InputAction.CallbackContext context);
            void OnPlayerSecondShoot(InputAction.CallbackContext context);
        }
    }
}
