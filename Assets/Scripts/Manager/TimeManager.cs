using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class TimeManager : MonoBehaviour {

    public bool onPlay = false;
    private Text timeText;

    public float elapsedTime=0.0f;

	void Awake()
    {
        timeText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        if(onPlay)
        {
            elapsedTime += Time.deltaTime;
            timeText.text = elapsedTime.ToString("#0.00")+"초";
        }            	
	}

    public void Reset()
    {
        onPlay = true;
        elapsedTime = 0;
    }

    public void Stop()
    {
        onPlay = false;
    }

}
