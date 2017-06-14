using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DarkLordGame {
    public class SlotMachine : MonoBehaviour {
        [SerializeField]private List<Sprite> imageList = new List<Sprite>();


        [SerializeField]private int spinningSpeed;
        [SerializeField]private bool useShuffleSpeed = true;

        [SerializeField]private SlotUIDisplay displayPrefab;
        [SerializeField]private bool AlignHorizontal;//else vertical



        //for easy debuging
        [ReadOnlyAttribute][SerializeField]private int SlotNumbers;
        [SerializeField]private IntVector2 slotSize;
        [ReadOnlyAttribute][SerializeField]private List<Slot> CreatedSlot;
        [ReadOnlyAttribute][SerializeField]private List<int> slotItemModel;
        [ReadOnlyAttribute][SerializeField]private IntVector2 speedVector;
        [SerializeField]private int positionOffset = 3;

        //for stoping
        [SerializeField]private float stopingDelay = 0.1f;
        public void Init() {
            this.SlotNumbers = 3;
            this.addAlignmentLayout();
            this.InitSpeed();

            this.slotItemModel = new List<int>();
            for(int i = 0; i < this.imageList.Count; i++) {
                this.slotItemModel.Add(i);
            }
            this.CreatedSlot = new List<Slot>();
            for(int i = 0; i < this.SlotNumbers; i++) {
                this.CreatedSlot.Add(this.CreateSlot());
            }
            this.ShuffleAll();
        }

        private Slot CreateSlot() {
            if(this.useShuffleSpeed) {
                this.speedVector = -this.speedVector;
            }
            Slot slot = new Slot(this.slotItemModel, this.speedVector, !this.AlignHorizontal, this.slotSize);
            SlotUIDisplay display = Instantiate<SlotUIDisplay>(displayPrefab, this.transform);

            IntVector2 direction = this.AlignHorizontal ? IntVector2.Up : IntVector2.Right;
            display.Init(this.slotSize, direction);
            for(int i = 0; i < this.imageList.Count; i++) {
                display.AddImage(this.imageList[i]);
            }
            slot.SetUpDisplay(display);
            return slot;
        }

        public void AddSlot() {
            if(this.CreatedSlot == null) {
                this.CreatedSlot = new List<Slot>();
            }
            this.CreatedSlot.Add(this.CreateSlot());
        }

        private void addAlignmentLayout() {
            if(this.AlignHorizontal) {
                HorizontalLayoutGroup group = this.gameObject.AddComponent<HorizontalLayoutGroup>();
                group.childControlWidth = false;
                group.padding.top = this.positionOffset * this.slotSize.Y;
                return;
            }
            VerticalLayoutGroup group2 = this.gameObject.AddComponent<VerticalLayoutGroup>();
            group2.childControlWidth = false;
            group2.padding.left = this.positionOffset * this.slotSize.Y;
        }


        private void InitSpeed() {
            if(AlignHorizontal) {//if align horizontal then spin vertical
                this.speedVector = new IntVector2(0, this.spinningSpeed);
                return;
            }//else
            this.speedVector = new IntVector2(this.spinningSpeed, 0);
        }

        public void SetVisibleAll(bool visible) {
            for(int i =0; i < this.CreatedSlot.Count; i++) {
                this.CreatedSlot[i].SetVisible(visible);
            }
        }

        public void SetVisbleAt(int index, bool visible) {
            if(this.CreatedSlot.Count <= index) {
                return;
            }
            this.CreatedSlot[index].SetVisible(visible);
        }

        public void ShuffleAll() {
            for(int i = 0; i < this.CreatedSlot.Count; i++) {
                this.CreatedSlot[i].Shuffle();
                List<int> itemNumber = this.CreatedSlot[i].SlotItem;
                for(int j = 0; j < itemNumber.Count; j++) {
                    this.CreatedSlot[i].slotDisplay.UpdateImageAt(j, this.imageList[itemNumber[j]]);
                }
            }
        }

        public void StartSpinningAll() {
            for(int i = 0; i < this.CreatedSlot.Count; i++) {
                StartCoroutine(this.CreatedSlot[i].SpinCoroutine());
            }
        }

        public void StopSpinningAll() {
            StartCoroutine(this.stopSpinningCoroutine());
        }

        private IEnumerator stopSpinningCoroutine() {
            for(int i = 0; i < this.CreatedSlot.Count; i++) {
                this.CreatedSlot[i].StopSpining();
                yield return new WaitForSeconds(this.stopingDelay);
            }
        }
    }
}