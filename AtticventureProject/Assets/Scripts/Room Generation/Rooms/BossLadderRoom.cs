using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration
{
    public class BossLadderRoom : Room
    {
        public static bool keyFound = false;
        public static Room neighbouringRoom;
        public static GameObject keyRoom;
        public static BoxCollider2D entranceCol;
        public static Animator entranceAnim;

        public Item key;

        public static void UnlockRoom() {
            if (!keyFound) return;
            entranceCol.enabled = false;
            entranceAnim.SetBool("closeDoor", false);
            entranceAnim.SetBool("openDoor", true);
        }

        private void Awake() {
            SetUpRoom();
            keyRoom = RoomGenerator.PickRoom();
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
