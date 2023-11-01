using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    public VFX[] VFXParticales;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayVFX(string name)
    {
        VFX s = Array.Find(VFXParticales, x => x.name == name);
        if (s == null)
        {
            Debug.Log("VFX not found");
        }

        else
        {
            Vector3 VFXpos = s.position.transform.position;
            ParticleSystem instance = Instantiate(s.particle, VFXpos, Quaternion.identity);
            

        }
    }
}