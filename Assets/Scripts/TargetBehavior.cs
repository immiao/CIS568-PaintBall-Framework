﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;
using System;

public class TargetBehavior : MonoBehaviour, ITrackableEventHandler
{

    public Button TrackButton;
    public Button ShotTopButton;
    public GyroController CameraGyro;
    public Transform ARCameraTransform;
    public GameObject ARCamera;
    bool tracked = false;


    void ResumeTracking()
    {
        Tracker imageTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        imageTracker.Start();
    }

    void PauseTracking()
    {
        Tracker imageTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        imageTracker.Stop();
    }

    // Use this for initialization
    void Start () {
        CameraGyro = GetComponent<GyroController>();
        CameraGyro.Paused = true;
        CameraGyro.ControlledObject = GameObject.FindWithTag("ARCamera");
       
        Debug.Assert(CameraGyro.ControlledObject != null); 

        var mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        
        TrackButton.onClick.AddListener(ResumeTracking);

        ARCamera = GameObject.Find("ARCamera");
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        switch (newStatus)
        {
            case TrackableBehaviour.Status.TRACKED:
                // Target in camera
                // TODO-2.b 
                // Recalibrate reference quaternions at GyroController
                //   and switch the Control of the camera between Vuforia and GyroController.
                // You may want to toggle GyroController.Paused .
                tracked = true;


                TrackButton.image.color = new Color(0.4f, 1, 0.1f, 0.5f);
                break;
            case TrackableBehaviour.Status.EXTENDED_TRACKED:
                // Target not in camera, but Vuforia can still calculate position and orientation
                //   and update ARCamera.
                // TODO-2.b
                tracked = false;
                CameraGyro.ControlledObject = ARCamera;

                TrackButton.image.color = new Color(0.7f, 0.5f, 0.1f, 0.5f);
                break;
            default:
                tracked = false;
                // TODO-2.b
                CameraGyro.ControlledObject = ARCamera;
                TrackButton.image.color = new Color(1, 0.1f, 0.1f, 0.5f);
                break;
        }
        
    }
}
