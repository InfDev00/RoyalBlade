using Entity.Enemy;
using UnityEngine;

namespace Entity.Player
{
    public class Weapon : MonoBehaviour
    {
        public int damage;
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack() => _animator.SetTrigger("Attack");

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy")) other.transform.parent.GetComponent<EnemyGroup>().Damaged(damage);
            if (other.CompareTag("Boss"))
            {
                other.GetComponent<Enemy.Enemy>().IsDiedByDamaged(damage);
            }
        }
    }
}