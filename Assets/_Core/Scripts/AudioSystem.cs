using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public StringWrapper LastMarker = new();
    }

    TimelineInfo _timelineInfo;
    GCHandle _timelineHandle;
    EVENT_CALLBACK _markerCallback;

    public event Action<string> OnMarker;
    public event Action OnComplete;

    public static string Marker;
    public void PlayOneShot(EventReference evt)
    {
        RuntimeManager.PlayOneShot(evt);
    }

    public void AssignEvents(EventInstance instance)
    {
        _timelineInfo = new();
        _timelineHandle = GCHandle.Alloc(_timelineInfo, GCHandleType.Pinned);
        _markerCallback = new EVENT_CALLBACK(MarkerEventCallback);
        instance.setUserData(GCHandle.ToIntPtr(_timelineHandle));
        instance.setCallback(_markerCallback, EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
    }

    public void Stop(EventInstance instance)
    {
        instance.setUserData(IntPtr.Zero);
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        _timelineHandle.Free();
    }

    [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
    private RESULT MarkerEventCallback(EVENT_CALLBACK_TYPE type, IntPtr _event, IntPtr parameters)
    {
        var instance = new EventInstance(_event);
        if (instance.getUserData(out IntPtr pTimelineInfo) != RESULT.OK)
        {
            UnityEngine.Debug.LogError("Timeline Callback error");
        }
        else if (pTimelineInfo != IntPtr.Zero)
        {
            GCHandle handle = GCHandle.FromIntPtr(pTimelineInfo);
            TimelineInfo info = (TimelineInfo)handle.Target;

            switch (type)
            {
                case EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var param = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(TIMELINE_MARKER_PROPERTIES));
                        info.LastMarker = param.name;
                        Marker = info.LastMarker;
                        OnMarker?.Invoke(Marker);
                        break;
                    }
                case EVENT_CALLBACK_TYPE.DESTROYED:
                    {
                        OnComplete?.Invoke();
                        handle.Free();
                        break;
                    }
            }
        }

        return RESULT.OK;
    }


}
