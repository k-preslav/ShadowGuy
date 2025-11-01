using UnityEngine;

public class KibritManager : MonoBehaviour
{
    [SerializeField] GameObject kibritSprite;

    int kibrits = 0;

    [SerializeField] Transform worldStart;
    [SerializeField] Transform worldEnd;

    private void Update()
    {
        if (kibrits <= 0)
        {
            for (int i = 0; i < 4; i++)
            {
                kibrits++;

                Vector2 position = new Vector2(
                    Random.Range(worldStart.position.x, worldEnd.position.x),
                    Random.Range(worldStart.position.y, worldEnd.position.y)
                );

                GameObject kibrit = Instantiate(kibritSprite, position, Quaternion.identity);
                kibrit.GetComponent<DrasniPalniKlechica>().OnTrigger += () => { kibrits--; };
            }
        }
    }
}
