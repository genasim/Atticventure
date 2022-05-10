using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration
{
    public class BossLadderRoom : RoomManager
    {
        private RoomManager keyRoomManager;
        public Item key;
        [HideInInspector] public GameObject keyRoom;
        [HideInInspector] public BoxCollider2D entranceCol;
        [HideInInspector] public Animator entranceAnim;

        public void UnlockRoom() {
            entranceCol.enabled = false;
            entranceAnim.SetBool("closeDoor", false);
            entranceAnim.SetBool("openDoor", true);
        }

        private void Awake() {
            SetUpRoom();
            keyRoom = RoomGenerator.PickRoom();
            keyRoomManager = keyRoom.GetComponentInChildren<RoomManager>();
        }

        private void Start() {
            keyRoom.GetComponentInChildren<LootEffects>().item = key;   
            mapTile.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1,0,0,mapTile.alpha);
        }

        protected override void InitiateRoom()
        {
        }
        
        override protected void OnTriggerEnter2D(Collider2D other) {
            base.OnTriggerEnter2D(other);
           
        }
    }
}
