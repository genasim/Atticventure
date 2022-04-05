using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : Singleton<Minimap>
{
    private static RoomTemplates templates;

    private bool showMap = false;
    private bool ShowMap {get => showMap;
        set {
            showMap = value;
            if (showMap)
                canvas.enabled = true;
            else
                canvas.enabled = false;
        }
    }
    private InputKeyboard inputKeyboard;
    private InputGamepad inputGamepad;

    private static Canvas canvas;

    private void Awake() {
        inputGamepad = PlayerManager.Instance.inputGamepad;
        inputKeyboard = PlayerManager.Instance.inputKeyboard;

        canvas = Minimap.Instance.GetComponent<Canvas>();
        templates = RoomGenerator.Instance.templates;
    }
    private void OnEnable() {
        inputGamepad.Player.Map.performed += _ => ShowMap = true;
        inputKeyboard.Player.Map.performed += _ => ShowMap = true;
        inputGamepad.Player.Map.canceled += _ => ShowMap = false;
        inputKeyboard.Player.Map.canceled += _ => ShowMap = false;
    }

    private void OnDisable() {
        inputGamepad.Player.Map.performed -= _ => ShowMap = true;
        inputKeyboard.Player.Map.performed -= _ => ShowMap = true;
        inputGamepad.Player.Map.canceled -= _ => ShowMap = false;
        inputKeyboard.Player.Map.canceled -= _ => ShowMap = false;
    }

    public static void AddRoomToMap(GameObject position, out MapTile mapTile) {
        var offset = new Vector2(position.transform.position.x * 110/22.25f, 
                                 position.transform.position.y * 110/12.5f);
        var go = Instantiate(templates.mapTile, (Vector2)canvas.transform.position + offset, Quaternion.identity, canvas.transform);
        mapTile = go.GetComponent<MapTile>(); 
    }
}
