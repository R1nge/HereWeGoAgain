using UnityEngine;

namespace _Assets.Scripts.Demos.Chess
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
            {
                var rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speed, transform.up);
                transform.rotation = rotation * transform.rotation;
            }
        }
    }
}