using UnityEngine;

namespace Entity.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public int maxHp;
        public int Hp { get; private set; }
        
        private void Awake()
        {
            Hp = maxHp;
        }

        public void Init(int mul = 1)
        {
            maxHp = ((int)Mathf.Log(2, Database.Instance.Stage+1) + 1) * mul;
            Hp = maxHp;
        }
        
        public void Init(EnemySo so)
        {
            Init();

            GetComponent<SpriteRenderer>().color = so.color;
            if (so.image) GetComponent<SpriteRenderer>().sprite = so.image;
        }
        
        public bool IsDiedByDamaged(int damage)
        {
            Hp -= damage;
            return Hp <= 0;
        }
    }
}