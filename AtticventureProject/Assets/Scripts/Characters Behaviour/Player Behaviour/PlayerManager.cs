using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject onScreenControls;
    public static InputKeyboard inputKeyboard;
    public static InputGamepad inputGamepad;
    public PlayerData data;
    [SerializeField] private PlayerData dataDefault;
    private GameObject player;

    private void OnEnable() {
            Debug.Log(InputSystem.devices[0].displayName);
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

                if(device.displayName == "Gamepad") {
                        player.AddComponent<MoveGamepad>();
                        player.AddComponent<ShootGamepad>();
                } else if (device.displayName == "Keyboard") {
                        player.AddComponent<MoveKeyboard>();
                        player.AddComponent<ShootKeyboard>();
                }
                break;
            case InputDeviceChange.Disconnected:    // Device got unplugged.
                if(device.displayName == "Gamepad") {
                        Destroy(player.GetComponent<MoveGamepad>());
                        Destroy(player.GetComponent<ShootGamepad>());
                } else if (device.displayName == "Keyboard") {
                        Destroy(player.GetComponent<MoveKeyboard>());
                        Destroy(player.GetComponent<ShootKeyboard>());
                }
                break;
            case InputDeviceChange.Reconnected:     // Plugged back in.
                if(device.displayName == "Gamepad") {
                        player.AddComponent<MoveGamepad>();
                        player.AddComponent<ShootGamepad>();
                } else if (device.displayName == "Keyboard") {
                        player.AddComponent<MoveKeyboard>();
                        player.AddComponent<ShootKeyboard>();
                }
                break;
        //     case InputDeviceChange.Removed:         // Remove from Input System entirely; by default, Devices stay in the system once discovered.
        //         break;
            default:                                // See InputDeviceChange reference for other event types.
                break;
        }
    }

    void Awake()
    {
        DefaultDataValues(data, dataDefault);
        PlayerMove.data = Object.Instantiate(data);
        PlayerShoot.data = Object.Instantiate(data);

        inputKeyboard = new InputKeyboard();
        inputGamepad = new InputGamepad();

        player = GameObject.FindGameObjectWithTag("Player");

        
#if UNITY_ANDROID || UNITY_IOS
        player.AddComponent<MoveGamepad>();
        player.AddComponent<ShootGamepad>();
        onScreenControls.SetActive(true);
#else
        onScreenControls.SetActive(false);
        
        player.AddComponent<MoveKeyboard>();
        player.AddComponent<ShootKeyboard>();
#endif
    }

    private void DefaultDataValues(PlayerData data, PlayerData dataDefault) {
        data.speed = dataDefault.speed;
        data.bulletSpeed = dataDefault.bulletSpeed;
        data.damage = dataDefault.damage;
        data.attackSpeed = dataDefault.attackSpeed;
        data.critRate = dataDefault.critRate;
        data.critDamage = dataDefault.critDamage;
    }
}
