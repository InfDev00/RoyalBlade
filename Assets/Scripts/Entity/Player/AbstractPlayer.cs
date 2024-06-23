using System;
using UnityEngine;

namespace Entity.Player
{
    public abstract class AbstractPlayer : MonoBehaviour
    {
        [Header("Status")] 
        public float jumpPower = 1f;
        public int maxHp = 3;
        public int Hp { protected set; get; }

        [Header("Equipment")]
        public Weapon weapon;
        public Shield shield; //임시
        
        public bool _isGround;
        protected bool _isDefence;
        protected Rigidbody2D _rigidBody2D;
        
        public Action OnPlayerDamaged = null;
        
        private void Awake()
        {
            _isGround = true;
            _isDefence = false;
            _rigidBody2D = GetComponent<Rigidbody2D>();
            Hp = maxHp;

        }

        public void SetHp(int hp) => this.Hp = hp;
        
        private void Start()
        {
            shield.Defence(false);
        }

        private void Update()
        {
            _rigidBody2D.gravityScale = _rigidBody2D.velocity.y < 0 ? 2f : 1f;
        }

        public abstract void Jump();

        public abstract void Attack();

        public abstract void Defence(bool def);

        protected void Damage(int damage)
        {
            Debug.Log($"{Hp}-{damage} = {Hp-damage}");
            Hp -= damage;
            OnPlayerDamaged?.Invoke();
        }
    }
}