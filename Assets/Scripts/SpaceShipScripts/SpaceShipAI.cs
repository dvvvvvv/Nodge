using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceShipAI : MonoBehaviour {

    public bool onPlay = false;

    public float moveSpeed = 2.0f;

    private Vector3 destination;
    public GameManager gameManager;
    public CometManager cometManager;
    public Transform Space;

    public float attractiveFactor = 1.0f;
    public float repulsiveFactor = 1.0f;
    public float repulsiveThreshold = 2.0f;
    public float linearRepulsiveFactor = 3.0f;
    public float linearRepulsiveWidth = 3.0f;
    public float linearRepulsiveHeight = 20.0f;
    
    void Awake()
    {
        destination = Vector3.zero;
    }

	// Update is called once per frame
	void Update () {
        if (!onPlay) return;
        Vector3 moveDir = Vector3.zero;

        moveDir = GetAttractivPotential() + GetRepulsivePotential() + GetLinearRepulsivePotential();
        moveDir.Normalize();
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        Debug.Log("moveDir:" + moveDir);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Space.lossyScale.x / 2, Space.lossyScale.x / 2),Mathf.Clamp(transform.position.x, -Space.lossyScale.y / 2, Space.lossyScale.y / 2),0);
        

    }

    private Vector3 GetAttractivPotential()
    {
        Vector3 force;
        Vector3 dir;
                
        force = (destination - transform.position);
        
        dir = force.normalized;

        float maginitude = force.magnitude;
        maginitude *= attractiveFactor / 2;

        force = dir * maginitude;

        if (force.magnitude < 0.01) force = Vector3.zero;

        return force;
    }

    private Vector3 GetRepulsivePotential()
    {
        Vector3 repulsive = Vector3.zero;

        foreach (CometMove comet in cometManager.comets)
        {
            Vector3 cometPosition;

            cometPosition = comet.gameObject.transform.position;

            Vector3 force = transform.position - cometPosition;
            Vector3 dir = force.normalized;

            if (force.sqrMagnitude > repulsiveThreshold)
            {
                force = Vector3.zero;
            }
            else
            {

                float repulseMagnitude;

                float length = (gameObject.transform.position - comet.gameObject.transform.position).sqrMagnitude;

                repulseMagnitude = Mathf.Pow(1 / length - 1 / repulsiveThreshold, 2);

                repulseMagnitude *= repulsiveFactor / 2;

                force = dir * repulseMagnitude;
            }

            if (force.magnitude < 0.1) force = Vector3.zero;

            repulsive += force;
        }

        if (repulsive.magnitude < 0.01) repulsive = Vector3.zero;
        Debug.Log("Repulse:"+repulsive);
        return repulsive;
    }

    private Vector3 GetLinearRepulsivePotential()
    {
        Vector3 linearRepulse = Vector3.zero;

        //Debug.Log(cometManager.comets.Count);

        foreach(CometMove comet in cometManager.comets)
        {
            Vector3 cometPosition = comet.gameObject.transform.position;
            Vector3 comet2ship = gameObject.transform.position - cometPosition;

            Vector3 cometDir = new Vector2(Mathf.Cos(comet.transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(comet.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            
            
            float comp = Vector3.Dot(comet2ship, cometDir);
            
            if (comp < 0 || comp > linearRepulsiveHeight)
            {
                continue;
            }

            Vector3 repulsePoint = cometPosition + (cometDir * comp);

            Vector3 repulseDir = transform.position - repulsePoint;

            if (repulseDir.sqrMagnitude < 0.1 && repulseDir.x < 0) repulseDir *= -1;

            float repulsePower = linearRepulsiveFactor * (linearRepulsiveHeight - comp)/linearRepulsiveHeight * (linearRepulsiveWidth - repulseDir.sqrMagnitude)/linearRepulsiveWidth;            

            if (repulseDir.sqrMagnitude < linearRepulsiveWidth / 2)
            {
                linearRepulse += repulseDir.normalized * repulsePower;
            }
        }
        if (linearRepulse.magnitude < 0.01) linearRepulse = Vector3.zero;
        Debug.Log("linearRepulse:" + linearRepulse);
        return linearRepulse;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        string othersTag = other.tag;

        if (othersTag.Equals("Comet") || othersTag.Equals("Player"))
        {
            onPlay = false;
            gameManager.Gameover();
        }
    }

    public void Respawn()
    {
        transform.position = Vector3.zero;
        onPlay = true;
    }
}
