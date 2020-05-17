using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonScripts
{
    public class LightControllScipt : MonoBehaviour
    {
        public bool InitOnStart = false;
        public float Range = 10;
        public float Intensity = 0.5f;
        public bool IsOn = false;
    
        public float DRange = 5;
        public float DIntensity = 5;
    
        [SerializeField]
        public Light[] Lights = new Light[0];
    
        [SerializeField]
        public Light[] DecorLights = new Light[0]; 
        void Start()
        {
            
            foreach (Light l in Lights)
            {
                if (IsOn)
                    l.enabled = true;
                else
                    l.enabled = false;
    
                if (!InitOnStart)
                    continue;
                l.intensity = Intensity;
                l.range = Range;
            }
    
            foreach(Light l in DecorLights)
            {
                if (IsOn)
                    l.enabled = true;
                else
                    l.enabled = false;
    
                if (!InitOnStart)
                    continue;
                l.intensity = DIntensity;
                l.range = DRange;
            }
        }
    }
}