using System;
using Entity.Enemy;
using UnityEngine;

namespace Entity.Player
{
    public class Shield : MonoBehaviour
    {
        public int damage;
        public float knockBackPower;
        public bool stopAtCollision;
        public bool disableAtCollision;
        
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }


        public void Defence(bool def)
        {
            _boxCollider2D.enabled = def;
            var alpha = def ? 1 : 0;
            _spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var parent = other.transform.parent;
            if (parent!=null && parent.TryGetComponent<EnemyGroup>(out var enemyGroup))
            {
                enemyGroup.Damaged(damage);
                
                var rigid = parent.GetComponent<Rigidbody2D>();
                if (stopAtCollision) rigid.velocity = Vector2.zero;
                rigid.AddForce(Vector2.up * 100f * knockBackPower);
                
                
            }
            if (disableAtCollision) Defence(false);
        }
    }
}