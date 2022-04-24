using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private RoomState _state;
    public RoomState State { get => _state;
        set {
            if (_state == value) return;
            _state = value;
            switch (_state) {
                case RoomState.ItemRoom:
                    ItemRoom();
                    break;
            }
        }
    }
	public List<SpawnPoint> roomSpawnPoints;

    [HideInInspector] public List<GameObject> enemyList;
    private int enemiesCount = 0;
    private int EnemiesCount { get => enemyList.Count;
        set {
            enemiesCount = value;
            if (hasBeenActivated && enemiesCount == 0)
            {
                foreach (var border in doorColliders)
                    border.enabled = false;

                foreach (var animator in doorAnimators) {
                    animator.SetBool("closeDoor", false);
                    animator.SetBool("openDoor", true);
                }

                crate.roomHasBeenCleared = true;
                effects.PlusHP();
            }
        }
    }

    [SerializeField] private List<GameObject> spawnPoints;
    public List<BoxCollider2D> doorColliders;
    public List<Animator> doorAnimators;
    [SerializeField] private LootEffects crate;

    private LootEffects effects;
    [SerializeField] private bool hasBeenActivated = false;
    private CinemachineVirtualCamera camCinamachine;
    private MapTile mapTile;
    [SerializeField] private GameObject obstacles;

    private void Awake() {
        this.effects = new LootEffects();
        this.effects.playerHealth = PlayerManager.Instance.Player.GetComponent<HealthManager>();

        RoomGenerator.Instance.rooms.Add(transform.parent.gameObject);

        camCinamachine = FindObjectOfType<CinemachineVirtualCamera>();

        Minimap.AddRoomToMap(gameObject.transform.parent.gameObject, out mapTile);
    }

    void Update()
    {
        foreach (var enemy in enemyList)
        {
            if (!enemy) enemyList.Remove(enemy);
            EnemiesCount = enemyList.Count;
        }
    }


    public void InitiateRoom()
    {
        switch (this.State)
        {
            case RoomState.Regular:
                RegularRoom();
                break;

            case RoomState.Boss:
                break;
            case RoomState.ItemRoom:
                break;
        }
    }

    private void RegularRoom() {
        if (spawnPoints.Count == 0) {
            EnemiesCount = 0;
            return;
        }

        foreach (var spawnPoint in spawnPoints) {
            GameObject spawnedEnemy = Instantiate(spawnPoint.GetComponent<EnemySpawnPoints>().enemyToSpawn, 
                                                  spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
            enemyList.Add(spawnedEnemy);
        }

        foreach (var border in doorColliders) {
            border.enabled = true;
        }

        foreach (var item in doorAnimators) {
            item.SetBool("closeDoor", true);
            item.SetBool("openDoor", false);
        }

        crate.roomHasBeenCleared = false;
        hasBeenActivated = true;
    }

    private void ItemRoom() {
        for (int i = spawnPoints.Count - 1; i >= 0 ; i--) {
            Destroy(spawnPoints[i]);
            spawnPoints.Remove(spawnPoints[i]);
        }
        obstacles.SetActive(false);

        crate.GenerateItem(RoomGenerator.Instance.Pools.ItemRoom);
        crate.gameObject.transform.position = transform.position;
        crate.roomHasBeenCleared = true;

        hasBeenActivated = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out SpawnPoint sp)) {
			sp.spawned = true;
			//	Don't do sp.CleanDestroy() or Destroy(other.gameObject)!!!
            sp.mapTile = this.mapTile;
		}

		if (other.TryGetComponent(out RoomManager rm))
			Destroy(other.transform.parent.gameObject);
        
        if (other.TryGetComponent(out PlayerMove player)) {
            Invoke("ConfigureMapTilesState", .15f);

            camCinamachine.LookAt = transform.parent;
            camCinamachine.Follow = transform.parent;

            AStarGridGraph.UpdateGraph(centre: transform.position);
            PlayerShoot.currentRoom = this;

            if (!hasBeenActivated) {
                this.InitiateRoom();
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
    Boss = 2,
    ItemRoom = 3
};
