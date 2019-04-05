using UnityEngine;

public class AISpawn : MonoBehaviour
{
    private GameObject objSpawn;
    private int SpawnerID;
    // Used to find the parent spawner object
    void Start()
    {
        objSpawn = (GameObject)GameObject.FindWithTag("Spawner");
    }
    // Call this when you want to kill the enemy
    void removeMe()
    {
        objSpawn.BroadcastMessage("killEnemy", SpawnerID);
        Destroy(gameObject);
    }
    // this gets called in the beginning when it is created by the spawner script
    void setName(int sName)
    {
        SpawnerID = sName;
    }
}