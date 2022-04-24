using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTile : MonoBehaviour
{
    private RoomTemplates templates;
    public bool visited = false;
    private RoomMapState _tileState;
    public RoomMapState TileState { get => _tileState;
        set {
            if (_tileState == value) return;
            _tileState = value;
            var image = GetComponent<Image>();
            var color = GetComponent<Image>().color;
            switch (_tileState) {
                case RoomMapState.Current:
                    color.a = 1;
                    image.color = color;
                    image.sprite = templates.currentRoom;
                    break;
                case RoomMapState.Visited:
                    color.a = 1;
                    image.color = color;
                    image.sprite = templates.visitedRoom;
                    break;
                case RoomMapState.Unvisited:
                    color.a = 1;
                    image.color = color;
                    image.sprite = templates.unvisitedRoom;
                    break;
                case RoomMapState.Hidden:
                    color.a = 0;
                    image.color = color;
                    break;
            }
        }
    }

    private void Awake() {
        this.TileState = RoomMapState.Hidden;
        this.templates = RoomGenerator.Instance.Templates;
    }
}


public enum RoomMapState {
    Current = 1,
    Visited = 2,
    Unvisited = 3,
    Hidden = 4
}