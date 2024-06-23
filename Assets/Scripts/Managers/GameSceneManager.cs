using System;
using Entity;
using Entity.Enemy;
using Entity.Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Managers
{
    public class GameSceneManager : MonoBehaviour
    {
        [Header("Units")]
        public AbstractPlayer player;
        public GameSceneUI gameSceneUI;
        public EnemySpawner spawner;
        public Enemy boss;
        
        public bool isStageUpdate;
        private const int MAX_STAGE = 10;
        
        [Header("prefab")]
        public GameObject enemy;
        public EnemySo[] enemySoArray;

        [Header("CoolTimes")] 
        public float[] coolTimes = new float[3];
        private readonly float[] _timeUpdate = new float[3];

        private Action<int, float> _onButtonClick;
        
        private void Awake()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            
            gameSceneUI.JumpAction += () => ButtonClick(0);
            gameSceneUI.AttackAction += () => ButtonClick(2);
            gameSceneUI.DefenceAction += (def) => ButtonClick(1, def);
            
            _onButtonClick += gameSceneUI.ButtonCoolTimeImage;
            
            player.OnPlayerDamaged += OnPlayerDamaged;
            player.SetHp(Database.Instance.Hp);
            gameSceneUI.UpdatePlayerHp(Database.Instance.Hp);
            
            for (int i = 0; i < 3; ++i) _timeUpdate[i] = 100f;
        }
                
        private void Start()
        {
            if(boss) boss.Init(20);
            UpdateStage();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < 3; ++i) _timeUpdate[i] += Time.fixedDeltaTime;
            

            if (boss)
            {
                gameSceneUI.UpdateHpSlider(boss.Hp, boss.maxHp);
                if(boss.Hp<=0) SceneChange("GameScene");
            }
            else if (spawner && spawner.currentEnemyGroup)
            {
                var currentEnemy = spawner.currentEnemyGroup.CurrentEnemy;
                gameSceneUI.UpdateHpSlider(currentEnemy.Hp, currentEnemy.maxHp);
            }
            else if (spawner) gameSceneUI.UpdateHpSlider(0, 10);
        }

        private void ButtonClick(int idx, bool def = false)
        {
            if (coolTimes[idx] > _timeUpdate[idx]) return;

            switch (idx)
            {
                case 0:
                    if (!player._isGround) return;
                    player.Jump();
                    break;
                case 1:
                    player.Defence(def);
                    break;
                case 2:
                    player.Attack();
                    break;
            }

            if (!def)
            {
                _timeUpdate[idx] = 0f;
                _onButtonClick?.Invoke(idx, coolTimes[idx]);
            }
        }

        private void OnPlayerDamaged()
        {
            if (player.Hp <= 0)
            {
                Database.Instance.Hp = 0;
                gameSceneUI.UpdatePlayerHp(0);
                GameOver();
                return;
            }
            
            Database.Instance.Hp = player.Hp;
            gameSceneUI.UpdatePlayerHp(Database.Instance.Hp);
        }

        private void GameOver()
        {
            Time.timeScale = 0f;
            Database.Instance.Reset();
            gameSceneUI.gameOverPanel.SetActive(true);
        }

        private void GameEnd()
        {
            Time.timeScale = 0f;
            Database.Instance.Reset();
            gameSceneUI.gameEndPanel.SetActive(true);
        }
        
        private void UpdateStage()
        {
            if (enemySoArray == null) return;
            var idx = Database.Instance.Stage < enemySoArray.Length ? Database.Instance.Stage : 0;

            if (spawner)
            {
                spawner.spawnDelay *= Mathf.Pow(0.99f, Database.Instance.Stage);
                spawner.spawnGravity *= Mathf.Pow(1.01f, Database.Instance.Stage);
                spawner.InstantiateEnemy(enemy, enemySoArray[idx], UpdateStage);
            }
            if (gameSceneUI.stageSlider) gameSceneUI.stageSlider.value = (float)Database.Instance.Stage / MAX_STAGE;
            
            if (isStageUpdate)
            {
                Database.Instance.Stage++;
                if (Database.Instance.Stage > MAX_STAGE)
                {
                    GameEnd();
                }
                else if (Database.Instance.Stage % 5 == 0 && !SceneManager.GetActiveScene().name.Equals("SubScene"))
                    SceneChange("SubScene");
            }
        }
        private void SceneChange(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}