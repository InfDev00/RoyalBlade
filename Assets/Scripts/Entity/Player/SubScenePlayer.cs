using Entity.Enemy;
using UnityEngine;

namespace Entity.Player
{
    public class SubScenePlayer : AbstractPlayer
    {
        public override void Jump()
        {
            _rigidBody2D.AddForce(Vector2.up * 100f * jumpPower);
            _isGround = false;
        }

        public override void Attack()
        {
            Defence(false);
            weapon.Attack();
        }

        public override void Defence(bool def)
        {
            _isDefence = def;
            shield.Defence(def);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<EnemyGroup>(out var _))
            {
                if (_isGround) Damage(1);
                else _rigidBody2D.velocity = Vector2.zero;
            }
            
            else if (other.gameObject.CompareTag("Floor")) _isGround = true;
        }
    }
}