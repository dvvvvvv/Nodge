using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(Image))]
public class TargetPointer : MonoBehaviour {

    private Image pointer;
    private RectTransform rectTransform;
    public Transform target;
    public Transform player;

    void Awake()
    {
        pointer = gameObject.GetComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        //방향획득
        Vector2 dir = (target.position - player.position).normalized;

        //위치계산
        Rect canvasSize = pointer.canvas.pixelRect;
        float t;
        float xLength = ((canvasSize.width-30) / 2) / Mathf.Abs(dir.x);
        float yLength = ((canvasSize.height-30) / 2) / Mathf.Abs(dir.y);        

        if (xLength < yLength) t = xLength;
        else t = yLength;        

        //적용
        rectTransform.localPosition = dir * t;

        //각도획득
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //회전
        rectTransform.rotation = Quaternion.Euler(0,0,angle);
    }
}
