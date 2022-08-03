using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator transitionFade;
    public float transitionTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFade());
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (Input.GetMouseButtonDown(0))
        {

            transitionFade.SetTrigger("End");
        }

    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(3);
        transitionFade.SetTrigger("Start");
    }
}
