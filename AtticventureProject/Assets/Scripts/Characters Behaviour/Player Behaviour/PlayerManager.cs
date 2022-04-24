using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject onScreenControls;
    public InputKeyboard inputKeyboard;
    public InputGamepad inputGamepad;
    public PlayerData data;
    private GameObject player;
    public GameObject Player { get {
        if (player == null) 
                player = GameObject.FindGameObjectWithTag("Player");
        return player;
        }
    }

    private void OnEnable() {
            InputSystem.onDeviceChange += ConfigureDevices;
    }

    private void OnDisable() {
            InputSystem.onDeviceChange -= ConfigureDevices;
    }

    private void ConfigureDevices(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:           // New Device.
                Debug.Log(device.displayName);
                InputSystem.AddDevice(device);

                var allKeyboards = InputSystem.FindControls("<keyboard>");
                if (allKeyboards.ToArray().Length > 0) {
                        Player.AddComponent<MoveKeyboard>();
                        Player.AddComponent<ShootKeyboard>();
                }
                 allKeyboards.Dispose();
        
                var allGamepads = InputSystem.FindControls("<gamepad>");
                if (allGamepads.ToArray().Length > 0) {
                        Player.AddComponent<MoveKeyboard>();
                        Player.AddComponent<ShootKeyboard>();
                }
                allGamepads.Dispose();
                break;
            case InputDeviceChange.Disconnected:    // Device got unplugged.
                if(device.displayName == "Gamepad") {
                        Destroy(Player.GetComponent<MoveGamepad>());
                        Destroy(Player.GetComponent<ShootGamepad>());
                } if (device.displayName == "Keyboard") {
                        Destroy(Player.GetComponent<MoveKeyboard>());
                        Destroy(Player.GetComponent<ShootKeyboard>());
                }
                break;
            case InputDeviceChange.Reconnected:     // Device plugged back in.
                if(device.displayName == "Gamepad") {
                        Player.AddComponent<MoveGamepad>();
                        Player.AddComponent<ShootGamepad>();
                } if (device.displayName == "Keyboard") {
                        Player.AddComponent<MoveKeyboard>();
                        Player.AddComponent<ShootKeyboard>();
                }
                break;
        //     case InputDeviceChange.Removed:         // Remove from Input System entirely; by default, Devices stay in the system once discovered.
        //         break;
            default:                                // See InputDeviceChange reference for other event types.
                break;
        }
    }

    private void Awake()
    {
        data.Reset();

        inputKeyboard = new InputKeyboard();
        inputGamepad = new InputGamepad();

        var allKeyboards = InputSystem.FindControls("<keyboard>");
        if (allKeyboards.ToArray().Length > 0) {
                Player.AddComponent<MoveKeyboard>();
                Player.AddComponent<ShootKeyboard>();
        }
        allKeyboards.Dispose();
        
        var allGamepads = InputSystem.FindControls("<gamepad>");
        if (allGamepads.ToArray().Length > 0) {
                Player.AddComponent<MoveKeyboard>();
                Player.AddComponent<ShootKeyboard>();
        }
        allGamepads.Dispose();
        
#if UNITY_ANDROID || UNITY_IOS
        onScreenControls.SetActive(true);
#else
        onScreenControls.SetActive(false);
#endif
    }

}
