using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VisualUtils
{
    public static void setSpriteDirection(SpriteRenderer renderer, bool isRight)
    {
        renderer.flipX = !isRight;
    }
}
