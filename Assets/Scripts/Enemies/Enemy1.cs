using UnityEngine;
using UnityEngine.Events;

public class Enemy1 : MonoBehaviour
{
  [SerializeField] public int Level;

  public UnityEvent enemyKilledEvent;

  public EnemySpawner enemySpawner;

  public CombatManager combatManager;

  private void Start()
  {
    
  }

  public void SetLevel(int level)
  {
    this.Level = level;
  }

  public int GetLevel()
  {
    return Level;
  }

  private void OnDestroy()
  {
    if (enemySpawner != null && combatManager != null)
        {
            enemySpawner.onDeath();
            combatManager.onDeath();

        }
  }
}
