using UnityEngine;
using System.Collections;

public class CometMove : MonoBehaviour {

    public float moveSpeed = 10.0f;
    public float turnSpeed = 10.0f;

    public bool IsInSpace = false;
    public bool onPlay = false;

    protected CometManager cometManager;

    ParticleSystem particle;

    virtual protected void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        cometManager = GetComponentInParent<CometManager>();
    }
	
	// Update is called once per frame
	virtual protected void Update () {
        if(onPlay) Move();
	}

    void Move()
    {
        transform.Translate(Time.deltaTime *  moveSpeed * Vector3.right,Space.Self);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        string othersTag = other.tag;

        if(othersTag.Equals("Space"))
        {
            //when it goes Inside of Space
            IsInSpace = true;
            //Debug.Log("SpaceIn");
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        string othersTag = other.tag;

        if (othersTag.Equals("Space"))
        {
            IsInSpace = false;
            //Debug.Log("SpaceOut");
        }
    }

    public void Respawn()
    {
        particle.Pause();
        //transform.position = cometManager.GetRandomStart();
        //Vector3 rotation = new Vector3(0, 0, cometManager.GetRandomDirection(transform.position));
        //transform.rotation = Quaternion.Euler(rotation);

        if (cometManager == null) cometManager = GetComponentInParent<CometManager>();

        Vector2 position = new Vector2();
        Quaternion rotation = new Quaternion();
        cometManager.GetRandomStart(ref position, ref rotation);

        transform.position = position;
        transform.rotation = rotation;


        IsInSpace = true;
        particle.Play();
    }
}
