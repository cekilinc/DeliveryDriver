using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TMP_Text countdown;
    
    // Start is called before the first frame update
    void Start()
    {
        countdown = GetComponent<TextMeshProUGUI>();
        StartCoroutine(startCountdown(3));
    }

    // Update is called once per frame
    void Update()
    {
        
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
            countdown.text ="";
        }
    }
}
