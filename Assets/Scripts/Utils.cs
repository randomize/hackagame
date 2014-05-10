using System;
using UnityEngine;
using System.Collections.Generic;

public static class Utils
{
    public static Vector2 ToVector2(this UnityEngine.Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }

    public static Vector2 Clone(this UnityEngine.Vector2 vector2)
    {
        return new Vector2(vector2.x, vector2.y);
    }

    public static Vector3 ToVector3(this UnityEngine.Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y);
    }

    public static Vector3 Clone(this UnityEngine.Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.y, vector3.z);
    }

    // Convert vector from screen to world coordinates, using main camera, trimming to 2D
    public static Vector2 ToWorldVector2(this UnityEngine.Vector2 vector2)
    {
        var v3 = vector2.ToWorldVector3();
        return new Vector2(v3.x, v3.y);
    }

    // Convert vector from screen to world coordinates, using main camera
    public static Vector2 ToWorldVector3(this UnityEngine.Vector2 vector2)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(vector2.x, vector2.y, 0));
    }

    public static double AngleToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
    public static float AngleToRadians(float angle)
    {
        return Mathf.PI * angle / 180.0f;
    }

    public static double RadiansToAngle(double angle)
    {
        return angle * (180.0 / Math.PI);
    }
    public static float RadiansToAngle(float angle)
    {
        return angle * (180.0f / Mathf.PI);
    }

    public static float TrueAngle(this Vector2 from, Vector2 to)
    {
        from = from.normalized;
        to = to.normalized;
        double result = Math.Atan2(from.x, from.y) - Math.Atan2(to.x, to.y);

        if (result < 0)
        {
            result += Math.PI * 2;
        }

        return (float)RadiansToAngle(result);
    }

    public static Vector2 NormaleVector2(this Vector2 vector)
    {
        return new Vector2(vector.y, vector.x);
    }

    public static Vector2 Rotate(this Vector2 vector, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        return (rotation * vector.ToVector3()).ToVector2();
    }

    public static int FirstIndex<T>(this IEnumerable<T> items, Predicate<T> predicate)
    {
        if (items == null) throw new ArgumentNullException("items");
        if (predicate == null) throw new ArgumentNullException("predicate");

        int retVal = 0;

        foreach (var item in items)
        {
            if (predicate(item)) return retVal;
            retVal++;
        }

        return -1;
    }

    public static Color MultiLerp(this Color[] scale, float p)
    {
        float step = 1.0f / (scale.Length - 1);

        for (int i = 0; i < scale.Length - 1; i++)
        {
            float ps = i * step;
            float pe = (i + 1) * step;
            if (p >= ps && p < pe)
            {
                return Color.Lerp(scale[i], scale[i + 1], (p - ps) / (pe - ps));
            }
        }

        return scale[scale.Length - 1]; // Return last
    }

    public static Vector2[][] GenerateTwoOffsets(Vector2[] points, float width, bool closed)
    {
        var N = points.Length;

        if (N <= 1)
        {
            return new Vector2[2][] { null, null };
        }

        // Generate normals
        var normals = new Vector2[closed ? N : N - 1];
        for (var k = 0; k < N - 1; k++)
        {
            normals[k] = (points[k + 1] - points[k]).normalized;
        }
        if (closed)
        {
            normals[N - 1] = (points[1] - points[N - 1]).normalized;
        }

        // TODO: optimize to not traverse twice
        var left = OffsetFrom(points, normals, width, closed);
        var right = OffsetFrom(points, normals, -width, closed);

        return new Vector2[2][] { left, right };
    }

    // P - _points
    // U - normalized vectors
    private static Vector2[] OffsetFrom(Vector2[] P, Vector2[] U, float width, bool closed)
    {

        int N = P.Length;
        float Lim = Mathf.Abs(100*width);

        // Results go here
        var H = new Vector2[N];

        // For the start point calculate the normal
        H[0].x = P[0].x - width * U[0].y;
        H[0].y = P[0].y + width * U[0].x;


        // if Closed For 1 to N-1 calculate the intersection of the offset lines
        // if not closed, than For 1 to N
        var C = closed ? N : N - 1;

        for (var k = 1; k < C; k++)
        {
            var L = width / (1 + Vector2.Dot(U[k], U[k - 1]));
            var La = Mathf.Abs(L);
            if (La > Lim )
            {
                //Debug.Log(L + " " + width);
                L = Lim*Mathf.Sign(L);
            }
            H[k].x = P[k].x - L * (U[k].y + U[k - 1].y);
            H[k].y = P[k].y + L * (U[k].x + U[k - 1].x);
        }

        if (!closed)
        {
            // For the end point use the normal
            H[N - 1].x = P[N - 1].x - width * U[N - 2].y;
            H[N - 1].y = P[N - 1].y + width * U[N - 2].x;
        }
        else
        {
            // Correct first point to last
            H[0] = H[N - 1];
        }

        return H;
    }

    public static void DrawCircleGizmo(Transform transform, Color color, float radius)
    {
        Gizmos.color = color;
        GL.PushMatrix();
        GL.MultMatrix(transform.worldToLocalMatrix);
        Gizmos.DrawWireSphere(transform.position, radius);
        GL.PopMatrix();
    }

    /// <summary>
    /// Create array of random variated colors
    /// </summary>
    /// <param name="source">original color</param>
    /// <param name="var">amount of variance</param>
    /// <param name="count"></param>
    /// <returns>resulting array of length "count"</returns>
    public static Color[] RandomOpacityVariace(this Color source, float var, int count = 2)
    {
        var cols = new Color[count];
        for (int i = 0; i < count; i++)
        {
            cols[i] = source;
            cols[i].a = Mathf.Clamp01(source.a + UnityEngine.Random.Range(-var, var));
        }
        return cols;
    }

    public static Color[] RandomHSVVariace(this Color source, float vVar, float sVar = 0, float hVar = 0, int count = 2)
    {
        // TODO: Randy - copy my code from MagneticPixels HSV classes and translate to C#
        return null;
    }

    public static int allParticlesMask = -1;
    public static int GetAllParticlesLayerMask()
    {
        if (allParticlesMask < 0)
        {
            allParticlesMask = 1 << LayerMask.NameToLayer("ParticleRed") |
        1 << LayerMask.NameToLayer("ParticleGreen") |
        1 << LayerMask.NameToLayer("ParticleBlue") |
        1 << LayerMask.NameToLayer("ParticleYellow") |
        1 << LayerMask.NameToLayer("ParticleCyan") |
        1 << LayerMask.NameToLayer("ParticleMagenta") |
        1 << LayerMask.NameToLayer("ParticleBlack") |
        1 << LayerMask.NameToLayer("ParticleWhite");
        }

        return allParticlesMask;
    }

    public static void BroadcastAll(string fun, System.Object msg)
    {
        GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in gos)
        {
            if (go && go.transform.parent == null)
            {
                go.gameObject.BroadcastMessage(fun, msg, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public static void BroadcastAll(string fun)
    {
        GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in gos)
        {
            if (go && go.transform.parent == null)
            {
                go.gameObject.BroadcastMessage(fun, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    /// <summary>
    /// Loads identity matrix to transform
    /// </summary>
    /// <param name="obj">This object</param>
    public static void ResetTransform(this Transform t)
    {
        t.localRotation = Quaternion.identity;
        t.localPosition = Vector3.zero;
        t.localScale = Vector3.one;
    }
}
