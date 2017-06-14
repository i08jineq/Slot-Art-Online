using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DarkLordGame {
    /// <summary>
    /// Slot display. use this with UI
    /// </summary>
    public class SlotUIDisplay : ASlotDisplay {
        private List<Image> imageList;
        private IntVector2 slotItemSize;
        private IntVector2 spinDirection;

        /// <summary>
        /// Init the specified itemSize and dispayNumbers.
        /// </summary>
        /// <param name="itemSize">Item size.</param>
        /// <param name="dispayNumbers">minimum is 1.</param>
        public override void Init(IntVector2 itemSize, IntVector2 direction, int displayNumbersX = 1, int displayNumbersY = 1) {
            this.imageList = new List<Image> ();
            this.slotItemSize = itemSize;
            this.spinDirection = direction;
            int x = Mathf.Max(1, displayNumbersX);
            int y = Mathf.Max(1, displayNumbersY);
            RectTransform trans = this.transform as RectTransform;
            trans.sizeDelta = new Vector2(slotItemSize.X * x, slotItemSize.Y * y);
        }

        public override void AddImage(Sprite sprite) {
            if(this.imageList == null) {
                this.imageList = new List<Image>();
            }
            GameObject obj = new GameObject("image");
            obj.AddComponent<CanvasRenderer>();
            Image img = obj.AddComponent<Image>();
            img.sprite = sprite;
            this.imageList.Add(img);

            //set stransform
            obj.transform.SetParent(this.transform);
            img.rectTransform.sizeDelta = (Vector2)this.slotItemSize;
            int slbingIndex = obj.transform.GetSiblingIndex();
            img.rectTransform.anchoredPosition = new Vector2(this.slotItemSize.X * spinDirection.X * slbingIndex, this.slotItemSize.Y * spinDirection.Y * slbingIndex);
        }

        public override void UpdateImageRage(List<Sprite> spriteList, int startIndex = 0) {
            if(this.imageList == null) {
                return;
            }

            for(int i = startIndex; i < this.imageList.Count && i < spriteList.Count; i++) {
                this.imageList[i].sprite = spriteList[i];
            }
        }

        public override void UpdateImageAt(int index, Sprite sprite) {
            if(this.imageList == null) {
                return;
            }
            if(this.imageList.Count <= index) {
                return;
            }
            this.imageList[index].sprite = sprite;
        }

        public override void UpdatePosition(IntVector2 position) {
//            this.transform.localPosition = (Vector2)position;
        }
    }
}