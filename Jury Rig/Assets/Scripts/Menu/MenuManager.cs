using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("Scene Members")]

    [SerializeField] private string gamePlayScene;

    [Space]

    [Header("Fade Ui")]
    [SerializeField] private CanvasGroup _fadeImg;

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(1);
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

        yield return new WaitForSeconds(0.5f);

        while(_fadeImg.alpha != valueToGo)
        {
            _fadeImg.alpha -= (0.05f * index);

            yield return new WaitForSeconds(1f * Time.deltaTime);
        }

        //If its fade out means that will start the game 
        //So check if is fade out to call the LoadGame

        if(!isfadeIn)
        {
            LoadGameScene();
        }
    }

}
