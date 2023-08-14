﻿using UnityEngine;
using System.Collections;
using Xft;

public class XWeaponTrailDemo : MonoBehaviour 
{
//    public Animation SwordAnimation;
//
//
//
//    public XWeaponTrail ProTrailDistort;
//    public XWeaponTrail ProTrailShort;
//    public XWeaponTrail ProTraillong;


    public XWeaponTrail SimpleTrail;



    //pre-init to save some performance.
    public void Start()
    {
//        ProTrailDistort.Init();
//        ProTrailShort.Init();
//        ProTraillong.Init();
        SimpleTrail.Init();
		SimpleTrail.Deactivate();
    }



    void OnGUI()
    {

        //GUI.Label(new Rect(60, 0, 500, 30), "Pro example requires unity Pro.");

        if (GUI.Button(new Rect(0, 0, 150, 30), "Activate Trail1"))
        {

//            ProTrailDistort.Deactivate();
//            ProTrailShort.Deactivate();
//            ProTraillong.Deactivate();
//
//            SwordAnimation.Play();
            SimpleTrail.Activate();
        }
        if (GUI.Button(new Rect(0, 30, 150, 30), "Stop Trail1"))
        {

            SimpleTrail.Deactivate();
        }
        if (GUI.Button(new Rect(0, 60, 150, 30), "Stop Trail1 Smoothly"))
        {
            SimpleTrail.StopSmoothly(0.3f);
        }


    }

}
