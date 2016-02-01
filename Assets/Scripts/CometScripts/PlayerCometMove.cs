using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCometMove : CometMove {

    public Image pointer;

    protected override void Awake()
    {
        base.Awake();

        cometManager.comets.Add(this);

    }

    protected override void Update()
    {
        if (!IsInSpace)
            SetDirByInput();
        base.Update();        
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if(other.gameObject.tag == "Space")
        {
            pointer.enabled = true;
        }
        if (other.gameObject.tag == "OuterSpace")
        {
            transform.Rotate(0, 0, 180);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.tag == "Space")
        {
            pointer.enabled = false;
        }
    }

    void SetDirByInput()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 cursorPos = Input.mousePosition;
            
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(cursorPos);

            targetPos.z = transform.position.z;

            Vector3 dir = targetPos - transform.position;

            float rotateDegree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0f, 0f, rotateDegree),turnSpeed * Time.deltaTime);
        }
    }
}
