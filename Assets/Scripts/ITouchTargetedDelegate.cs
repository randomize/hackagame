using System.Collections;
using UnityEngine;

public interface ITouchTargetedDelegate
{

    bool TouchBegan(Vector2 position, int fingerId);

    void TouchMoved(Vector2 position, int fingerId);

    void TouchEnded(Vector2 position, int fingerId);

    void TouchCanceled(Vector2 position, int fingerId);
}
