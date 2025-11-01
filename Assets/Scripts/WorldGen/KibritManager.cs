using UnityEngine;

public class KibritManager : MonoBehaviour
{
    [SerializeField] GameObject kibritSprite;

    int kibrits = 0;

    [SerializeField] Transform worldStart;
    [SerializeField] Transform worldEnd;

    public int totalKibritsMax = 999;
    int totalKibrits = 0;

    private void Update()
    {
        if (kibrits <= 0)
        {
            if (totalKibrits >= totalKibritsMax) return;

            for (int i = 0; i < 3; i++)
            {
                kibrits++;
                totalKibrits++;

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
