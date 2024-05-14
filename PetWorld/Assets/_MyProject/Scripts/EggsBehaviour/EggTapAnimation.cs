using UnityEngine;

public class EggTapAnimation : MonoBehaviour
{
    [SerializeField] private float fullScale, tapScale;
    public int Multiplier = 1;

    private void Update()
    {
        if (gameObject.transform.localScale.x >= fullScale && Multiplier == 1)
        {
            gameObject.transform.localScale = new Vector3(fullScale, fullScale, fullScale);
        }
        else if (gameObject.transform.localScale.x <= tapScale && Multiplier == -1)
        {
            Multiplier = 1;
        }
        else
        {
            gameObject.transform.localScale += new Vector3(Multiplier, Multiplier, Multiplier) * Time.deltaTime * 2;
        }
    }
}
