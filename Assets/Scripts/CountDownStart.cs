using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CountDownStart : MonoBehaviour
{
    public bool IsCounting { get; set; }
    public float countDown = 3;
    public Text countDownText;

    public UnityEvent OnCountDownEnd;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCounting)
        {
            countDown -= Time.deltaTime;
            countDownText.text = Mathf.RoundToInt(countDown).ToString();

            if (countDown <= 0)
            {
                OnCountDownEnd.Invoke();
            }
        }
    }

    public void CountDownEnd()
    {
        countDownText.text = "";
    }

    public void CountDownReset()
    {
        countDown = 3;
        countDownText.text = Mathf.RoundToInt(countDown).ToString();
    }
}
