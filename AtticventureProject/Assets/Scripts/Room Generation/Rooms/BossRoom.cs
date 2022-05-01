using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration
{
    public class BossRoom : RoomManager
    {
        [SerializeField] private GameObject boss;

        private void Awake() {
            SetUpRoom();
        }

        protected override void InitiateRoom()
        {
          
            // throw new System.NotImplementedException();
        }
        
        override protected void OnTriggerEnter2D(Collider2D other) {
            base.OnTriggerEnter2D(other);
            
        }
    }
}
