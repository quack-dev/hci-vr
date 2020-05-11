using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace uWintab
{

    public class Pen : MonoBehaviour 
    {
        [SerializeField]
        Vector2 localScale = new Vector2(1.92f, 1.08f);

        [SerializeField]
        float hoverHeight = 0.1f;

        public float pressureThreshold;
        private bool clicked;
        Button CurrentButton = null;

        Tablet tablet_;
        float y_ = 0f;

        void Start()
        {
            tablet_ = FindObjectOfType<Tablet>();
        }

        void Update()
        {
            UpdatePosition();
            UpdateRotation();
            CheckClick();
        }

        void UpdatePosition()
        {
            var x = (tablet_.x - 0.5f) * localScale.x;
            var z = (tablet_.y - 0.5f) * localScale.y;
            y_ = Mathf.Lerp(y_, (tablet_.proximity ? 0f : hoverHeight), 0.2f);
            transform.localPosition = new Vector3(x, y_, z);
        }

        void UpdateRotation()
        {
            var yaw = tablet_.azimuth * 360f;
            var pitch = (0.5f - tablet_.altitude) * 180f;
            var rot = Quaternion.Euler(pitch, yaw, 0f);
            transform.localRotation = Quaternion.FromToRotation(Vector3.up, rot * Vector3.up);
        }

        void CheckClick()
        {
            var pressure = tablet_.pressure;
            if(!clicked && pressure > pressureThreshold)
            {
                clicked = true;
                doClick();
            }
            else if (clicked && pressure <= pressureThreshold)
            {
                clicked = false;
            }
        }

        void doClick()
        {
            RaycastHit hit;
            Vector3 position = transform.position + (transform.TransformDirection(Vector3.up)*.08f);
            Vector3 direction = transform.TransformDirection(-Vector3.up);
            Debug.DrawRay(position, direction, Color.green, 2, false);
            if (Physics.Raycast(position, direction, out hit, 100.0F)) //tk might need to adjust the  parameters on this raycast
            {
                Debug.Log("Hit something");
                Button btn = hit.collider.gameObject.GetComponent<Button>();
                if(btn != null)
                {
                    CurrentButton = btn;
                    btn.onClick.Invoke();
                }
                else
                {
                    Debug.Log("Hit something non-buttony");
                }
            }
            else
            {
                Debug.Log("Hit not found");
            }
        }
    }

}