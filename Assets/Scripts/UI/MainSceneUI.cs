using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainSceneUI : MonoBehaviour
    {
        [Header("Buttons")] 
        public Button gameStartButton;

        private void Awake()
        {
            gameStartButton.onClick.AddListener((() => SceneManager.LoadScene("GameScene")));
        }
    }
}