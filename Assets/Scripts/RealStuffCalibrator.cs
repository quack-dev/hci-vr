using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RealStuffCalibrator : MonoBehaviour
{
    public SteamVR_Action_Boolean Calibrate;
    public SteamVR_Input_Sources leftHand;
    public SteamVR_Behaviour_Pose leftPose;
    public SteamVR_Input_Sources rightHand;
    public SteamVR_Behaviour_Pose rightPose;
    public float CalibratedScaleFactor = 1.0f;
    public float TrueDistance;
    public float TrueYOffset;
    public float TrueZOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        Calibrate.AddOnStateDownListener(TriggerDown, leftHand);
        Calibrate.AddOnStateDownListener(TriggerDown, rightHand);
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (fromSource == SteamVR_Input_Sources.LeftHand)
        {
            if (Calibrate.GetState(rightHand))
            {
                calibrate();
            }
        }

        else if (fromSource == SteamVR_Input_Sources.RightHand)
        {
            if (Calibrate.GetState(leftHand))
            {
                calibrate();
            }
        }
    }

    public void calibrate()
    {
        float length = rightPose.transform.position.x - leftPose.transform.position.x;
        CalibratedScaleFactor = length / TrueDistance;
        transform.localScale = Vector3.one * CalibratedScaleFactor;
        Vector3 position = transform.localPosition;
        //x offset. Real offset is exactly 0
        position[0] = ((rightPose.transform.position.x + leftPose.transform.position.x) / 2);
        //y offset
        position[1] = ((rightPose.transform.position.y + leftPose.transform.position.y) / 2) - TrueYOffset;
        //z offset
        position[2] = ((rightPose.transform.position.z + leftPose.transform.position.z) / 2) + TrueZOffset;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
