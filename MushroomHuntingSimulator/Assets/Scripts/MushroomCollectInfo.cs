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
        healthPointDeltaInfo.text = "xxx: " + info;
    }

    public void setEffectInfo(string info)
    {
        durationTimer.StartCountdown(durationSeconds);
        effectInfo.text = info;
    }

    public void resetMushroomCollectInfo() {
        healthPointDeltaInfo.text = "";
        effectInfo.text = "";
    }

}
