using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Luokka joka luo xp tarvitsemiskäyrän
public class ExperienceScript : MonoBehaviour {

    private List<float> experienceRequired;
    private float experienceFactor = 2f;
    private float baseExperienceReq = 50;
    private int maxLevel = 100;

    //Luodaan experiencentarvintakäyrä tyyliin base*(currentLevel^experienceFactor)
    public List<float> initExperienceCurve()
    {
        experienceRequired = new List<float>();
        float currentLevel;
        for (int i = 0; i < maxLevel; i++)
        {
            currentLevel = i;
            experienceRequired.Add(baseExperienceReq * (Mathf.Pow(currentLevel, experienceFactor)));
            //Debug.Log("Experience required to reach Level " + i + " is " + experienceRequired[i]);
        }

        return experienceRequired;
    }
}
