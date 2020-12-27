using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class AnimCall : MonoBehaviour
{
    [Tooltip("Call時に実行したいイベント"), SerializeField]
    UnityEvent callEvents = new UnityEvent();
    public void Call()
    {
        callEvents.Invoke();
    }

}
