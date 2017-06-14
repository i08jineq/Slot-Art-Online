using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkLordGame {
    [System.Serializable]
    public class IntVector2 {
        public int X;
        public int Y;

        public IntVector2(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        public static IntVector2 operator +(IntVector2 v1, IntVector2 v2) {
            return new IntVector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static IntVector2 operator -(IntVector2 v1, IntVector2 v2) {
            return new IntVector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static IntVector2 operator -(IntVector2 v2) {
            return new IntVector2(- v2.X, - v2.Y);
        }

        public static IntVector2 operator *(int x, IntVector2 v2) {
            return new IntVector2(x * v2.X, x * v2.Y);
        }

        public static explicit operator Vector2(IntVector2 v) {
            return new Vector2(v.X, v.Y);
        }

        public static IntVector2 Right{
            get{return new IntVector2(1, 0);}
        }
        public static IntVector2 Left{
            get{return new IntVector2(-1, 0);}
        }
        public static IntVector2 Up{
            get{return new IntVector2(0, 1);}
        }
        public static IntVector2 Down{
            get{return new IntVector2(0, -1);}
        }
    }
}