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
        public override void Init(IntVector2 itemSize, IntVector2 direction) {
            this.imageList = new List<Image> ();
            this.slotItemSize = itemSize;
            this.spinDirection = direction;
            RectTransform trans = this.transform as RectTransform;
            trans.sizeDelta = new Vector2(slotItemSize.X, slotItemSize.Y);
            IntVector2 offset = new IntVector2(-direction.X * itemSize.X, -direction.Y * itemSize.Y);
            trans.anchoredPosition = (Vector2)offset;
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
            int dx = spinDirection.X;
            int dy = spinDirection.Y;
            IntVector2 maxDistance = this.imageList.Count * this.slotItemSize;

            for(int i = 0; i < this.imageList.Count; i++) {
                IntVector2 pos = position + i * this.slotItemSize;
                pos.X = pos.X % maxDistance.X * dx;
                pos.Y = pos.Y % maxDistance.Y * dy;
                this.imageList[i].transform.localPosition = (Vector2)pos;
            }
        }
    }
}