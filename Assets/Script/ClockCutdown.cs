using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using EazyEngine.UI;
   

public class ClockCutdown : MonoBehaviour
{
    [SerializeField] private Text timer;
    [SerializeField] private Image imageLight;
    [SerializeField] UIElement pnShow;
    public int totaltime;
    private int time;
    public bool isPause = false;
       // Start is called before the first frame update

    private void Start()
    {
        Being(totaltime);
    }

    private void Being(int Second)
    {
        time = Second;
       StartCoroutine(UpdateTimer());
    }
    // Update is called once per frame
    private IEnumerator UpdateTimer()
    {
            while (time >0)
        {          
                float minutes = Mathf.FloorToInt(time / 60);
                float sec = Mathf.FloorToInt(time % 60);
                timer.text = string.Format("{0:00}:{1:00}", minutes, sec);
                imageLight.fillAmount = Mathf.InverseLerp(0, totaltime, time);
                time--;
                yield return new WaitForSeconds(1f);        
        }
       /* yield return new WaitForSeconds(1f);*/
    }
    private void Update()
    {
        if(time == 0)
        {
            timer.text = time.ToString("0");
            pnShow.show();
        }
    }

    private void OnEnable()
    {

        StartCoroutine(UpdateTimer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
