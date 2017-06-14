using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame {
    [System.Serializable]
    public class Slot {
        public List<int> SlotItem{get; private set;}//list of item id
        [SerializeField]private IntVector2 spiningSpeed;
        [SerializeField]private bool useHorizontal;

        [SerializeField]private IntVector2 slotItemSize;// should be smaller than slow window
        public ASlotDisplay slotDisplay{get; private set;}
        private int centerOffset;

        public Slot(List<int> slotItem, IntVector2 speed, bool useHorizontal, IntVector2 itemSize, int centerOffset = 0) {
            this.SlotItem = slotItem;
            this.spiningSpeed = speed;
            this.useHorizontal = useHorizontal;
            this.slotItemSize = itemSize;
            this.CurrentPosition = new IntVector2(0, 0);
            this.CurrentCenterIndex = centerOffset;
            this.centerOffset = centerOffset;
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
            for(int i = 0; i < this.SlotItem.Count; i++) {
                int index = Random.Range(0, this.SlotItem.Count);
                int temp = this.SlotItem[index];
                this.SlotItem[index] = SlotItem[i];
                this.SlotItem[i] = temp;
            }
        }

        public IEnumerator SpinCoroutine() {
            if(this.isSpinning) {
                yield break;
            }
            this.isSpinning = true;
            while(this.isSpinning) {
                this.CurrentPosition = this.CurrentPosition + this.spiningSpeed;
                if(this.slotDisplay != null) {
                    this.slotDisplay.UpdatePosition(this.CurrentPosition);
                }
                yield return null;
            }
        }

        public void StopSpining() {
            this.isSpinning = false;
            if(this.useHorizontal) {
                this.Snapping(this.CurrentPosition.X, this.slotItemSize.X, this.SlotItem.Count);
                return;
            }
            this.Snapping(this.CurrentPosition.Y, this.slotItemSize.Y, this.SlotItem.Count);
        }

        private void Snapping(int currentDistance, int sizeLength, int maxSize) {
            int modDistance = (MathHelper.Mod(currentDistance, maxSize));
            this.CurrentCenterIndex = modDistance / sizeLength + this.centerOffset; 
            this.CurrentPosition = (this.CurrentCenterIndex - centerOffset) * this.slotItemSize;

            if(this.slotDisplay != null) {
                this.slotDisplay.UpdatePosition(this.CurrentPosition);
            }
        }

        public void SetVisible(bool visible) {
            if(this.slotDisplay != null) {
                this.slotDisplay.gameObject.SetActive(visible);
            }
        }

        public int GetItemWithOffset(int offset = 0) {
            if(0 <= offset) {
                int max = Mathf.Min(offset, this.SlotItem.Count);
                return this.SlotItem[(this.CurrentCenterIndex + max) % this.SlotItem.Count];
            }
            int min = Mathf.Max(offset, -this.SlotItem.Count);
            return this.SlotItem[(this.CurrentCenterIndex + min + this.SlotItem.Count) % this.SlotItem.Count];
            
        }
    }
}