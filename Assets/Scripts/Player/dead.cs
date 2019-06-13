using UnityEngine;

public class dead : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;
    public Transform transform;
    private void Awake()
    {
        transform.position = player.position + offset;
        transform.LookAt(player);
    }
}
