using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace uWintab
{
    public class Pen : MonoBehaviour
    {
        public PenSpaceMover penSpaceMover;
        [SerializeField]
        Vector2 localScale = new Vector2(1.92f, 1.08f);

        [SerializeField]
        float hoverHeight = 0.1f;

        public float pressureThreshold;
        private bool clicked;
        Button CurrentButton = null;

        Tablet tablet_;
        float y_ = 0f;

        public bool erasing = false;

        void Start()
        {
            tablet_ = FindObjectOfType<Tablet>();
        }

        void Update()
        {
            UpdateRotation();
            UpdatePosition();
            CheckClick();
            CheckExpkeyPress();
        }

        void UpdatePosition()
        {
            var x = (tablet_.x - 0.5f) * localScale.x;
            var z = (tablet_.y - 0.5f) * localScale.y;
            y_ = Mathf.Lerp(y_, (tablet_.proximity ? 0f : hoverHeight), 0.2f);
            transform.localPosition = new Vector3(x, y_, z);
            DragSpace.transform.localPosition = new Vector3(.03f, 0f, -.15f) + new Vector3(x, y_, z);
        }

        void UpdateRotation()
        {
            bool xrot = transform.localEulerAngles.x > 90 && transform.localEulerAngles.x < 270;
            bool zrot = transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270;
            var yaw = tablet_.azimuth * 360f;
            var pitch = (0.5f - tablet_.altitude) * 180f;
            if (xrot || zrot)
            {
                erasing = true;
                //yaw = -yaw;
                pitch = -pitch;
                transform.Find("Wacom Pen").localPosition = new Vector3(0f, -.73f, 0f);
            }
            else
            {
                erasing = false;
                transform.Find("Wacom Pen").localPosition = new Vector3(0f, .43f, 0f);
            }

            var rot = Quaternion.Euler(pitch, yaw, 0f);
            transform.localRotation = Quaternion.FromToRotation(Vector3.up, rot * Vector3.up);
        }

        void CheckClick()
        {
            var pressure = tablet_.pressure;
            if (!clicked && pressure > pressureThreshold)
            {
                clicked = true;
                doClick();
            }
            else if (clicked && pressure <= pressureThreshold)
            {
                clicked = false;
            }
        }

        bool editModePressed = false;
        bool tabletModePressed = false;
        void CheckExpkeyPress()
        {
            //do NOT touch //tablet_.expKeyNum
            bool[] pressed = new bool[8];
            for (int i = 0; i < tablet_.expKeyNum; ++i)
            {
                pressed[i] = tablet_.GetExpKey(i);
            }
            if (pressed[4] || pressed[5])
            {
                editModePressed = true;
                GoToRitualFrame();
            }
            else if (!(pressed[4] || pressed[5]) && editModePressed)
            {
                editModePressed = false;
            }


            if (pressed[6] || pressed[7])
            {
                tabletModePressed = true;
                GoToTabletFrame();
            }
            else if (!(pressed[6] || pressed[7]) && tabletModePressed)
            {
                tabletModePressed = false;
            }
        }

        private static string GetGameObjectPath(Transform transform)
        {
            string path = transform.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                path = transform.name + "/" + path;
            }
            return path;
        }

        GameObject draggingStatement = null;
        public GameObject DragSpace;
        public void Drag(GameObject s)
        {
            draggingStatement = s;
            s.transform.parent = DragSpace.transform;
            s.transform.localPosition = Vector3.zero;
            s.transform.localScale = Vector3.one;
            s.transform.localRotation = Quaternion.identity;
        }

        T findParentWith<T>(GameObject go)
        {
            T btn = go.GetComponent<T>();
            while (btn == null && !(go.transform.parent is null))
            {
                go = go.transform.parent.gameObject;
                try
                {
                    btn = go.GetComponent<T>();
                }
                catch
                {

                }
            }
            return btn;
        }

        void doClick()
        {
            RaycastHit hit;
            Vector3 position = transform.position + (transform.TransformDirection(Vector3.up) * (erasing ? -.08f : .08f));
            Vector3 direction = transform.TransformDirection(erasing ? Vector3.up : -Vector3.up);
            Debug.DrawRay(position, direction, Color.green, 2, false);

            if (!EditMode)
            {
                if (Physics.Raycast(position, direction, out hit, 100.0F)) //tk might need to adjust the  parameters on this raycast
                {
                    Debug.Log((erasing ? "Erasing " : "Clicking") + GetGameObjectPath(hit.collider.gameObject.transform));
                    Button btn = findParentWith<Button>(hit.collider.gameObject);
                    
                    if (btn != null)
                    {
                        CurrentButton = btn;
                        btn.onClick.Invoke();
                    }
                    else
                    {
                        Debug.Log("No Button available");
                    }
                }
                else
                {
                    Debug.Log("No hit in frame mode");
                }
            }
            else //EditMode
            {
                if (Physics.Raycast(position, direction, out hit, 100.0F))
                {
                    Debug.Log((erasing ? "Erasing " : "Clicking") + GetGameObjectPath(hit.collider.gameObject.transform));
                    if (draggingStatement is null)
                    {
                        NumberArgument na = findParentWith<NumberArgument>(hit.collider.gameObject);
                        if(!(na is null))
                        {
                            na.Select();
                        }
                        ASTNodeMono mononode = findParentWith<ASTNodeMono>(hit.collider.gameObject);
                        if (!(mononode is null))
                        {
                            Method m = mononode.ContainingMethod();
                            Statement s = mononode.ContainingStatement();
                            if (!(s is null))
                            {
                                Debug.Log("StatementEdit");
                                if (!erasing)
                                {
                                    Debug.Log("MethodEdit");
                                }
                                else
                                {
                                    s.RemoveSelf();
                                    Destroy(s.gameObject);
                                }
                            }
                            else if (!(m is null))
                            {
                                Debug.Log("MethodEdit");
                                //no method edits available
                                //m.AcceptStatement(s);
                            }
                        }
                    }
                    else
                    {
                        ASTNodeMono mononode = hit.collider.gameObject.GetComponent<ASTNodeMono>();
                        GameObject go = hit.collider.gameObject;
                        while (mononode is null && !(go.transform.parent is null))
                        {
                            go = go.transform.parent.gameObject;
                            try
                            {
                                mononode = go.GetComponent<ASTNodeMono>();
                            }
                            catch
                            {

                            }
                        }
                        if (!(mononode is null))
                        {
                            Statement s = draggingStatement.GetComponent<ASTNodeMono>().ContainingStatement();
                            Method m = mononode.ContainingMethod();
                            Statement s2 = mononode.ContainingStatement();
                            if (!(s2 is null))
                            {
                                s.transform.parent = s2.transform.parent;
                                if (!erasing)
                                    s2.AddStatementAfter(s);
                                else
                                {
                                    s2.ReplaceWith(s);
                                    Destroy(s2.gameObject);
                                }
                            }
                            else if (!(m is null))
                            {
                                m.AcceptStatement(s);
                            }
                            else
                            {
                                Debug.Log("??????????");
                            }
                            draggingStatement = null;
                            GoToTabletFrame();
                        }
                    }
                }
                else if (!(draggingStatement is null))
                {
                    Debug.Log("Destroying dragged statement");
                    Destroy(draggingStatement);
                    GoToTabletFrame();
                }
                else
                {
                    Debug.Log("No hit in frame mode");
                }
            }
        }

        public bool EditMode = false;

        public void GoToRitualFrame()
        {
            penSpaceMover.NewOrientation(new Vector3(-.071f, .825f, 1.185f),
                                         5 * Vector3.one,
                                         new Vector3(-90f, 0f, 0f),
                                         .5f * Vector3.one,
                                         .75f);
            EditMode = true;
        }

        public void GoToTabletFrame()
        {
            penSpaceMover.NewLocalOrientation(Vector3.zero,
                                         Vector3.one,
                                         new Vector3(0f, 0f, 0f),
                                         Vector3.one,
                                         .75f);
            EditMode = false;
        }
    }

}