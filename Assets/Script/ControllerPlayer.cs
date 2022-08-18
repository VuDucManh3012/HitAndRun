using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CnControls;
using RocketTeam.Sdk.Services.Ads;
using Cinemachine;
using MoreMountains.NiceVibrations;
public class ControllerPlayer : MonoBehaviour
{
    Rigidbody myBody;
    Animator myAnim;

    private float deltaX2;
    private float deltaX2Before;

    public float SpeedLeftRight;

    private bool isMove = true;

    public float maxX;

    public bool isUnDead = false;

    public bool moveOnWall = false;
    private bool onWall = false;

    public double myLevel;

    public double QualityDiamond;
    private double DiamondFound;

    public double QualityStage;
    private bool PlusStage;

    public Save Save;

    private bool BossEndingBonus = false;
    private float SeBonus;
    public Text SeBonusText;
    public Text DiamondFoundText;
    private double diamondBonus;
    public GameObject BossBonus;

    private Vector3 myposition;

    private bool unLimitDamage;
    private bool startStageEnding;
    public GameObject attackObject;

    public GameObject GameOverScene;
    public GameObject VictoryScene;

    private bool onRoad;
    public GameObject GameStartScene;
    public bool startgame;

    public bool unsetLevelText;

    public GameObject WeaponLeft;
    public GameObject WeaponRight;

    [Header("CharacterParent")]
    public AutoMove AutoMoveCharacter;

    public GameObject ModelCharacterParent;
    public GameObject ModelCharacterBase;
    public GameObject ModelCharacterArmor;
    public GameObject WeaponSpecial;
    public Texture[] textSkin;
    public int NumberTextSkin;
    private Renderer SkinRenderer;
    private Renderer SkinArmorRenderer;

    private bool changeSkin;
    private bool victory;
    private int NumberTextSkinLevel;

    public bool OnChangeSkin;

    public GameObject CanvasChestRoom;
    public GameObject CanvasX2Hole;
    private int DiamondBonusInHole;
    private int attack;

    private int CharacterGlowUp;
    private Animator FloatingTextAnimator;
    private GameObject FloatingTextUp;
    private GameObject Floatingtext;

    private GameObject EnemyEndingModel;
    private GameObject EnemyEndingRagdoll;
    private GameObject FireWorkEnemyEnding;

    private SWS.splineMove splineMoveComponent;
    public bool PlusedSeBonus = false;
    [Header("Ragdoll")]
    public GameObject Ragdoll;
    public RagdollCharacter RagdollCharacter;

    [Header("Plus999Level")]
    public GameObject Plus999Level;

    [Header("Particle System")]
    public GameObject[] Particle;

    [Header("SpawnMap")]
    public EnemyCheckLevel EnemyCheckLevelObject;

    [Header("ListCamera")]
    public GameObject CamManager;

    [Header("CanvasManger")]
    public CanvasManager CanvasManager;

    [Header("CanvasNewSkinEnding")]
    public GameObject CanvasNewSkinEnding;

    [Header("Speed")]
    public int SpeedRoad;
    public int SpeedJump;
    public int SpeedPush;

    public Material WhiteBoard;

    [Header("FollowCharacter")]
    public GameObject ObjectFollowCharacter;
    public GameObject CamRun;
    public GameObject CamInHole;

    [Header("SpawnMap")]
    public GameObject SpawnMap;

    [Header("AnimKey")]
    public GameObject AnimKey;
    private void Awake()
    {
        myLevel = 1;
        startgame = false;
    }
    public void CheckLevelEnemy()
    {
        EnemyCheckLevelObject.checkLevelEnemy(myLevel);
    }
    // Start is called before the first frame update
    public void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        splineMoveComponent = GetComponentInParent<SWS.splineMove>();
        SkinRenderer = ModelCharacterBase.GetComponent<Renderer>();
        SkinArmorRenderer = ModelCharacterArmor.GetComponent<Renderer>();
        FloatingTextUp = transform.Find("Character").Find("FloatingTextUp").gameObject;
        Floatingtext = transform.Find("Character").Find("FloatingText").gameObject;
        DiamondFound = 0;
        SeBonus = 0;
        diamondBonus = 0;
        PlusStage = false;
        SetSpeed(0);
        startStageEnding = false;

        SetWeapon();
        SetLevelUpdate();

        NumberTextSkin = 0;
        changeSkin = true;
        victory = false;
        attack = 0;
        CharacterGlowUp = 0;
        SetSkin();
        SetFloatingTextAnimator();
        ChangeCam("CamStart");
        CheckLevelEnemy();

        myAnim.SetInteger("Ending", Random.Range(0, 5));
    }

    void FixedUpdate()
    {
        LeftRight();
        checkDead();
        checkY();
        setLevelText();
        //
        SetOnRoad();
        AutoRotateFloatingText();
    }
    public void AutoRotateFloatingText()
    {
        if (Floatingtext.transform.rotation != new Quaternion(0, 0, 0, 1))
        {
            Floatingtext.transform.rotation = new Quaternion(0, 0, 0, 1);
        }
    }
    public void Update()
    {
        if (startgame && !onWall && !OnJump)
        {
            AutoRotate();
        }
        else if (OnJump)
        {
            if (JumpHighLeft == -1)
            {
                transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, -30), 0.1f);
            }
            else if (JumpHighLeft == 1)
            {
                transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 30), 0.1f);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), 0.1f);
            }

        }
    }
    public int JumpHighLeft;
    [ContextMenu("Addforce")]
    public void addForce(float y)
    {
        myBody.AddForce(new Vector3(0, y, 0), ForceMode.Impulse);
    }
    public void AutoRotate()
    {
        deltaX2 = transform.position.x;
        if (deltaX2 != deltaX2Before)
        {
            float dis = deltaX2 - deltaX2Before;
            float Angle = Mathf.Clamp(Mathf.Atan(dis) * 180 / Mathf.PI * 10, -30, 30);
            Quaternion newRotation = Quaternion.Euler(0, Angle, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, 0.1f);
        }
        else
        {
            float newRotation = Quaternion.Angle(transform.localRotation, Quaternion.Euler(0, 0, 0)) * 0.1f;
            transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, newRotation, 0), 0.2f);
        }
        deltaX2Before = transform.position.x;
    }
    public void SetFloatingTextAnimator()
    {
        FloatingTextAnimator = Floatingtext.GetComponent<Animator>();
    }
    public void SetLevelUpdate()
    {
        myLevel += PlayerPrefs.GetInt("UpdateLevel");
    }
    [ContextMenu("SetSkin")]
    public void SetSkin2()
    {
        for (int i = 0; i <= 11; i++)
        {
            WeaponSpecial.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void SetSkin()
    {
        if (changeSkin && OnChangeSkin)
        {
            if (!PlayerPrefs.HasKey("CurrentSkin"))
            {
                PlayerPrefs.SetString("CurrentSkin", "ArmorIron4");
            }
            for (int i = 0; i <= textSkin.Length - 1; i++)
            {
                if (PlayerPrefs.GetString("CurrentSkin").Remove(PlayerPrefs.GetString("CurrentSkin").Length - 1) == textSkin[i].name.Remove(textSkin[i].name.Length - 1))
                {
                    SkinRenderer.material.mainTexture = textSkin[i];
                    SkinArmorRenderer.material.mainTexture = textSkin[i];
                    NumberTextSkin = i;
                    changeSkin = false;
                    return;
                }
            }
        }

        if (!changeSkin && OnChangeSkin)
        {
            if (myLevel >= double.Parse("250"))
            {
                if (CharacterGlowUp == 2)
                {
                    //chay anim
                    if (!myAnim.GetBool("JumpAttack360"))
                    {
                        myAnim.Play("GlowUp");
                        Jump(0, 4, 0);
                    }
                    OnParticle(3);
                    //
                    CharacterGlowUp = 3;
                }
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                NumberTextSkinLevel = NumberTextSkin + 3;
                SkinRenderer.material.mainTexture = textSkin[NumberTextSkinLevel];
                ModelCharacterArmor.SetActive(true);
                SkinArmorRenderer.material.mainTexture = textSkin[NumberTextSkinLevel];
                WeaponSpecial.transform.Find(PlayerPrefs.GetString("CurrentSkin").Remove(PlayerPrefs.GetString("CurrentSkin").Length - 1)).gameObject.SetActive(true);
            }
            else if (myLevel >= double.Parse("150"))
            {
                if (CharacterGlowUp == 1)
                {
                    //chay anim
                    if (!myAnim.GetBool("JumpAttack360"))
                    {
                        myAnim.Play("GlowUp");
                        Jump(0, 4, 0);
                    }
                    OnParticle(3);
                    //
                    CharacterGlowUp = 2;
                }
                NumberTextSkinLevel = NumberTextSkin + 2;
                SkinRenderer.material.mainTexture = textSkin[NumberTextSkinLevel];
            }
            else if (myLevel >= double.Parse("70"))
            {
                if (CharacterGlowUp == 0)
                {
                    //chay anim
                    if (!myAnim.GetBool("JumpAttack360"))
                    {
                        myAnim.Play("GlowUp");
                        Jump(0, 4, 0);
                    }
                    OnParticle(3);
                    //
                    CharacterGlowUp = 1;
                }
                NumberTextSkinLevel = NumberTextSkin + 1;
                SkinRenderer.material.mainTexture = textSkin[NumberTextSkinLevel];
            }
        }
    }
    private void SetWeapon()
    {
        if (PlayerPrefs.GetString("CurrentWeapon") == "")
        {
            PlayerPrefs.SetString("CurrentWeapon", WeaponLeft.transform.GetChild(0).name);
        }
        string weapon = WeaponLeft.transform.Find(PlayerPrefs.GetString("CurrentWeapon")).name;

        for (int i = 0; i < WeaponLeft.transform.childCount; i++)
        {
            if (WeaponLeft.transform.GetChild(i).name == weapon)
            {
                WeaponLeft.transform.GetChild(i).gameObject.SetActive(true);
                WeaponRight.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                WeaponLeft.transform.GetChild(i).gameObject.SetActive(false);
                WeaponRight.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    private void LeftRight()
    {
        if (startgame && isMove == true)
        {
            SetRunTrue();

            transform.position = new Vector3(Mathf.Clamp(transform.position.x + CnInputManager.GetAxis("Horizontal") * SpeedLeftRight * Time.deltaTime, -maxX, maxX), transform.position.y, transform.position.z);
        }
    }
    public void ChangeCam(string CamName)
    {
        for (int i = 0; i < CamManager.transform.childCount; i++)
        {
            CamManager.transform.GetChild(i).gameObject.SetActive(false);
        }
        CamManager.transform.Find(CamName).gameObject.SetActive(true);
    }
    public void OffCam()
    {
        for (int i = 0; i < CamManager.transform.childCount; i++)
        {
            CamManager.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void OnParticle(int a)
    {
        Particle[a].SetActive(false);
        Particle[a].SetActive(true);
    }
    public void OffParticle(int a)
    {
        Particle[a].SetActive(false);
    }
    //Animation
    void SetRunTrue()
    {
        myAnim.SetBool("StartMove", true);
    }
    void SetDieTrue()
    {
        myAnim.SetBool("Die", true);
        //tat textlevel
        unsetLevelText = true;
    }
    void SetVictoryTrue()
    {
        victory = true;
        myAnim.SetBool("Victory", true);
        ChangeCam("CamBossEnding");
    }
    void SetOnRoad()
    {
        if (onRoad)
        {
            myAnim.SetBool("JumpWall", false);
            myAnim.SetBool("JumpAttack360", false);
        }
    }
    void SetJumpWall()
    {
        if (myAnim.GetBool("JumpWall"))
        {
            myAnim.SetBool("JumpWall", false);
        }
        else
        {
            myAnim.SetBool("JumpWall", true);
        }
    }
    void SetJumpAttack360(bool TorF)
    {
        myAnim.SetBool("JumpAttack360", TorF);
    }
    void SetAttack()
    {
        if (attack == 0)
        {
            myAnim.SetBool("Attack", true);
            myAnim.SetBool("Attack2", false);
            attack = 1;
            OnParticle(1);
        }
        else if (attack == 1)
        {
            myAnim.SetBool("Attack", false);
            myAnim.SetBool("Attack2", true);
            attack = 2;
            OnParticle(2);
        }
    }
    void SetAttackFalse()
    {
        myAnim.SetBool("Attack", false);
        myAnim.SetBool("Attack2", false);
        if (attack == 2)
        {
            attack = 0;
        }
    }
    private void UpdateDiamond()
    {
        if (!PlusedSeBonus)
        {
            int TotalDiamond = (int)(DiamondFound * SeBonus);
            DiamondFoundText.text = "You Earned";
            if (TotalDiamond <= 50)
            {
                TotalDiamond = 50;
            }
            SeBonusText.text = TotalDiamond.ToString();
            diamondBonus = (DiamondFound * SeBonus) - (DiamondFound * SeBonus) % 1;
            if (BossEndingBonus)
            {
                QualityDiamond += diamondBonus;
                QualityDiamond += 300;
                BossBonus.SetActive(true);
            }
            else
            {
                QualityDiamond += diamondBonus;
            }
            Save.Diamond.text = QualityDiamond.ToString();
        }
        PlusedSeBonus = true;
    }
    private void setLevelText()
    {
        if (!unsetLevelText)
        {
            GetComponentInChildren<TextMeshPro>().text = "LV " + myLevel.ToString();
        }
    }
    public void SetSpeed(int speed)
    {
        AutoMoveCharacter.SetSpeed(speed);
    }
    private void RotateFloatingText(float x, float y, float z)
    {
        Quaternion newRotation = Quaternion.Euler(x, y, z);
        Floatingtext.transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, 0.25f);
    }
    private void RotateCharacter(float x, float y, float z)
    {
        Quaternion newRotation = Quaternion.Euler(x, y, z);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, 0.25f);
    }
    public void rotateTo180()
    {
        transform.localEulerAngles = new Vector3(0, 180, 0);
        //Floatingtext.transform.localEulerAngles = new Vector3(0, 180, 0);
    }
    private void Jump(float x, float y, float z)
    {
        myBody.velocity = new Vector3(x, y, z);
    }
    public bool OnJump;
    IEnumerator setOnJump()
    {
        OnJump = true;
        yield return new WaitForSeconds(1.5f);
        //OnJump = false;
    }
    private void JumpHigh()
    {
        if (!OnJump)
        {
            ObjectFollowCharacter.GetComponent<CameraFollow2>().onJump = true;
            SetSpeed(SpeedJump);
            myBody.velocity = new Vector3(0, 24f, 0);
            StartCoroutine(setOnJump());
            //if(transform.position.x > 2)
            //{
            //    transform.localEulerAngles = new Vector3(0, 0, 45);
            //}
            //else if(transform.position.x < 2)
            //{
            //    transform.localEulerAngles = new Vector3(0, 0, -45);
            //}  
        }
    }
    private void JumpLow()
    {
        if (!OnJump)
        {
            ObjectFollowCharacter.GetComponent<CameraFollow2>().onJump = true;
            myBody.velocity = new Vector3(0, 12f, 0);
            SetSpeed(SpeedJump + 1);
            StartCoroutine(setUnDead());
            StartCoroutine(setOnJump());
        }
    }
    IEnumerator setUnDead()
    {
        isUnDead = true;
        OnJump = true;
        yield return new WaitForSeconds(0.3f);
        OnJump = false;
        isUnDead = false;
    }
    IEnumerator setMoveOnWall()
    {
        yield return new WaitForSeconds(2);
        if (moveOnWall && !onWall)
        {
            moveOnWall = false;
        }
    }
    [ContextMenu("Attack")]
    public void AttackJump()
    {
        StartCoroutine(jumpAttack());
    }
    IEnumerator jumpAttack()
    {
        unLimitDamage = true;
        SetJumpAttack360(false);
        yield return new WaitForSeconds(0.2f);
        myAnim.SetInteger("AttackItem", 1);
        attackObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        attackObject.transform.GetChild(1).gameObject.SetActive(true);
        myAnim.SetInteger("AttackItem", 2);
    }
    public void SetAttackAnimTo0()
    {
        myAnim.SetInteger("AttackItem", 0);
        unLimitDamage = false;
    }
    IEnumerator WaitToVictory(float second)
    {
        yield return new WaitForSeconds(second);
        OnParticle(5);
        Destroy(EnemyEndingModel);
        EnemyEndingRagdoll.transform.GetChild(PlayerPrefs.GetInt("IntBossToSpawn")).gameObject.SetActive(true);
        EnemyEndingRagdoll.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        Time.timeScale = 1f;
        OffParticle(5);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        FireWorkEnemyEnding.SetActive(true);
        yield return new WaitForSeconds(5);
        UpdateDiamond();
        ActiveCanvasVictory();

        transform.rotation = new Quaternion(0, 1, 0, 0);
    }
    public void pushOpposite()
    {
        myBody.AddForce(new Vector3(0, 0, -10), ForceMode.Impulse);
        StartCoroutine(setUnDead());
    }
    public void pushOppsiteEnemy()
    {
        myBody.AddForce(new Vector3(0, 0, -23), ForceMode.VelocityChange);
        StartCoroutine(setUnDead());
    }
    private bool buttonHoleGet = false;
    IEnumerator WaitX2Hole()
    {
        isMove = false;
        yield return new WaitForSeconds(5f);
        if (!buttonHoleGet)
        {
            ButtonHole();
        }
    }
    private void pushRight()
    {
        myBody.AddForce(Vector3.right, ForceMode.Impulse);
        myBody.velocity = new Vector3(-3, 10, 0);
    }
    private void pushLeft()
    {
        myBody.velocity = new Vector3(3, 10, 0);
        myBody.AddForce(Vector3.left, ForceMode.Impulse);
    }
    [ContextMenu("Hurt")]
    private void hurt()
    {
        if (startStageEnding)
        {
            myLevel -= 10000;
            FloatingTextUp.SetActive(false);
        }
        else
        {
            FloatingTextUp.GetComponent<TextMeshPro>().text = "-10";
            FloatingTextUp.SetActive(false);
            FloatingTextUp.SetActive(true);
            FloatingTextAnimator.Play("FloatingText");

            myLevel -= 10;
        }
    }
    private void checkY()
    {
        if (transform.position.y <= -20 && !startStageEnding)
        {
            myLevel = 0;
            SetDieTrue();
            SetSpeed(0);

            QualityDiamond -= DiamondFound;
            GameOverScene.SetActive(true);
        }
    }
    IEnumerator VictoryInStageEnding()
    {
        yield return new WaitForSeconds(3f);
        ActiveCanvasVictory();
    }
    public bool ActiveCanvasVictoy;
    public void ActiveCanvasVictory()
    {
        if (!ActiveCanvasVictoy)
        {
            if (System.Int32.Parse(PlayerPrefs.GetString("key")) >= 3)
            {
                CanvasChestRoom.SetActive(true);
            }
            else
            {
                if (PlayerPrefs.GetInt("skinSpecial" + 4) >= 2 && PlayerPrefs.GetInt("skinSpecial" + 5) >= 2)
                {
                    VictoryScene.SetActive(true);
                }
                else
                {
                    CanvasManager.CanvasNewSkinEndingController();
                }

            }
            ActiveCanvasVictoy = true;
        }
    }
    private void checkDead()
    {
        if (myLevel <= 0)
        {
            SetSpeed(0);
            ChangeCam("CamDead");
            CamManager.transform.Find("CamDead").gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
            SetDieTrue();
            myLevel = 0;
            isMove = false;
            if (startStageEnding)
            {
                transform.Find("Character").gameObject.SetActive(false);
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.GetComponent<Animator>().enabled = false;
                transform.Find("Ragdoll").gameObject.SetActive(true);
                RagdollCharacter.RagdollChangeSkin(NumberTextSkinLevel, myLevel);
                UpdateDiamond();
                StartCoroutine(VictoryInStageEnding());
            }
            else
            {
                QualityDiamond -= DiamondFound;
                GameOverScene.SetActive(true);
                isMove = false;
            }
        }
    }

    [ContextMenu("ReBorn")]
    public void ReBorn(int value)
    {
        AnalyticManager.LogWatchAds("AdsReborn", 1);
        adsShowing = false;
        GameOverScene.SetActive(false);
        unsetLevelText = false;
        myAnim.SetBool("Die", false);
        myLevel = 1;
        ChangeCam("CamRun");
        isMove = true;

        //dich vi tri
        Transform Start, End;
        for (int i = 1; i <= SpawnMap.transform.childCount; i++)
        {
            Start = SpawnMap.transform.GetChild(i).Find("Start");
            End = SpawnMap.transform.GetChild(i).Find("End");
            if (Start.position.z <= transform.position.z && transform.position.z <= End.transform.position.z)
            {
                transform.position = new Vector3(0, 2.3f, Start.transform.position.z - 5);
                break;
            }
        }
    }
    public void StartGame()
    {
        startgame = true;
        GameStartScene.SetActive(false);
        Plus999Level.SetActive(true);
        ChangeCam("CamRun");
        SetSpeed(SpeedRoad);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bridge")
        {
            isMove = false;
            transform.position = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
            SetSpeed(SpeedRoad);
        }
        else if (other.tag == "Glass")
        {
            other.transform.parent.Find("Brick").gameObject.SetActive(true);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "EndingBoard")
        {
            other.transform.GetComponent<Renderer>().material = WhiteBoard;
        }
        else if (other.tag == "ButtonOpenGate")
        {
            other.transform.parent.Find("Gate").GetComponent<Gate>().enabled = true;
        }
        else if (other.tag == "Road")
        {
            if (startgame)
            {
                onRoad = true;
                SetSpeed(SpeedRoad);
            }
        }
        else if (other.tag == "StageEnding")
        {
            isMove = false;
            ChangeCam("CamEnding");
            SetSpeed(SpeedRoad - 2);
            if (!PlusStage)
            {
                QualityStage += 1;
                PlusStage = true;
            }
            startStageEnding = true;
            if (victory)
            {
                SetSpeed(0);
            }
        }
        else if (other.tag == "FireWork")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (other.tag == "JumpLow")
        {
            JumpLow();
            if (other.transform.position.x < -1.5)
            {
                pushRight();
            }
            else if (other.transform.position.x > 1.5)
            {
                pushLeft();
            }
            other.transform.GetComponent<Animator>().enabled = true;

        }
        else if (other.tag == "JumpHigh")
        {
            JumpHigh();
            SetJumpAttack360(false);
            other.transform.GetComponent<Animator>().Play("Piston", -1, 0f);
            if (transform.position.x <= -2)
            {
                JumpHighLeft = -1;
            }
            else if (transform.position.x >= 2)
            {
                JumpHighLeft = 1;
            }
            else
            {
                JumpHighLeft = 0;
            }
        }
        else if (other.tag == "ItemWall")
        {
            moveOnWall = true;
            StartCoroutine(setMoveOnWall());
        }
        else if (other.tag == "ItemWallLeft")
        {
            moveOnWall = true;
            StartCoroutine(setMoveOnWall());
        }
        else if (other.tag == "Wall")
        {
            if (moveOnWall)
            {
                onWall = true;
                onRoad = true;
                isMove = false;
                myBody.useGravity = false;
                if (other.transform.position.x > 0)
                {
                    //Wall Right
                    RotateCharacter(0, 0, 60);
                    ChangeCam("CamWallRight");
                }
                else
                {
                    //Wall Left
                    RotateCharacter(0, 0, -60);
                    ChangeCam("CamWallLeft");
                }

                SetSpeed(SpeedRoad);
            }
        }
        else if (other.tag == "BoxingGloves")
        {
            other.transform.parent.transform.Find("Boxing_Gloves").transform.GetComponent<Animator>().enabled = true;
        }
        else if (other.tag == "Flag")
        {
            other.transform.gameObject.GetComponent<Animator>().enabled = true;
        }
        else if (other.tag == "Spike")
        {
            if (isUnDead == false)
            {
                pushOpposite();
                myLevel -= 10000;
            }
        }
        else if (other.tag == "Enemy")
        {
            SeBonus = other.GetComponent<Enemy>().SeBonus;
            if (!unLimitDamage)
            {
                if (other.GetComponent<Enemy>().level <= myLevel)
                {
                    SetAttack();
                    Destroy(other.transform.Find("EnemyModel").gameObject);
                    Destroy(other.transform.Find("TextLevel").gameObject);
                    Destroy(other.transform.Find("LowerLevel").gameObject);
                    myLevel += other.GetComponent<Enemy>().levelBonus;
                    FloatingTextAnimator.Play("FloatingText");
                    FloatingTextUp.GetComponent<TextMeshPro>().text = "+" + other.GetComponent<Enemy>().levelBonus + " level";
                    if (other.GetComponent<Enemy>().levelBonus >= 10)
                    {
                        FloatingTextUp.SetActive(false);
                        FloatingTextUp.SetActive(true);
                        OnParticle(4); OnParticle(6);
                    }
                }
                else
                {
                    if (other.GetComponent<Enemy>().unDamage)
                    {
                        StartCoroutine(VictoryInStageEnding());
                        UpdateDiamond();
                        SetDieTrue();
                        SetSpeed(0);
                        myLevel -= 10000;
                    }
                    else
                    {
                        SetAttack();
                        Destroy(other.transform.Find("EnemyModel").gameObject);
                        Destroy(other.transform.Find("TextLevel").gameObject);
                        myLevel += other.GetComponent<Enemy>().levelBonus;
                        FloatingTextAnimator.Play("FloatingText");
                        FloatingTextUp.GetComponent<TextMeshPro>().text = "+" + other.GetComponent<Enemy>().levelBonus + " level";
                        if (other.GetComponent<Enemy>().levelBonus >= 10)
                        {
                            FloatingTextUp.SetActive(false);
                            FloatingTextUp.SetActive(true);
                            OnParticle(4); OnParticle(6);
                        }
                    }
                }
            }
            else
            {
                SetAttack();
                Destroy(other.transform.Find("EnemyModel").gameObject);
                Destroy(other.transform.Find("TextLevel").gameObject);
                other.transform.Find("EnemyRagdoll").gameObject.SetActive(true);
                myLevel += other.GetComponent<Enemy>().levelBonus;
                FloatingTextAnimator.Play("FloatingText");
                FloatingTextUp.GetComponent<TextMeshPro>().text = "+" + other.GetComponent<Enemy>().levelBonus + " level";
                if (other.GetComponent<Enemy>().levelBonus >= 10)
                {
                    FloatingTextUp.SetActive(false);
                    FloatingTextUp.SetActive(true);
                    OnParticle(4); OnParticle(6);
                }
            }
            CheckLevelEnemy();
        }
        else if (other.tag == "Enemy3")
        {
            SeBonus = other.GetComponent<Enemy>().SeBonus;
            if (!unLimitDamage)
            {
                if (other.GetComponent<Enemy>().level <= myLevel)
                {
                    //anim
                    myAnim.SetBool("AttackBoss", true);
                    //
                    Destroy(other.transform.Find("EnemyModel").gameObject);
                    Destroy(other.transform.Find("TextLevel").gameObject);
                    Destroy(other.transform.Find("LowerLevel").gameObject);
                    other.transform.Find("EnemyRagdoll").gameObject.SetActive(true);
                    myLevel += other.GetComponent<Enemy>().levelBonus;
                    FloatingTextAnimator.Play("FloatingText");
                    FloatingTextUp.GetComponent<TextMeshPro>().text = "+" + other.GetComponent<Enemy>().levelBonus + " level";
                    if (other.GetComponent<Enemy>().levelBonus >= 10)
                    {
                        FloatingTextUp.SetActive(false);
                        FloatingTextUp.SetActive(true);
                        OnParticle(4); OnParticle(6);
                    }
                }
                else
                {
                    if (other.GetComponent<Enemy>().unDamage)
                    {
                        StartCoroutine(VictoryInStageEnding());
                        UpdateDiamond();
                        SetDieTrue();
                        SetSpeed(0);
                        myLevel -= 10000;
                    }
                    else
                    {
                        hurt();
                        pushOppsiteEnemy();
                    }
                }
            }
            else
            {
                Destroy(other.transform.Find("EnemyModel").gameObject);
                Destroy(other.transform.Find("TextLevel").gameObject);
                other.transform.Find("EnemyRagdoll").gameObject.SetActive(true);
                myLevel += other.GetComponent<Enemy>().levelBonus;
                FloatingTextAnimator.Play("FloatingText");
                FloatingTextUp.GetComponent<TextMeshPro>().text = "+" + other.GetComponent<Enemy>().levelBonus + " level";
                if (other.GetComponent<Enemy>().levelBonus >= 10)
                {
                    FloatingTextUp.SetActive(false);
                    FloatingTextUp.SetActive(true);
                    OnParticle(4);
                    OnParticle(6);
                }

            }
            CheckLevelEnemy();
        }
        else if (other.tag == "Diamond")
        {
            AudioAssistant.Shot(TYPE_SOUND.Diamond);
            MMVibrationManager.Haptic(HapticTypes.Selection, false, true);
            QualityDiamond += 1;
            Destroy(other.gameObject);
            Save.Diamond.text = QualityDiamond.ToString();
            DiamondFound += 1;
            DiamondBonusInHole += 1;
            OnParticle(7);
        }
        else if (other.tag == "Key")
        {
            Destroy(other.gameObject);
            if (!PlayerPrefs.HasKey("key"))
            {
                PlayerPrefs.SetString("key", 0.ToString());
            }
            int keyCurrent = System.Int32.Parse(PlayerPrefs.GetString("key"));
            Debug.LogWarning(keyCurrent);
            keyCurrent += 1; Debug.LogWarning(keyCurrent);
            PlayerPrefs.SetString("key", keyCurrent.ToString());
            AnimKey.SetActive(true);
        }
        else if (other.tag == "EnemyStageEnding")
        {
            BossEndingBonus = true;
            //
            int numberTimesKillBoss = PlayerPrefs.GetInt("NumberTimesKillBoss");
            numberTimesKillBoss += 1;
            PlayerPrefs.SetInt("NumberTimesKillBoss", numberTimesKillBoss);
            //
            Time.timeScale = 0.7f;
            SetSpeed(0);
            Jump(0, 26, 0);
            SetVictoryTrue();
            StartCoroutine(WaitToVictory(1.5f));
            startgame = false;
            EnemyEndingRagdoll = other.transform.Find("EnemyRagdoll").gameObject;
            try
            {
                EnemyEndingModel = other.transform.Find("EnemyModel").gameObject;
                FireWorkEnemyEnding = other.transform.Find("FireWork").GetChild(0).gameObject;
            }
            catch
            {

            }
        }
        else if (other.tag == "EndBlackHole")
        {
            //bat canvas
            CanvasX2Hole.SetActive(true);
            //settext
            CanvasX2Hole.transform.Find("BackGround").Find("Diamond").Find("Text").gameObject.GetComponent<Text>().text = DiamondBonusInHole.ToString();
            //bam gio tat canvas
            SetSpeed(0);
            StartCoroutine(WaitX2Hole());
            //
        }
        else if (other.tag == "JumpAttack")
        {
            attackObject.SetActive(true);
            StartCoroutine(jumpAttack());
        }
        else if (other.tag == "StartCurve")
        {
            GetComponent<PathCreation.Examples.PathFollower>().moveOnCurve = true;
            GetComponent<PathCreation.Examples.PathFollower>().pathCreator = other.transform.parent.Find("ControllerCurve").GetComponent<PathCreation.PathCreator>();
            SetSpeed(0);
        }
        else if (other.tag == "EndCurve")
        {
            GetComponent<PathCreation.Examples.PathFollower>().moveOnCurve = false;
            GetComponent<PathCreation.Examples.PathFollower>().pathCreator = null;
            GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = 0;
            isMove = true;
            Jump(0, 17, 0);
            SetSpeed(SpeedRoad);
            SetJumpWall();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall")
        {
            if (moveOnWall)
            {
                unLimitDamage = true;
                if (other.transform.position.x > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x - 0.5f, other.transform.position.y + 1, transform.position.z), 0.2f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 70)), 0.1f);
                    //transform.localEulerAngles = new Vector3(0, 0, 70);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x + 0.5f, other.transform.position.y + 1, transform.position.z), 0.2f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, -70)), 0.1f);
                    //transform.localEulerAngles = new Vector3(0, 0, -70);
                }
            }
        }
        else if (other.tag == "StageEnding")
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(other.transform.position.x, transform.position.y, transform.position.z), 0.2f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bridge")
        {
            isMove = true;
            Jump(0, 17, 0);
            SetSpeed(SpeedRoad);
            SetJumpWall();
        }
        else if (other.tag == "Wall")
        {
            if (moveOnWall)
            {
                moveOnWall = false;
                onWall = false;
                unLimitDamage = false;
                myBody.useGravity = true;
                RotateCharacter(0, 0, 0);

                JumpLow();
                SetSpeed(SpeedRoad);
                isMove = true;
                if (transform.position.x < 0)
                {
                    pushLeft();
                }
                else
                {
                    pushRight();
                }

                //
                onRoad = false;
                SetJumpWall();
                ChangeCam("CamRun");
                //
            }
        }
        else if (other.tag == "StartBlackHole")
        {
            myposition = transform.position;
            myposition.z += 1;
            transform.position = other.GetComponent<BlackHole>().start.position;
            ObjectFollowCharacter.GetComponent<CameraFollow2>().PositionYOnJump = transform.position.y + 2;
            ObjectFollowCharacter.transform.position = transform.position;
            DiamondBonusInHole = 0;
            //Cam
            OffCam();
            CamInHole = other.transform.parent.parent.Find("BlackHole").Find("CamInHole").gameObject;
            CamInHole.SetActive(true);
            CamInHole.GetComponent<CinemachineVirtualCamera>().Follow = ObjectFollowCharacter.transform;
        }
        else if (other.tag == "Road")
        {
            onRoad = false;
        }
        else if (other.tag == "JumpHigh")
        {
            SetJumpAttack360(true);
            onRoad = false;
        }
        else if (other.tag == "Enemy")
        {
            SetSkin();
            SetAttackFalse();
            other.transform.gameObject.SetActive(false);
        }
        else if (other.tag == "Enemy3")
        {
            SetSkin();
            myAnim.SetBool("AttackBoss", false);
            other.transform.gameObject.SetActive(false);
        }
    }
    public void ButtonX2Hole()
    {
        ///Ads
        WatchAdsX2Hole();
    }
    private void ChangeCamInHoleToRun()
    {
        CamInHole.SetActive(false);
        ChangeCam("CamRun");
    }
    public void ButtonHole()
    {
        buttonHoleGet = true;
        transform.position = myposition;
        ObjectFollowCharacter.GetComponent<CameraFollow2>().PositionYOnJump = transform.position.y;
        ChangeCamInHoleToRun();
        CanvasX2Hole.SetActive(false);
        isMove = true;
        SetSpeed(SpeedRoad);
        DiamondBonusInHole = 0;
    }
    public bool adsShowing = false;
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsX2Hole()
    {

        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }

        if (adsShowing)
            return;


        if (!GameManager.EnableAds)
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsX2Hole, "ButtonX2Hole");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(OnCompleteAdsX2Hole, "ButtonX2Hole");
        }
#endif
    }
    private void OnCompleteAdsX2Hole(int value)
    {
        AnalyticManager.LogWatchAds("AdsInHole", 1);
        transform.position = myposition;
        ObjectFollowCharacter.GetComponent<CameraFollow2>().PositionYOnJump = transform.position.y;
        isMove = true;
        ChangeCamInHoleToRun();
        buttonHoleGet = true;
        adsShowing = false;
        CanvasX2Hole.SetActive(false);
        SetSpeed(SpeedRoad);
        QualityDiamond += DiamondBonusInHole;
        Save.Diamond.text = QualityDiamond.ToString();
        DiamondFound += DiamondBonusInHole;
        DiamondBonusInHole = 0;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void WatchAdsAFK25s()
    {
        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }

        if (adsShowing)
            return;


        if (GameManager.EnableAds)
        {
            AdManager.Instance.ShowInterstitial("AdsAFK25s", 1);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ADSReborn()
    {
        if (!GameManager.NetworkAvailable)
        {
            PopupNoInternet.Show();
            return;
        }

        if (adsShowing)
            return;


        if (!GameManager.EnableAds)
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(ReBorn, "Reborn");
        }
#if !PROTOTYPE
        else
        {
            adsShowing = true;
            AdManager.Instance.ShowAdsReward(ReBorn, "Reborn");
        }
#endif
    }
}
