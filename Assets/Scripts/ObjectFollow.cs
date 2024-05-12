using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float offset;

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + offset, target.position.z);

            targetPosition.z = transform.position.z;

            Vector3 delta = targetPosition - transform.position;
            
            transform.position = transform.position + delta * speed;
        }
    }

    public void ChangeTarget(Transform target)
    {
        this.target = target;
    }
}
