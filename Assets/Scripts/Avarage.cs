using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Avarage{ }
    
public static class ListExtensions {
    public static float Average(this List<float> list) => list.Sum() / list.Count;
    public static Vector2 Average(this List<Vector2> list) {
        var sum = list.Aggregate(Vector2.zero, (current, vector2) => current + vector2);
        return sum / list.Count;
    }

    public static List<T> Except<T>(this List<T> list, T elem) {
        var result = new List<T>(list);
        result.RemoveAll(t => t.Equals(elem));
        return result;
    }
}

