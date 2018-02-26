using UnityEngine;
using System.Collections;
using System;

//Jokaisessa barissa oleva "stat", jokaiseen liitetty tietty bar 
//vain atribuutit maxVal ja currentVal
[Serializable]
public class Stat
{
    [SerializeField]
    private BarScript bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
            //Clamp varmisaa ettei mennä minimin ali/ maksimin yli kun esim kuollaan/healataan
            if (bar.name != "ExperienceBar")
            {               
                this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            }
            else
            {
                this.currentVal = value;
            }
            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            bar.MaxValue = value;
            //Tässä bar.Value =currentVal vain päivitystarkoituksessa!!! helpompi tehdä tässä kuin BarScriptissä
            bar.Value = currentVal;
           
            
        }
    }

    //Metodi joka alustaa barin, PAKKO KUTSUA AINA ENNEN BARIN KÄYTTÖÖNOTTOA!!!
    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
