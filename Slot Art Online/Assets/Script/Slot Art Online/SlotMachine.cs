using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DarkLordGame {
    public class SlotMachine : MonoBehaviour {
        [SerializeField]private List<Sprite> imageList = new List<Sprite>();


        [SerializeField]private int spinningSpeed;

        [SerializeField]private SlotUIDisplay displayPrefab;
        [SerializeField]private bool AlignHorizontal;//else vertical

        //for easy debuging
        [ReadOnlyAttribute][SerializeField]private int SlotNumbers;
        [SerializeField]private IntVector2 slotSize;
        [ReadOnlyAttribute][SerializeField]private List<Slot> CreatedSlot;
        [ReadOnlyAttribute][SerializeField]private List<int> slotItem;
        [ReadOnlyAttribute][SerializeField]private IntVector2 speedVector;

        void Start() {
            this.Init();//test
        }

        public void Init() {
            this.SlotNumbers = 3;
            this.addAlignmentLayout();
            this.InitSpeed();

//            this.slotSize = new IntVector2(30, 30);
            this.slotItem = new List<int>{0, 1, 2, 3};
            this.CreatedSlot = new List<Slot>();
            for(int i = 0; i < this.SlotNumbers; i++) {
                this.CreatedSlot.Add(this.CreateSlot());
            }
        }

        private Slot CreateSlot() {
            Slot slot = new Slot(this.slotItem, this.speedVector, !this.AlignHorizontal, this.slotSize);
            SlotUIDisplay display = Instantiate<SlotUIDisplay>(displayPrefab, this.transform);

            IntVector2 direction = this.AlignHorizontal ? IntVector2.Up : IntVector2.Right;
            display.Init(this.slotSize, direction);
            for(int i = 0; i < this.imageList.Count; i++) {
                display.AddImage(this.imageList[i]);
            }
            slot.SetUpDisplay(display);
            return slot;
        }

        private void addAlignmentLayout() {
            if(this.AlignHorizontal) {
                this.gameObject.AddComponent<HorizontalLayoutGroup>();
                return;
            }
            this.gameObject.AddComponent<VerticalLayoutGroup>();
        }


        private void InitSpeed() {
            if(AlignHorizontal) {//if align horizontal then spin vertical
                this.speedVector = new IntVector2(0, this.spinningSpeed);
                return;
            }//else
            this.speedVector = new IntVector2(this.spinningSpeed, 0);
        }


    }
}