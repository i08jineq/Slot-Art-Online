using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper {

    public static int Mod(int a, int b) {
        return (a%b + b)%b;
    }
}
