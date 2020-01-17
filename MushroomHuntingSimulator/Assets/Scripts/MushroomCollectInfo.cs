using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MushroomCollectInfo : MonoBehaviour
{

    public GameObject healthPointDeltaInfoGameObject;
    public GameObject effectInfoGameObject;

    private TextMeshProUGUI healthPointDeltaInfo;
    private TextMeshProUGUI effectInfo;
    private CountdownTimer durationTimer = new CountdownTimer();

    private float durationSeconds = 3.0f;

    void Start()
    {
        healthPointDeltaInfo = healthPointDeltaInfoGameObject.GetComponent<TextMeshProUGUI>();
        effectInfo = effectInfoGameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        durationTimer.DecrementTime(Time.deltaTime);
        if (durationTimer.HasEnded())
        {
            resetMushroomCollectInfo();
        }
    }

    public void setHealthPointDeltaInfo(int info)
    {
        durationTimer.StartCountdown(durationSeconds);

        string finalText = "";
        if (info < 0)
        {
            healthPointDeltaInfo.color = new Color32(255, 0, 0, 255);
            finalText = info.ToString();
        }
        else
        {
            healthPointDeltaInfo.color = new Color32(0, 255, 0, 255);
            finalText = "+" + info.ToString();
        }
        healthPointDeltaInfo.text = finalText;
    }

    public void setEffectInfo(string info, bool isNegative)
    {
        if (isNegative)
        {
            effectInfo.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            effectInfo.color = new Color32(0, 255, 0, 255);
        }
        durationTimer.StartCountdown(durationSeconds);
        effectInfo.text = info;
    }

    public void resetMushroomCollectInfo() {
        healthPointDeltaInfo.text = "";
        effectInfo.text = "";
    }

}
