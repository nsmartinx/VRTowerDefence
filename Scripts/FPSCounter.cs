using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI frameCounter;
    float i = 0;
    // Update is called once per frame
    void Update()
    {
        i += Time.deltaTime;

        if (i >= 1)
        {
            frameCounter.text = Mathf.Round((1 / Time.deltaTime)).ToString() + " FPS";
            i = 0;
        }
    }
}
