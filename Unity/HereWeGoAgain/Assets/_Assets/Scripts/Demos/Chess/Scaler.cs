using System;
using UnityEngine;

namespace _Assets.Scripts.Demos.Chess
{
    public class Scaler : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float maxScale = 1.4f;
        [SerializeField] private float minScale = .2f;

        private void Update()
        {
            if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
            {
                float scaleFactor = Mathf.Pow(2f, Input.GetAxis("Mouse X") * speed);

                float newX = Mathf.Clamp(transform.localScale.x * scaleFactor, minScale, maxScale);
                float newZ = Mathf.Clamp(transform.localScale.z * scaleFactor, minScale, maxScale);

                transform.localScale = new Vector3(
                    newX,
                    transform.localScale.y,
                    newZ);
            }
        }
    }
}