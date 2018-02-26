using UnityEngine;
using System.Collections;
public class LocationTooltip : MonoBehaviour {


    //Tämä luokka näyttää tooltipin kun mouseover johonkin lokaatioon

    LocationManager LocationManager;

    void Start()
    {
        LocationManager = GameObject.Find("LocationManager").GetComponent<LocationManager>();
    }


    public void OnClick()
    {
        LocationManager.changeLocation(transform.GetComponentInParent<Location>().id);
        

    }


    public void showTooltip()
    {
        transform.gameObject.SetActive(true);
    }

    public void hideTooltip()
    {
        transform.gameObject.SetActive(false);
    }
}
