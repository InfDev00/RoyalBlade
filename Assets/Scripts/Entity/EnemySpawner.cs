using System;
using System.Collections;
using Entity.Enemy;
using UnityEngine;

namespace Entity
{
    public class EnemySpawner : MonoBehaviour
    {
        public int enemiesPerSpawn = 5;
        public float spawnDelay = 1f;
        public float spawnGravity = 0.5f;

        public EnemyGroup currentEnemyGroup;
        
        public void InstantiateEnemy(GameObject prefab, EnemySo so, Action action = null) => StartCoroutine(Spawn(prefab, so, action));

        private IEnumerator Spawn(GameObject prefab, EnemySo so, Action action)
        {
            yield return new WaitForSeconds(spawnDelay);
            
            var enemyGroup = new GameObject("EnemyGroup")
            {
                transform =
                {
                    position = transform.position,
                    rotation = transform.rotation
                }
            };
            for (int i = 0; i < enemiesPerSpawn; ++i)
            {
                var enemy = Instantiate(prefab, enemyGroup.transform, true);
                enemy.GetComponent<Enemy.Enemy>().Init(so);
                enemy.transform.localPosition = new Vector3(0, i * 2, 0);
            }

            var rigid = enemyGroup.AddComponent<Rigidbody2D>();
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            rigid.gravityScale = spawnGravity;
            enemyGroup.AddComponent<EnemyGroup>().OnDestroyed += action;

            currentEnemyGroup = enemyGroup.GetComponent<EnemyGroup>();
        }
    }
}