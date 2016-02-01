using UnityEngine;
using System.Collections;

public class CameraRig : MonoBehaviour {

    public float smoothTime = 0.1f;
    public Vector3 currentVelocity;
    public Transform target;
    public PlayerCometMove player;

    public bool onPlay = false;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.gameObject.GetComponent<PlayerCometMove>();
    }

	// Update is called once per frame
	void LateUpdate () {

        if (!player.IsInSpace && onPlay)
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, smoothTime);
        else
            transform.position = Vector3.SmoothDamp(transform.position, Vector2.zero, ref currentVelocity, smoothTime);



    }
}
