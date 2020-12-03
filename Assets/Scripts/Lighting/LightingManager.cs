using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://youtu.be/m9hj9PdO328

//Executes certain methods in this class 
//even outside of the game
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //References
    [SerializeField]private Light DirectionalLight;
    [SerializeField]private LightingPreset Preset;
    //Variables
    [SerializeField,Range(0,900)]private float TimeOfDay;

    private void Update(){
        if(Preset == null){return;}

        if(Application.isPlaying){
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 900;
            UpdateLighting(TimeOfDay / 900);
        } 
        else{
            UpdateLighting(TimeOfDay / 900);
        }
            
    }

    private void UpdateLighting(float timePercent){

        RenderSettings.ambientLight = Preset.AmbientColour.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColour.Evaluate(timePercent);

        DirectionalLight.color = Preset.DirectionalColour.Evaluate(timePercent);
        DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) -90f,170f,0));
    }
    
    
    private void OnValidate(){
        if(DirectionalLight != null){
            return;
        }

        if(RenderSettings.sun != null){
            DirectionalLight = RenderSettings.sun;
        }
        else{
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights){
                if(light.type == LightType.Directional){
                    DirectionalLight = light;
                    return;
                }
            }

        }
    }
}
