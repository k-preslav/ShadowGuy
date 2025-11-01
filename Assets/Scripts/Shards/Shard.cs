using UnityEngine;

public class Shard : MonoBehaviour
{
    [SerializeField] GameObject liveWorldMask;

    GameObject _instantiated;

    private void Update()
    {
        if (_instantiated)
        {
            _instantiated.transform.localScale = Vector2.Lerp(_instantiated.transform.localScale, new Vector2(1, 1), Time.deltaTime * 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _instantiated = Instantiate(liveWorldMask, transform.position, Quaternion.identity);
            _instantiated.transform.localScale = new Vector2(0, 0);
        }
    }
}
