using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int totalPoints;

    [SerializeField] private Text _timerText;
    float tmp_Counter;
    bool _canCount;

    [SerializeField] private Text _scoreText;
    float tmp_points = 0;

    [SerializeField]private GameObject _scoreNotificationPrefab;

    public static ScoreManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            return;
        }
        //else
        Instance = this;
    }

    void Start()
    {
        _canCount = true;
        tmp_Counter = 0;
        _timerText.text = null;
        _scoreText.text = 0.ToString();

    }

    void Update()
    {
        if(_canCount)
            StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        _canCount = false;
        while(true)
        {
            _timerText.text = tmp_Counter.ToString("f1");
            tmp_Counter += 1f * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void AddPoints(int points = 10)
    {
        Debug.Log(points);
        tmp_points += points;
        _scoreText.text = tmp_points.ToString("f0");
        //add visual to the player
    }

    public void CountScore()
    {
        StopCoroutine(Timer());
        totalPoints = (int)tmp_points + (int)tmp_Counter; 
    }

    public int TotalScore => totalPoints;
}
