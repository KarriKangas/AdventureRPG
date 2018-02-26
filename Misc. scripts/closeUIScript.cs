using UnityEngine;
using System.Collections;

public class closeUIScript : MonoBehaviour {
    [SerializeField]
    private GameObject MainObject;


    // Use this for initialization
    
	
	// Update is called once per frame
	public void OnClick()
    {
        if(MainObject.activeSelf == true)
        {
            MainObject.SetActive(false);
        }
        if(MainObject.name == "QuestRewardPanel")
        {
            Destroy(MainObject.transform.GetChild(0).GetChild(0).gameObject);
        }
    }
}
