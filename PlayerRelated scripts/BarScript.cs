using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Jokaisella barilla (palkilla) oleva script
public class BarScript : MonoBehaviour {

    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    public float MaxValue { get; set; }
    //Valuen set jokaisessa barissa
    public float Value
    {
        set
        {
            //Jaetaan string osiksi tyyliin "Health:" + value +"/" + MaxValue
            string[] tmp = valueText.text.Split(':');

            value = (int)value;

            //Ja asetetaan ensimmäinen osa ([0]) vastaamaan barin nimeä
            valueText.text = tmp[0] + ": " + value +"/" + MaxValue;
            //Ja fillAmount oikeaksi
            fillAmount = Map(value,0 ,MaxValue);
            
        }
    }

	
	void Update ()
    {
        //Jokainen bar käsittelee itsensä oikeaksi määräksi kun on tarve
        HandleBar();
	}

    //HandleBar määrää barin sisällön ja täyttää/tyhjentää sitä lerpaten (eli liukuen) kun on tarve
    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
    }


    //Muuttaa palkin contentin(0-inf) fillAmountiksi (0-1)
    private float Map(float value, float Min, float Max)
    {
        //Value = current content
        //Max = max content
        //Min = min content (always(?) 0)
        return value / (Max - Min);
        


    }

}
