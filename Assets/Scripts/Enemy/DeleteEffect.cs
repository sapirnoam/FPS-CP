using UnityEngine;

public class DeleteEffect : MonoBehaviour
{
    public float TimeToRemote = 15;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TimeToRemote);
    }
}
