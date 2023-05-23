using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{

    [SerializeField] float totalTime = 300f;
    float remainingTime;

    public TMP_Text countdown;
    bool gameCountdown;
    
    // Start is called before the first frame update
    void Start()
    {
        countdown = GetComponent<TextMeshProUGUI>();
        StartCoroutine(startCountdown(3));
        remainingTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int)(remainingTime/60);
        int seconds = (int)(remainingTime%60);
        int miliseconds = (int)((remainingTime*10)%10);
        if (gameCountdown)
        {
            remainingTime -= Time.deltaTime;
            countdown.text = string.Format("{0:0}:{1:00}:{2:0}",minutes,seconds,miliseconds);
        }
        if(remainingTime <= 0)
        {
            countdown.text = "TIME'S UP!";
            gameCountdown = false;
        }
        
    }

    IEnumerator startCountdown (int seconds)
    {
        while (seconds >= 0)
        {
            if(seconds>0)
            {
                countdown.text = (seconds).ToString();
            }
            else if (seconds == 0)
            {
                countdown.text = "GO!";
            }
            seconds--;
            yield return new WaitForSeconds(1f);
        }
        countdown.text ="";
        gameCountdown = true;
    }
}
