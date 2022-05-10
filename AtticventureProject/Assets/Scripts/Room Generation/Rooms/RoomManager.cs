using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MazeGeneration
{
    public abstract class RoomManager : MonoBehaviour
    {
        public RoomState state;
        public List<SpawnPoint> roomSpawnPoints;
        public List<BoxCollider2D> doorColliders;
        public List<Animator> doorAnimators;

        [SerializeField] protected bool hasBeenActivated = false;
        private CinemachineVirtualCamera camCinamachine;
        public MapTile mapTile;

        protected void SetUpRoom() {
            RoomGenerator.Instance.rooms.Add(transform.parent.gameObject);

            camCinamachine = FindObjectOfType<CinemachineVirtualCamera>();

            if (mapTile == null)
                Minimap.AddRoomToMap(gameObject.transform.parent.gameObject.transform, out mapTile);
        }

        abstract protected void InitiateRoom();

        virtual protected void OnTriggerEnter2D(Collider2D other) {
            if (other.TryGetComponent(out SpawnPoint sp)) {
                sp.spawned = true;
                //	Don't do sp.CleanDestroy() or Destroy(other.gameObject)!!!
                sp.mapTile = this.mapTile;
            }
            
            if (other.TryGetComponent(out PlayerMove player)) {
                Invoke("ConfigureMapTilesState", .15f);

                camCinamachine.LookAt = transform.parent;
                camCinamachine.Follow = transform.parent;

                AStarGridGraph.UpdateGraph(centre: transform.position);
                // PlayerShoot.currentRoom = this;

                if (!hasBeenActivated) {
                    InitiateRoom();
                }
            }
        }

        private void ConfigureMapTilesState() {
            mapTile.TileState = RoomMapState.Current;
            mapTile.visited = true;
            foreach (var point in roomSpawnPoints)
            {
                if (point.mapTile.visited)
                    point.mapTile.TileState = RoomMapState.Visited;
                else 
                    point.mapTile.TileState = RoomMapState.Unvisited;
            }
        }
    }
    public enum RoomState {
        Regular = 1,
        ItemRoom = 2,
        Boss = 3,
        BossLadder = 4
    };
}
