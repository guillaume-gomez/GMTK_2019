using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{

    public Text textToBlink = null;
    public float speedBlink = 0.3f;
    public float currentSpeedBlink = 0f;

    // Start is called before the first frame update
    void Start()
    {
        textToBlink = this.GetComponent<Text>();
        if (!textToBlink) return;
        currentSpeedBlink = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeedBlink += 1f * Time.deltaTime;

        if (currentSpeedBlink> speedBlink) {
            currentSpeedBlink = 0f;
            if(textToBlink.enabled == true) { textToBlink.enabled = false; } else { textToBlink.enabled = true; }
        }
    }
}
