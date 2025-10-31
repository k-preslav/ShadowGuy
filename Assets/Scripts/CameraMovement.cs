using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform followTransform;

    private void LateUpdate()
    {
        if (followTransform != null)
        {
            Vector3 newPosition = followTransform.position;
            newPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10f);
        }
    }
}
