using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyEngine.UI;

public class ButtonTween : MonoBehaviour
{
    [SerializeField] UIElement show;
    [SerializeField] UIElement close;
    public void Show()
    {
        show.show();
    }
    public void Close()
    {
        close.close();
    }
}
