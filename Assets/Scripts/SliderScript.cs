using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    Slider slider;
    public TextUpdate text;
    public MoveWithScroll scroll;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChange);
    }
    
      
      public void OnValueChange(float value)
    {
        text.UpdateSliderValue(value);
        scroll.UpdateScalar(value);
    }
}
