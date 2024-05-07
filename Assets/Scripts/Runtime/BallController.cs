using HVNT.Test.Runtime;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    private Vector3 targetPos;
    private int indx;

    void Awake()
    {
        indx = 0;
        targetPos = ProjectSystemLocator.Service.GetPoint(indx);
    }

    void Update()
    {
        var dir = (targetPos - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        if((transform.position - targetPos).magnitude < 0.1f)
        {
            indx = (indx + 1) % ProjectSystemLocator.Service.PointAmount();
            targetPos = ProjectSystemLocator.Service.GetPoint(indx);
        }
    }
}