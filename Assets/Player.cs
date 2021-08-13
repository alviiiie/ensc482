using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem Lightning = null;
    [SerializeField] ParticleSystem Snow = null;
    [SerializeField] ParticleSystem Fire = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playLightning();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playFire();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playSnow();
        }
    }

    public void playLightning()
    {
        //play the lightning graphics
        Lightning.Play();
    }

    public void playFire()
    {
        //play the fire graphics
        Fire.Play();
    }


    public void playSnow()
    {
        //play the snow graphics
        Snow.Play();
    }

}
