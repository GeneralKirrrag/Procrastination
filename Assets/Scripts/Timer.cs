using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public List<Slider> Sliders = new List<Slider>();
    public float decreaseRate = 1f;

    public void Update()
    {
        for (int i = 0; i < Sliders.Count; i++)
        {
            if (Sliders[i].value != 0)
            {
                Sliders[i].value = Mathf.MoveTowards(Sliders[i].value, 0f, decreaseRate * Time.deltaTime);
                break;
            }
        }
    }

    public void DepleteTimeSegment()
    {
        for (int i = 0; i < Sliders.Count; i++)
        {
            if (Sliders[i].value != 0)
            {
                Sliders[i].value = 0f;
                break;
            }
        }
    }
}
