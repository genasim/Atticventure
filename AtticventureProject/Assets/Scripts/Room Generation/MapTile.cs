using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazeGeneration
{
    public class MapTile : MonoBehaviour
    {
        public bool visited = false;
        public float alpha { get; private set; }
        private RoomTemplates templates;
        private RoomMapState _tileState;
        public RoomMapState TileState { get => _tileState;
            set {
                if (_tileState == value) return;
                _tileState = value;
                var image = GetComponent<Image>();
                var color = GetComponent<Image>().color;
                switch (_tileState) {
                    case RoomMapState.Current:
                        alpha = 1;
                        color.a = alpha;
                        image.color = color;
                        image.sprite = templates.currentRoom;
                        break;
                    case RoomMapState.Visited:
                        alpha = 1;
                        color.a = alpha;
                        image.color = color;
                        image.sprite = templates.visitedRoom;
                        break;
                    case RoomMapState.Unvisited:
                        alpha = 1;
                        color.a = alpha;
                        image.color = color;
                        image.sprite = templates.unvisitedRoom;
                        break;
                    case RoomMapState.Hidden:
                        alpha = 0;
                        color.a = alpha;
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
}