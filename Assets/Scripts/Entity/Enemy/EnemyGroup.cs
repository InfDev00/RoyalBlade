using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Enemy
{
    public class EnemyGroup : MonoBehaviour
    {
        private readonly Queue<Enemy> _enemyStack = new Queue<Enemy>();
        public Enemy CurrentEnemy { get;private set; }

        public Action OnDestroyed = null;
        
        private void Awake()
        {
            foreach (var obj in transform.GetComponentsInChildren<Enemy>()) _enemyStack.Enqueue(obj);
            CurrentEnemy = _enemyStack.Dequeue();
        }

        private void Update()
        {
            if (!CurrentEnemy) CurrentEnemy = _enemyStack.Dequeue();
        }
        

        public void Damaged(int damage)
        {
            
            if (CurrentEnemy.IsDiedByDamaged(damage))
            {
                Database.Instance.Score += 30;
                Destroy(CurrentEnemy.gameObject);
                if(_enemyStack.Count == 0) Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}