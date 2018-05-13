using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

public class PushButton : MonoBehaviour
#if UNITY_EDITOR
    , IPointerDownHandler, IPointerUpHandler
#endif
{
    public delegate void OnButtonEvent();
    public OnButtonEvent onButtonDown;
    public OnButtonEvent onButtonUp;

#if UNITY_EDITOR
    public bool SpaceBarPushEnabled = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        PushBegin();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PushRelease();
    }

    private void Update()
    {
        if(SpaceBarPushEnabled)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                PushBegin();
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                PushRelease();
            }
        }
    }
#endif

    private void PushBegin()
    {
        onButtonDown();
    }

    private void PushRelease()
    {
        onButtonUp();
    }

}