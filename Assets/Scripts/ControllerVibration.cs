using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerVibration : MonoBehaviour
{
    public AnimationCurve timerEndVibration;
    public AnimationCurve timerSecondVibration;
    public float frequency = 0.35f;
    
    AnimationCurve current = null;
    float playhead;
    float length;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.AddListener<TimerEndEvent>(HandleEvent);
        EventBus.AddListener<TimerSecondPassEvent>(HandleEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if(current != null) {
            OVRInput.SetControllerVibration(frequency, current.Evaluate(playhead), OVRInput.Controller.RTouch);
            playhead += Time.deltaTime;
            if(playhead >= length) {
                StopVibration();
            }
        }
    }

    public void PlayVibration(AnimationCurve curve)
    {
        if(curve.length == 0 || current != null)
            return;

        current = curve;
        playhead = 0;
        length = curve.keys[curve.length - 1].time;
    }

    public void StopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        current = null;
    }

    void HandleEvent(TimerEndEvent msg)
    {
        StopVibration();
        PlayVibration(timerEndVibration);
    }

    void HandleEvent(TimerSecondPassEvent msg)
    {
        if(msg.total - msg.seconds < 5) {
            PlayVibration(timerSecondVibration);
        }
    }
}
