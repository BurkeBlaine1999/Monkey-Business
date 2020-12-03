using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Light Preset", menuName="Scriptables/Lighting Preset",order = 1)]
public class LightingPreset : ScriptableObject
{
    public Gradient AmbientColour;
    public Gradient DirectionalColour;
    public Gradient FogColour;
}
