using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Assisted : Session
{
    private static float toneDuration= 2.5f;

    public Assisted( int frequency, float volume, float prePlayDelay) : base(frequency, volume)
    {
    }

}