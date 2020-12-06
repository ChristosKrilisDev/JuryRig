using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperLabel : MonoBehaviour
{
    public Text myLabel;

    [Header("Fade Ui")]
    [SerializeField] private CanvasGroup _fadeImg;


    private void Awake()
    {
        _fadeImg = gameObject.GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        FadeIn();

    }

    void FadeIn()
    {
        StartCoroutine(StartFadeEffect(true , 0));
        Debug.Log("Fade in");
    }

    void FadeOut()
    {
        StartCoroutine(StartFadeEffect(false , 1));
    }

    IEnumerator StartFadeEffect(bool isfadeIn , float alphaToGo)
    {
        float valueToGo = alphaToGo;
        float index = 1;

        if(isfadeIn)
        {
            index = +1;
            _fadeImg.alpha = 1;
        }
        else
        {
            index = -1;
            _fadeImg.alpha = 0;
        }


        yield return new WaitForSeconds(1.5f);

        while(_fadeImg.alpha != valueToGo)
        {
            _fadeImg.alpha -= (0.05f * index);

            yield return new WaitForSeconds(1f * Time.deltaTime);
        }


        yield return new WaitForSeconds(0.5f);
        //FadeOut();
        if(isfadeIn)
            gameObject.SetActive(false);
        //If its fade out means that will start the game 
        //So check if is fade out to call the LoadGame

    }
}
