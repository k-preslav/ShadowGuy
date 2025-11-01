using UnityEngine;
using UnityEngine.SceneManagement;  
public class SmoothFollow : MonoBehaviour
{
    public Transform target;         
    public float followSpeed = 10f;   
    public float smoothness = 0.5f;  
    public Vector3 offset;          
      public float catchDistance = 0.5f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;
float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= catchDistance)
        {
            CatchPlayer();
            return;
        }
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothness,
            followSpeed
        );
    }
    void CatchPlayer()
    {
        Debug.Log("Player Caught!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, catchDistance);
    }

}