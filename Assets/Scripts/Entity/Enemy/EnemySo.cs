using UnityEngine;

namespace Entity.Enemy
{
    [CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Object/Enemy", order = 0)]
    public class EnemySo : ScriptableObject
    {
        public Sprite image;
        public Color color;
    }
}