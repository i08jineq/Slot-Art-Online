using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame {
    public abstract class ASlotDisplay : MonoBehaviour {
        public abstract void Init(IntVector2 itemSize, IntVector2 spinDirection, int displayNumbersX = 1, int displayNumbersY = 1);
        public abstract void AddImage(Sprite sprite);
        public abstract void UpdateImageRage(List<Sprite> spriteList, int startIndex = 0);
        public abstract void UpdateImageAt(int index, Sprite sprite);
        public abstract void UpdatePosition(IntVector2 position);
    }
}