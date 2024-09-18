using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LibraryUtils
{
    public static Vector3 convertVector(Vector2 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }

    public static Vector2 convertVector(Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }
}
