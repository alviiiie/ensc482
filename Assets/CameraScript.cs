using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    int currentCamIndex = 0;

    WebCamTexture tex;

    public RawImage display;
    public Text startStopText;

    public void swapCam_Clicked()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            currentCamIndex += 1;
            currentCamIndex %= WebCamTexture.devices.Length;
            if (tex != null)
            {
                stopCamera();
                startStopCam_Clicked();
            }
        }
    }
    public void startStopCam_Clicked()
    {
        if (tex != null)
        {
            stopCamera();
            startStopText.text = "Start Camera";
        }
        else
        {
            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            tex = new WebCamTexture(device.name);
            display.texture = tex;
            tex.Play();
            startStopText.text = "Stop Camera";
        }
    }

    private void stopCamera()
    {
        display.texture = null;
        tex.Stop();
        tex = null;
    }



    
}
