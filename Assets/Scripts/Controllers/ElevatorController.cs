using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ElevatorController : MonoBehaviour
{
    static private ElevatorController _instance;
    public static ElevatorController Instance
    {
        get
        {
            return _instance;
        }
    }
    float[] coors = { 1.2f, 3.2f, 7.2f, 10.2f, 13.2f }; //координаты точек расположения полов этажей

    public Button elevator1;
    public Button elevator2;
    public Button elevator4;
    public Button elevator5;

    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Перемещение игрока на этаж
    /// </summary>
    /// <param name="newFloor">Новый этаж</param>
    public void MoveToFloor( int newFloor )
    {
        GameController.Instance.AudioManager.PlaySFX("aud_clk_button_menu_1");
        Player.Instance.gameObject.transform.position = new Vector3(Player.Instance.gameObject.transform.position.x, coors[newFloor], Player.Instance.gameObject.transform.position.z);
        Player.Instance.Current_station = newFloor + 1;

        //должны быть в конце метода, важно!
        HUD.Instance.HideElevatorWindow();
        HUD.Instance.SetLock(false);
        CameraControl.Instance.AlignCameraWithPlayer();
        ////////////////////////////////////

        int rnd = Random.Range(0, 5);
        if (rnd == 0)
        {
            GameController.Instance.ShowElevatorEvent();
        }

        int realFloor = newFloor + 1;

        if(realFloor != 3 && !GameController.Instance.IsEventDone("opn_floor" + realFloor))
        {
            GameController.Instance.SetEventDone("opn_floor" + realFloor);
        }
    }
}
