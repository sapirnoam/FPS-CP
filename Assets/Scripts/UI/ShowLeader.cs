using GameJolt.UI;
using UnityEngine;

public class ShowLeader : MonoBehaviour
{
    // Start is called before the first frame update
    public void Showleader()
    {
        GameJoltUI.Instance.ShowLeaderboards();
    }
}
