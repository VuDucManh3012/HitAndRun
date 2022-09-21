using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomMechanic : MonoBehaviour
{
    [Header("Character")]
    public GameObject Character;
    public ControllerPlayer ControllerPlayer;
    private GameObject ModelCharacterRoot;

    [Header("Portal")]
    public GameObject Portal;
    public GameObject ModelInPortal;
    private Animator AnimatorPortal;

    [Header("Camera")]
    public GameObject CameraManger;

    [Header("CanvasManager")]
    public CanvasManager CanvasManager;

    [Header("SlowMotion")]
    public GameObject SlowMotion;

    [Header("Target")]
    private Vector3 Target;

    // Start is called before the first frame update
    void Start()
    {
        ControllerPlayer = Character.GetComponent<ControllerPlayer>();
        ModelCharacterRoot = ControllerPlayer.ModelCharacterRoot;
        AnimatorPortal = Portal.GetComponentInChildren<Animator>();
        OpenPortal();
        OnOFfTouchPad(false);
        Target = new Vector3(-7, 5, 0);
    }
    public void OnOFfTouchPad(bool On)
    {
        CanvasManager.CanvasTouchPad.SetActive(On);
    }
    public void TransformCharacter()
    {
        //DichViTri
        Character.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        ControllerPlayer.ObjectFollowCharacter.GetComponent<CameraFollow2>().PositionYOnJump = transform.position.y + 2;
        ControllerPlayer.ObjectFollowCharacter.transform.position = transform.position;
        //ChangeCam
        ControllerPlayer.OffCam();
        ChangeCamera("CamStartBossRoom");
        //AnimPortal
        ClosePortal();
    }
    void ChangeCamera(string cameraName)
    {
        for (int i = 0; i < CameraManger.transform.childCount; i++)
        {
            CameraManger.transform.GetChild(i).gameObject.SetActive(false);
        }
        CameraManger.transform.Find(cameraName).gameObject.SetActive(true);
    }
    void OpenPortal()
    {
        AnimatorPortal.SetTrigger("Open");
    }
    void ClosePortal()
    {
        Portal.transform.localPosition = new Vector3(0, 2.1f, 0);
        AnimatorPortal.SetTrigger("Close");
    }
    public void ActiveModelCharacterRoot(bool active)
    {
        ModelCharacterRoot.SetActive(active);
        ModelInPortal.SetActive(!active);
    }
    public void EffectOpenPortal()
    {
        ControllerPlayer.OnParticle(3);
    }
    public void ReadySlide()
    {
        SlowMotion.SetActive(true);
        ControllerPlayer.OffCam();
        ChangeCamera("CamSlide");
    }
    public void StartSlide()
    {
        //bat text
        ControllerPlayer.Floatingtext.SetActive(true);
        //change cam
        ChangeCamera("CamStartSlide");
        //slide
        ControllerPlayer.SetSpeed(20);
        //Effect
        ControllerPlayer.OnParticle(13);
    }
    public void ChangeTransformTarget()
    {
        Target = new Vector3(-Target.x, Random.Range(1, 10), 0);
    }
}
