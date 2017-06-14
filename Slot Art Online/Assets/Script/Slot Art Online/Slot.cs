using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame {
    [System.Serializable]
    public class Slot {
        [SerializeField]private List<int> slotItem;//list of item id
        [SerializeField]private IntVector2 spiningSpeed;
        [SerializeField]private bool useHorizontal;

        [SerializeField]private IntVector2 slotItemSize;// should be smaller than slow window

        [SerializeField]private ASlotDisplay slotDisplay;

        public Slot(List<int> slotItem, IntVector2 speed, bool useHorizontal, IntVector2 itemSize) {
            this.slotItem = slotItem;
            this.spiningSpeed = speed;
            this.useHorizontal = useHorizontal;
            this.slotItemSize = itemSize;
        }


        public IntVector2 CurrentPosition{ get; private set; }
        public int CurrentCenterIndex { get; private set;}
        private bool isSpinning = false;


        public void SetUpDisplay(ASlotDisplay display) {
            this.slotDisplay = display;
        }

        /// <summary>
        /// Shuffle this instance. for initialize
        /// </summary>
        public void Shuffle() {
            for(int i = 0; i < this.slotItem.Count; i++) {
                int index = Random.Range(0, this.slotItem.Count);
                int temp = this.slotItem[index];
                this.slotItem[index] = slotItem[i];
                this.slotItem[i] = temp;
            }
        }

        public IEnumerator SpinCoroutine() {
            this.isSpinning = true;
            while(this.isSpinning) {
                this.CurrentPosition += this.spiningSpeed;
                if(this.slotDisplay != null) {
                    this.slotDisplay.UpdatePosition(this.CurrentPosition);
                }
                yield return null;
            }
        }

        public void StopSpining() {
            this.isSpinning = false;
            if(this.useHorizontal) {
                this.Snapping(this.CurrentPosition.X, this.slotItemSize.X);
                return;
            }
            this.Snapping(this.CurrentPosition.Y, this.slotItemSize.Y);
        }

        private void Snapping(int currentDistance, int sizeLength) {
            this.CurrentCenterIndex = (currentDistance + sizeLength / 2) / sizeLength; 
        }
    }
}