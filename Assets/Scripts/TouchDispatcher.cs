using System.Collections.Generic;
using UnityEngine;

public class TouchInfo
{
    public int numberHandledTouches = 0;
    public bool swallowsTouches = false;
    public int touchPriority = 0;
    public int[] fingerId = new int[10];
}

public class TouchDispatcher : Singleton<TouchDispatcher>
{
    public List<ITouchTargetedDelegate> handlers = new List<ITouchTargetedDelegate>();
    private List<TouchInfo> handlersInfo = new List<TouchInfo>();
    private bool mouseReleased;

    void Start()
    {
        mouseReleased = true;
    }

    public void addTargetedDelegate(ITouchTargetedDelegate handler, int inTouchPriority, bool inswallowsTouches)
    {

        if (handlers.Contains(handler))
            return;


        int i = 0;
        //searching for place to insert delegate
        for (i = 0; i < handlers.Count; i++)
        {
            if (handlersInfo[i].touchPriority > inTouchPriority)
                break;
        }

        handlers.Insert(i, handler);
        TouchInfo newTouchInfo = new TouchInfo();
        newTouchInfo.swallowsTouches = inswallowsTouches;
        newTouchInfo.touchPriority = inTouchPriority;
        handlersInfo.Insert(i, newTouchInfo);
    }

    public void removeDelegate(ITouchTargetedDelegate intarget)
    {
        //add one to remove list
        int index = handlers.IndexOf(intarget);

        if (index > -1)
        {
            handlersInfo.RemoveAt(index);
            handlers.Remove(intarget);
        }

    }

    public void removeAllDelegates()
    {
        handlers.Clear();
        handlersInfo.Clear();
    }

    private void Update()
    {
        if (handlers.Count > 0)
        {
            MakeDetectionMouseTouch();
        }
    }

    protected virtual void MakeDetectionMouseTouch()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        MakeDetectionMouse();
#else
       		MakeDetectionTouch();
#endif
    }

    protected virtual void MakeDetectionMouse()
    {
        //left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            if (!mouseReleased)
            {
                TouchCanceled(Input.mousePosition, 1);
            }
            else
            {
                if (TouchBegan(Input.mousePosition, 1))
                {
                    mouseReleased = false;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            TouchMoved(Input.mousePosition, 1);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseReleased = true;
            TouchEnded(Input.mousePosition, 1);
        }
    }

    protected virtual void MakeDetectionTouch()
    {
        int count = Input.touchCount;
        Touch touch;
        for (int i = 0; i < count; i++)
        {
            touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began: TouchBegan(touch.position, touch.fingerId); break;
                case TouchPhase.Moved: TouchMoved(touch.position, touch.fingerId); break;
                case TouchPhase.Ended: TouchEnded(touch.position, touch.fingerId); break;
                case TouchPhase.Canceled: TouchCanceled(touch.position, touch.fingerId); break;
            }
        }
    }

    public virtual bool TouchBegan(Vector2 position, int infingerId)
    {
        for (int i = 0; i < handlers.Count; i++)
        {
            if (handlers[i].TouchBegan(position, infingerId))
            {
                handlersInfo[i].fingerId[handlersInfo[i].numberHandledTouches] = infingerId;
                handlersInfo[i].numberHandledTouches++;

                if (handlersInfo[i].swallowsTouches)
                {
                    break;
                }

            }
        }
        return true;
    }

    public virtual void TouchMoved(Vector2 position, int infingerId)
    {
        for (int i = 0; i < handlers.Count; i++)
        {

            for (int j = 0; j < handlersInfo[i].numberHandledTouches && i < handlers.Count; j++)
            {
                if (handlersInfo[i].fingerId[j] == infingerId)
                    handlers[i].TouchMoved(position, infingerId);
            }

        }
    }

    public virtual void TouchEnded(Vector2 position, int infingerId)
    {
        for (int i = 0; i < handlers.Count; i++)
        {
            for (int j = 0; j < handlersInfo[i].numberHandledTouches; j++)
            {
                if (handlersInfo[i].fingerId[j] == infingerId)
                {
                    handlers[i].TouchEnded(position, infingerId);
                    handlersInfo[i].numberHandledTouches--;
                    for (int k = j; k < handlersInfo[i].numberHandledTouches; k++)
                    {
                        handlersInfo[i].fingerId[k] = handlersInfo[i].fingerId[k + 1];
                    }
                }
            }
        }
    }

    public virtual void TouchCanceled(Vector2 position, int infingerId)
    {
        for (int i = 0; i < handlers.Count; i++)
        {
            for (int j = 0; j < handlersInfo[i].numberHandledTouches; j++)
            {
                if (handlersInfo[i].fingerId[j] == infingerId)
                {
                    handlers[i].TouchCanceled(position, infingerId);
                    handlersInfo[i].numberHandledTouches--;
                    for (int k = j; k < handlersInfo[i].numberHandledTouches; k++)
                    {
                        handlersInfo[i].fingerId[k] = handlersInfo[i].fingerId[k + 1];
                    }
                }
            }
        }
    }

}