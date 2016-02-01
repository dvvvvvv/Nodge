using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CometManager : MonoBehaviour
{
    public List<CometMove> comets;
    private GameObject space;
    public GameObject cometPrefab;
    public Text cometCountText;
    public TimeManager timeManager;

    public int initialCometCount = 10;
    public float additionalCometInterval = 2.0f;

    private bool onPlay = false;

    public void Awake()
    {
        comets = new List<CometMove>();
        if(space==null)space = GameObject.FindGameObjectWithTag("Space");        
    }

    public void Update()
    {
        if (!onPlay) return;
        cometCountText.text = "운석:" + comets.Count;
        //if (additionalCometInterval > 0 && timeManager.elapsedTime / additionalCometInterval > (comets.Count - initialCometCount)) AddComet(true);
    }
    
    public void GetRandomStart(ref Vector2 start, ref Quaternion dir)
    {
        if(space == null) space = GameObject.FindGameObjectWithTag("Space");

        Vector2 pos = Random.insideUnitCircle;

        float t;
        float xLength = (space.transform.localScale.x / 2) / Mathf.Abs(pos.x);
        float yLength = (space.transform.localScale.y / 2) / Mathf.Abs(pos.y);

        if (xLength < yLength) t = xLength;
        else t = yLength;

        start = pos * t;

        float randomDir = Random.Range(0, 90);

        if (pos.x > 0)
        {
            randomDir = 180 - randomDir;
            //Debug.Log("this Comet Must be go left");
        }
        if (pos.y > 0)
        {
            randomDir = 360 - randomDir;
            //Debug.Log("this Comet Must be go down");
        }

        Vector3 rotation = new Vector3(0, 0, randomDir);
        dir = Quaternion.Euler(rotation);
    }

    public CometMove AddComet()
    {
        CometMove newComet = GameObject.Instantiate(cometPrefab).GetComponent<CometMove>();
        newComet.transform.parent = transform;
        comets.Add(newComet);
        newComet.Respawn();
        newComet.onPlay = true;

        return newComet;
    }

    public CometMove AddComet(bool playWithBirth)
    {
        CometMove newComet = AddComet();
        newComet.onPlay = playWithBirth;

        return newComet;
    }

    public void Reset()
    {

        //Reset Comets
        int count = comets.Count;
        for (int i = count-1; i>=0;i--)
        {
            GameObject cometObject = comets[i].gameObject;
            if (cometObject.tag == "Comet")
            {
                comets.RemoveAt(i);
                GameObject.Destroy(cometObject);
            }
            else
            {
                comets[i].Respawn();
                comets[i].onPlay = true;
            }
        }

        for(int i = 0; i<initialCometCount;i++)
        {
            AddComet(true);
        }

        onPlay = true;
    }

    public void StopComets()
    {
        onPlay = false;
        foreach(CometMove comet in comets)
        {
            comet.onPlay = false;
        }
    }

}
