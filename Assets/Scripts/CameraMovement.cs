using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform followTransform;

    public bool isFollowing = true;

    bool isWitchEndCutscene = false;

    float fov = 5f;

    private void LateUpdate()
    {
        if (followTransform != null && isFollowing)
        {
            Vector3 newPosition = followTransform.position;
            newPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 10f);
            
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, fov, Time.deltaTime * 1f);
        }

        if (isWitchEndCutscene)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, transform.position.z), Time.deltaTime * 0.5f);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, fov, Time.deltaTime * 1f);
        }
    }

    public void StartCutsceneWitchEnd()
    {
        isFollowing = false;
        isWitchEndCutscene = true;
        fov = 3.5f;

        StartCoroutine(ResetToFollow(5f));
    }

    IEnumerator ResetToFollow(float time)
    {
        yield return new WaitForSeconds(time);
        StartFollow();
    }

    public void StartFollow()
    {
        isFollowing = true;
        isWitchEndCutscene = false;
        fov = 5f;
    }
}
