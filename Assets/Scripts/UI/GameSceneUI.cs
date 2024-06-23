using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class GameSceneUI : MonoBehaviour
    {
        [Header("Buttons")]
        public Button jumpButton;
        public Button defenceButton;
        public Button attackButton;
        public Button pauseButton;
        public Button backButton;
        public Button gameOverButton;
        public Button gameEndButton;

        [Header("Images")]
        public Image[] coolTimeImages = new Image[3];
        public Image[] hpImage;
        
        [Header("Slider")] 
        public Slider stageSlider;
        public Slider hpSlider;

        [Header("Text")] 
        public TextMeshProUGUI scoreText;

        [Header("Panel")] 
        public GameObject pausePanel;
        public GameObject gameOverPanel;
        public GameObject gameEndPanel;
        
        public Action JumpAction = null;
        public Action<bool> DefenceAction = null;
        public Action AttackAction = null;
        
        private void Awake()
        {
            if (stageSlider) stageSlider.value = 0;
            if(hpSlider) hpSlider.gameObject.SetActive(false);
            
            foreach (var img in coolTimeImages) img.gameObject.SetActive(false);
        }

        private void Start()
        {
            jumpButton.onClick.AddListener(Jump);
            attackButton.onClick.AddListener(Attack);
            defenceButton.gameObject.AddEventTriggerListener(EventTriggerType.PointerDown,
                _ => DefenceAction?.Invoke(true));
            defenceButton.gameObject.AddEventTriggerListener(EventTriggerType.PointerUp,
                _ => DefenceAction?.Invoke(false));
            pauseButton.onClick.AddListener((() => Pause(true)));
            backButton.onClick.AddListener((() => Pause(false)));
            gameOverButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainScene");
            });
            gameEndButton.onClick.AddListener((() =>
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainScene");
            }));
            
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            gameEndPanel.SetActive(false);
        }

        private void FixedUpdate() => scoreText.text = $"{Database.Instance.Score}";
        
        private void Jump() => JumpAction?.Invoke();
        private void Attack() => AttackAction?.Invoke();

        private void Pause(bool p)
        {
            Time.timeScale = p ? 0 : 1;
            pausePanel.SetActive(p);
        }
        
        public void UpdateHpSlider(int hp, int maxHp)
        {
            if (!hpSlider) return;
            hpSlider.value = (float)hp / maxHp;
            
            hpSlider.gameObject.SetActive(hp > 0);
        }
        public void ButtonCoolTimeImage(int idx, float coolTime) => StartCoroutine(ButtonCoolTime(idx, coolTime));
        public void UpdatePlayerHp(int life)
        {
            foreach (var img in hpImage) img.gameObject.SetActive(false);
            for (int i = 0; i < life; ++i) hpImage[i].gameObject.SetActive(true);
        }

        private IEnumerator ButtonCoolTime(int idx, float coolTime)
        {
            coolTimeImages[idx].gameObject.SetActive(true);
            var initCoolTime = coolTime;
            
            while (coolTime > 0f)
            {
                coolTimeImages[idx].fillAmount = coolTime / initCoolTime;
                
                coolTime -= Time.deltaTime;
                yield return null;
            }
            
            coolTimeImages[idx].gameObject.SetActive(false);
        }
    }
}