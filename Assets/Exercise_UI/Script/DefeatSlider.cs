using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatSlider : MonoBehaviour
{
    public Image percentBar;
    float maxPercent = 100f;
    public int current;
    public static float percent;
    /*public static float percentSlider { get; private set }*/
    [SerializeField] Text percentChange;

    private void Start()
    {
        percentBar = GetComponent<Image>();
    }

    private void Update()
    {
        float fillAmount = current / maxPercent;
        percentBar.fillAmount = fillAmount;
        percentChange.text = ((int)(fillAmount * 100)).ToString() + "%";
    }
}
