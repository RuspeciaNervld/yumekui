using UnityEngine;
public class Utils
{
    public static Vector2 Bezier(float t,Vector2 a,Vector2 b,Vector2 c) {
        Vector2 ab = Vector2.Lerp(a, b, t);
        Vector2 bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }
}
