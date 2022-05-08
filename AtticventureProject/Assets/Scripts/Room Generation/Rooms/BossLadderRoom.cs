using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration
{
    public class BossLadderRoom : RoomManager
    {
        [SerializeField] private GameObject keyRoom;

        private void Awake() {
            SetUpRoom();
        }

        private void Start() {
            keyRoom = RoomGenerator.PickRoom();
            keyRoom.GetComponentInChildren<LootEffects>();
        }

        protected override void InitiateRoom()
        {
        }
        
        override protected void OnTriggerEnter2D(Collider2D other) {
            base.OnTriggerEnter2D(other);
           
        }
    }
}
