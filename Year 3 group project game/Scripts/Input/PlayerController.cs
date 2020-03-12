// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""5674b4e8-1d73-480b-a0ea-494e4ded920b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c5108b8a-26aa-4fc4-92c6-477325b009cb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""bad24792-e118-435b-af34-155a10cb74ab"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b03d1bb4-8012-4927-86c3-c2361782eddb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toss"",
                    ""type"": ""Button"",
                    ""id"": ""930456eb-2fd5-49bd-b692-cd82276a4bcc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""2a84f250-b0c8-4d5a-b9b7-9ad5f18e7bbc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""48657468-5d8f-4f06-bbd5-fa5b72f3d196"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""e13ed10e-5cc0-4c5a-8d87-70e0dadb2ef0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.5,behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dd976ea9-8b81-4eae-a2fd-6d864cdcb373"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""0af812cb-7b14-41ab-94af-4c3ae38f92e7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""050e7d4c-20d6-4487-89d1-d5640c9fc180"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3664fcf8-123a-4b54-a5f7-d0a66ee261f6"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b5c8206a-5025-494e-92ac-07d85853afac"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""38f9b450-1d20-4c9e-8e2b-dfaf7d7e29b9"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4e704ff1-1967-429f-b5eb-51ae26cf8589"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0557ff5a-6fe0-4811-a8a4-d759cf87fc30"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4869036-4bf6-4dfb-ba3e-868128fa5c4c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toss"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d4d7189-2b70-4b4a-9e49-850d44483bca"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toss"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cae967dc-75ea-4ff5-a9c4-5f5c61f96d8f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3275e1bf-47be-4640-8a29-6f38d20eab37"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""084d51e2-de39-4ff8-b19e-f3af91e755c3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""QTE"",
            ""id"": ""aacd3b51-d0b1-4e5a-b57e-d3bee2768d4c"",
            ""actions"": [
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""1ba6a6ec-31f3-453e-beda-89306f2aaf6d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""961aa867-a4c9-457d-bf27-27716a00b9e8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""706631f6-4242-439c-994c-b2b9874965e7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""ea804d83-7d78-4a7d-bf0d-bc2f4c968c17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""40b33ab2-d655-4ecc-b706-553312a33ce2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a479ab95-372e-4300-830a-f8181d364029"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3402eaff-b183-4c98-a4b9-17b0b4335186"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82b05d0d-5cba-4939-b92b-ebe73205c5ba"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BreakingFree"",
            ""id"": ""6008c894-6479-47bb-92da-6bc6735d1360"",
            ""actions"": [
                {
                    ""name"": ""BreakFree"",
                    ""type"": ""Button"",
                    ""id"": ""5614f8fb-bfb3-4307-8df6-bf5ac34652b2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c7a463ce-afd5-4494-8d27-58b65857ef18"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BreakFree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""609dde72-cf00-469b-b3a5-c8799e2c0bb1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BreakFree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""929baefa-c214-4d3a-a82d-2398523b0c74"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BreakFree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd02760a-624f-43a1-b62d-90c8e49f4294"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BreakFree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""111ae079-8c85-4e49-883c-a837293f14d3"",
            ""actions"": [
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""90c657bf-8e1c-4d8d-82b0-cfa06cf34814"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""befc1fcb-2521-4385-8482-e1efee24a67c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""9958dc47-e8e9-4765-afd5-c40723e3c255"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5b528a24-96f9-4e4b-a21e-28bc24da8339"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5b673d4a-53d9-4ff3-9047-797795ce6798"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21f9c631-6f65-4b37-ab23-7de7a1149f02"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f025ae0b-d6a2-487d-ad6e-ba9b691c3acf"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4865cbb9-396c-46b7-8b6a-b099dc64fe93"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3887126f-7f5a-4df7-81c8-07af1ae94d17"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Pause"",
            ""id"": ""c3610341-c9b7-4d51-ba4b-cbe6da75039a"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        m_Gameplay_Toss = m_Gameplay.FindAction("Toss", throwIfNotFound: true);
        m_Gameplay_Rotate = m_Gameplay.FindAction("Rotate", throwIfNotFound: true);
        m_Gameplay_Crouch = m_Gameplay.FindAction("Crouch", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        // QTE
        m_QTE = asset.FindActionMap("QTE", throwIfNotFound: true);
        m_QTE_Down = m_QTE.FindAction("Down", throwIfNotFound: true);
        m_QTE_Right = m_QTE.FindAction("Right", throwIfNotFound: true);
        m_QTE_Up = m_QTE.FindAction("Up", throwIfNotFound: true);
        m_QTE_Left = m_QTE.FindAction("Left", throwIfNotFound: true);
        // BreakingFree
        m_BreakingFree = asset.FindActionMap("BreakingFree", throwIfNotFound: true);
        m_BreakingFree_BreakFree = m_BreakingFree.FindAction("BreakFree", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Accept = m_Menu.FindAction("Accept", throwIfNotFound: true);
        m_Menu_Navigate = m_Menu.FindAction("Navigate", throwIfNotFound: true);
        m_Menu_Back = m_Menu.FindAction("Back", throwIfNotFound: true);
        m_Menu_Start = m_Menu.FindAction("Start", throwIfNotFound: true);
        // Pause
        m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Interact;
    private readonly InputAction m_Gameplay_Toss;
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_Crouch;
    private readonly InputAction m_Gameplay_Pause;
    public struct GameplayActions
    {
        private PlayerController m_Wrapper;
        public GameplayActions(PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputAction @Toss => m_Wrapper.m_Gameplay_Toss;
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @Crouch => m_Wrapper.m_Gameplay_Crouch;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                Toss.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToss;
                Toss.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToss;
                Toss.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToss;
                Rotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Rotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Rotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                Crouch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                Crouch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                Crouch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.canceled += instance.OnMove;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Interact.started += instance.OnInteract;
                Interact.performed += instance.OnInteract;
                Interact.canceled += instance.OnInteract;
                Toss.started += instance.OnToss;
                Toss.performed += instance.OnToss;
                Toss.canceled += instance.OnToss;
                Rotate.started += instance.OnRotate;
                Rotate.performed += instance.OnRotate;
                Rotate.canceled += instance.OnRotate;
                Crouch.started += instance.OnCrouch;
                Crouch.performed += instance.OnCrouch;
                Crouch.canceled += instance.OnCrouch;
                Pause.started += instance.OnPause;
                Pause.performed += instance.OnPause;
                Pause.canceled += instance.OnPause;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // QTE
    private readonly InputActionMap m_QTE;
    private IQTEActions m_QTEActionsCallbackInterface;
    private readonly InputAction m_QTE_Down;
    private readonly InputAction m_QTE_Right;
    private readonly InputAction m_QTE_Up;
    private readonly InputAction m_QTE_Left;
    public struct QTEActions
    {
        private PlayerController m_Wrapper;
        public QTEActions(PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Down => m_Wrapper.m_QTE_Down;
        public InputAction @Right => m_Wrapper.m_QTE_Right;
        public InputAction @Up => m_Wrapper.m_QTE_Up;
        public InputAction @Left => m_Wrapper.m_QTE_Left;
        public InputActionMap Get() { return m_Wrapper.m_QTE; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(QTEActions set) { return set.Get(); }
        public void SetCallbacks(IQTEActions instance)
        {
            if (m_Wrapper.m_QTEActionsCallbackInterface != null)
            {
                Down.started -= m_Wrapper.m_QTEActionsCallbackInterface.OnDown;
                Down.performed -= m_Wrapper.m_QTEActionsCallbackInterface.OnDown;
                Down.canceled -= m_Wrapper.m_QTEActionsCallbackInterface.OnDown;
                Right.started -= m_Wrapper.m_QTEActionsCallbackInterface.OnRight;
                Right.performed -= m_Wrapper.m_QTEActionsCallbackInterface.OnRight;
                Right.canceled -= m_Wrapper.m_QTEActionsCallbackInterface.OnRight;
                Up.started -= m_Wrapper.m_QTEActionsCallbackInterface.OnUp;
                Up.performed -= m_Wrapper.m_QTEActionsCallbackInterface.OnUp;
                Up.canceled -= m_Wrapper.m_QTEActionsCallbackInterface.OnUp;
                Left.started -= m_Wrapper.m_QTEActionsCallbackInterface.OnLeft;
                Left.performed -= m_Wrapper.m_QTEActionsCallbackInterface.OnLeft;
                Left.canceled -= m_Wrapper.m_QTEActionsCallbackInterface.OnLeft;
            }
            m_Wrapper.m_QTEActionsCallbackInterface = instance;
            if (instance != null)
            {
                Down.started += instance.OnDown;
                Down.performed += instance.OnDown;
                Down.canceled += instance.OnDown;
                Right.started += instance.OnRight;
                Right.performed += instance.OnRight;
                Right.canceled += instance.OnRight;
                Up.started += instance.OnUp;
                Up.performed += instance.OnUp;
                Up.canceled += instance.OnUp;
                Left.started += instance.OnLeft;
                Left.performed += instance.OnLeft;
                Left.canceled += instance.OnLeft;
            }
        }
    }
    public QTEActions @QTE => new QTEActions(this);

    // BreakingFree
    private readonly InputActionMap m_BreakingFree;
    private IBreakingFreeActions m_BreakingFreeActionsCallbackInterface;
    private readonly InputAction m_BreakingFree_BreakFree;
    public struct BreakingFreeActions
    {
        private PlayerController m_Wrapper;
        public BreakingFreeActions(PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @BreakFree => m_Wrapper.m_BreakingFree_BreakFree;
        public InputActionMap Get() { return m_Wrapper.m_BreakingFree; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BreakingFreeActions set) { return set.Get(); }
        public void SetCallbacks(IBreakingFreeActions instance)
        {
            if (m_Wrapper.m_BreakingFreeActionsCallbackInterface != null)
            {
                BreakFree.started -= m_Wrapper.m_BreakingFreeActionsCallbackInterface.OnBreakFree;
                BreakFree.performed -= m_Wrapper.m_BreakingFreeActionsCallbackInterface.OnBreakFree;
                BreakFree.canceled -= m_Wrapper.m_BreakingFreeActionsCallbackInterface.OnBreakFree;
            }
            m_Wrapper.m_BreakingFreeActionsCallbackInterface = instance;
            if (instance != null)
            {
                BreakFree.started += instance.OnBreakFree;
                BreakFree.performed += instance.OnBreakFree;
                BreakFree.canceled += instance.OnBreakFree;
            }
        }
    }
    public BreakingFreeActions @BreakingFree => new BreakingFreeActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Accept;
    private readonly InputAction m_Menu_Navigate;
    private readonly InputAction m_Menu_Back;
    private readonly InputAction m_Menu_Start;
    public struct MenuActions
    {
        private PlayerController m_Wrapper;
        public MenuActions(PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accept => m_Wrapper.m_Menu_Accept;
        public InputAction @Navigate => m_Wrapper.m_Menu_Navigate;
        public InputAction @Back => m_Wrapper.m_Menu_Back;
        public InputAction @Start => m_Wrapper.m_Menu_Start;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                Accept.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                Accept.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                Accept.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnAccept;
                Navigate.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                Navigate.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                Navigate.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                Back.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                Back.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                Back.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                Start.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnStart;
                Start.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnStart;
                Start.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnStart;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                Accept.started += instance.OnAccept;
                Accept.performed += instance.OnAccept;
                Accept.canceled += instance.OnAccept;
                Navigate.started += instance.OnNavigate;
                Navigate.performed += instance.OnNavigate;
                Navigate.canceled += instance.OnNavigate;
                Back.started += instance.OnBack;
                Back.performed += instance.OnBack;
                Back.canceled += instance.OnBack;
                Start.started += instance.OnStart;
                Start.performed += instance.OnStart;
                Start.canceled += instance.OnStart;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // Pause
    private readonly InputActionMap m_Pause;
    private IPauseActions m_PauseActionsCallbackInterface;
    public struct PauseActions
    {
        private PlayerController m_Wrapper;
        public PauseActions(PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_Pause; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
        public void SetCallbacks(IPauseActions instance)
        {
            if (m_Wrapper.m_PauseActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_PauseActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public PauseActions @Pause => new PauseActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnToss(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IQTEActions
    {
        void OnDown(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
    }
    public interface IBreakingFreeActions
    {
        void OnBreakFree(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnAccept(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
    }
    public interface IPauseActions
    {
    }
}
