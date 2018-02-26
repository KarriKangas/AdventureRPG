using UnityEngine;
using System.Collections;

public class AnimationHandler : MonoBehaviour {

    Animator animator;
	
	void Start () {
        animator = transform.GetComponent<Animator>();
        gameObject.SetActive(true);
        
    }
	

    public void playAnimation(string animation)
    {
        animator.Play(animation);              
    }

}
