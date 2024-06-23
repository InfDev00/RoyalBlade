using System;
using UnityEngine;

namespace Utils
{
    public class Camera : MonoBehaviour
    {
        public bool isCameraFollow;
        
        [Header("Follow Camera")]
        public float cameraSpeed = 5.0f;
        public Vector3 cameraPosition;

        public Transform followee;

        private void Awake()
        {
            var cam = GetComponent<UnityEngine.Camera>();
            var viewportRect = cam.rect;
            const float targetAspectRatio = 9f / 16f;

            var screenAspectRatio = (float)Screen.width / Screen.height / targetAspectRatio;
            
            if (screenAspectRatio < 1)
            {
                viewportRect.height = screenAspectRatio;
                viewportRect.y = (1f - viewportRect.height) / 2f;
            }
            else
            {
                viewportRect.width = 1 / screenAspectRatio;
                viewportRect.x = (1f - viewportRect.width) / 2f;
            }
            
            cam.rect = viewportRect;
        }

        private void Update()
        {
            if (isCameraFollow) Move();
        }

        private void Move()
        {
            transform.position = followee.position + cameraPosition;
            
            var clampY = Mathf.Clamp(transform.position.y, 0, Mathf.Infinity);

            transform.position = new Vector3(transform.position.x, clampY, -10f);
        }
    }
}