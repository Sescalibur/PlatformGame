using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] Text timeValue;
    [SerializeField] float time;

    private bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
        Timer();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    public void Timer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            timeValue.text = ((int)time).ToString();
        }
        else if (time < 0 && gameActive == true)
        {
            GetComponent<PlayerController>().Die();
            gameActive = false;
        }
    }
}
