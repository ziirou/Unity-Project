using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  public PlayerHealth playerHealth;  // Reference to the player's heatlh.
  public GameObject player;               // ref to player position.
  public GameObject enemy;                // The enemy prefab to be spawned.
  public float spawnTime = 7f;            // How long between each spawn.
  public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
  private int Spawns;
  private float runtime;
  void Start()
  {
    player = GameObject.Find("Player");
    // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
    InvokeRepeating("Spawn", spawnTime, spawnTime);
  }

  private void Update() {
    /*  
    runtime += Time.deltaTime;
    // move into manager 
    if(runtime % Time.deltaTime == 9){
      difficulty += 0.1f;
      startingHealth = startingHealth * (int) difficulty;
      enemyAttack.setAttackDamage(attackDamage * (int) difficulty);
    }
    */
  } 

  void Spawn()
  {
    Spawns += 1;
    if(Spawns % 5 == 4 && spawnTime > 1f){
      spawnTime -= 0.1f;
    }

    // If the player has no health left...
    if (playerHealth.currentHealth <= 0f)
    {
        // Disable spawning
        CancelInvoke("Spawn");
        // ... exit the function.
        return;
    }

    // Find a random index between zero and one less than the number of spawn points.
    int spawnPointIndex = Random.Range(0, spawnPoints.Length);

    // Spawn enemies if closer than 20m
    if (50 > Vector3.Distance(spawnPoints[spawnPointIndex].position, player.transform.position))
    {
      // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
      Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    } else {
      Invoke("Spawn", 0.1f);
    }
  }
}