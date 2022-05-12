using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeGeneration
{
    public class ItemRoom : Room
    {
        [SerializeField] private LootEffects crate;

        private void Start() {
            SetUpRoom();
            crate.GenerateItem(RoomGenerator.Instance.Pools.ItemRoom);
            mapTile.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1f,.92f,.016f, mapTile.alpha);
        }

        protected override void InitiateRoom()
        {
            crate.roomHasBeenCleared = true;

            hasBeenActivated = true;
        }

        override protected void OnTriggerEnter2D(Collider2D other) {
            base.OnTriggerEnter2D(other);
        }
    }
}
