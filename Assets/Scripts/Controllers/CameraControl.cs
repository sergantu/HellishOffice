using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
    static private CameraControl _instance;
    public static CameraControl Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] Transform PlayerTran;
    [SerializeField] float speed; //скорость перемещения камеры
    [SerializeField] float swipeSpeed = 0.6f; //скорость свайпа
    [SerializeField] float swipeSensetive = 0.05f;  //расстояние, после которого начаниается свайп
    [SerializeField] float scrollSensetive = 5.0f;  //чувствительность приближения камеры
    [SerializeField] float startScroll = -40.0f;    //начальное положение камеры
    [SerializeField] Dictionary<string, float> swipeBorders = new Dictionary<string, float> //границы свайпа
    {
        ["left"]    = -12.0f,
        ["right"]   = 11.0f,
        ["up"]      = 14.0f,
        ["down"]    = -0.3f
    };
    private Vector2 startPos; //начальное положение камеры
    private Camera cam; //главная камера
    private Vector3 targetPos;  //целевая точка движения камреы
    private Vector3 startCamPos; //начальное положение камеры

    public Dictionary<string, float> scrollBorders = new Dictionary<string, float> //ограничение скролла
    {
        ["forward"] = -23.0f,
        ["back"] = -30.0f
    };


    private void Awake()
    {
        _instance = this;
    }

    void Start () 
    {
        cam = GetComponent<Camera>();
        targetPos = transform.position;
        startCamPos = transform.position;

        AlignCameraWithPlayerStart();
    }
	
	void Update () 
    {
        if ( HUD.Instance.isLocked() || GameController.Instance.LockClick == LockClick.True)    //игра на паузе или запрещен клик
        {
            return;
        }

        float new_pos_z = startScroll;

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) //если Пк
        {
            if (Input.GetMouseButtonDown(0)) //нажали
            {
                DownInputs();
            }
            else if (Input.GetMouseButton(0)) //отпустили
            {
                DownStayInputs();
            }

            new_pos_z = transform.position.z + Input.GetAxis("Mouse ScrollWheel") * scrollSensetive; //обновление скролла

        }
        else if (Application.platform == RuntimePlatform.Android) //если android
        {
            if ( Input.touchCount == 1 ) //1 касание
            {
                Touch touch = Input.GetTouch(0);

                switch( touch.phase )
                {
                    case TouchPhase.Began:
                        DownInputs();
                        break;

                    case TouchPhase.Moved:
                        DownStayInputs();
                        break;

                    case TouchPhase.Ended:
                        break;
                }
            }

            new_pos_z = transform.position.z;
            if (Input.touchCount == 2) //2 касания для скролла
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // Otherwise change the field of view based on the change in distance between the touches.
                new_pos_z = new_pos_z + deltaMagnitudeDiff * scrollSensetive * -0.005f;
            }

        }

        new_pos_z = Mathf.Clamp(new_pos_z, scrollBorders["back"], scrollBorders["forward"]); //проверка на границы скрола

        if ((targetPos - transform.position).magnitude > 0.001f) //если перемещена камера
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.x, speed * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPos.y, speed * Time.deltaTime), new_pos_z);
        }
        else
        {
            transform.position = new Vector3( transform.position.x, transform.position.y, new_pos_z );
        }
    }

    /// <summary>
    /// Начало клика или тапа
    /// </summary>
    void DownInputs()
    {
        startPos = cam.ScreenToViewportPoint(Input.mousePosition);
        startCamPos = transform.position;
    }

    /// <summary>
    /// Удержание клика или тапа
    /// </summary>
    void DownStayInputs()
    {
        float pos_x = cam.ScreenToViewportPoint(Input.mousePosition).x - startPos.x;
        float pos_y = cam.ScreenToViewportPoint(Input.mousePosition).y - startPos.y;

        if ( Mathf.Abs(pos_x) > swipeSensetive || Mathf.Abs(pos_y) > swipeSensetive)
        {
            pos_x = -pos_x * startCamPos.z * swipeSpeed;
            pos_y = -pos_y * startCamPos.z * swipeSpeed;
            pos_x = Mathf.Clamp(startCamPos.x - pos_x, swipeBorders["left"], swipeBorders["right"]);
            pos_y = Mathf.Clamp(startCamPos.y - pos_y, swipeBorders["down"], swipeBorders["up"]);

            targetPos = new Vector3(pos_x, pos_y, startCamPos.z);
        } 
    }

    public bool AlignCameraWithPlayer()
    {
        if ( Mathf.Abs(PlayerTran.position.x - transform.position.x) > 2 || Mathf.Abs(PlayerTran.position.y - (transform.position.y - 1.0f)) > 2  )
        {
            targetPos = new Vector3(PlayerTran.position.x, PlayerTran.position.y + 1.0f, transform.position.z);
            transform.position = targetPos;
            return true;
        }
        else
        {
            return false;
        }
    }

    void AlignCameraWithPlayerStart()
    {
        targetPos = new Vector3(PlayerTran.position.x, PlayerTran.position.y + 1.0f, transform.position.z);
        transform.position = targetPos;
    }

}
