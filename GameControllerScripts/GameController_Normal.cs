using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//using System;

public partial class GameController_Normal : MonoBehaviour
{

    [Header("�پ��� �ܺ� ����")][Space(5)]
    public ExternalGameObjects externalGameObjects;

    [Header("������� ����")]
    public CommonUserObjects myCommonObjects;

    [Header("�ٸ� ������� ����")]
    public CommonUserObjects[] otherCommonObjects = new CommonUserObjects[3];

    [Header("�⺻ info")] [Space(5)]
    public MainGameBaseInfo mainGameBaseInfo;

    [Header("���� �⺻ info")] [Space(5)]
    public PlayerBaseInfo myPlayerBaseInfo;
    //������ ���� �� UI�� �����Ѵ�.
    public MyPlayerUI myPlayerUI;

    //�׻� �⺻������ 3����� �޾ƾ��Ѵ�.
    public PlayerBaseInfo[] otherPlayerBaseInfo = new PlayerBaseInfo[3];

    [Header("���� �÷��� ���� info")] [Space(5)]
    public PlayerGameInfo myPlayerGameInfo;
    [Space(5)]
    public PlayerGameInfoControl myPlayerGameInfoControl;

    [Space(5)]
    public PlayerGameInfo[] otherPlayerGameInfo = new PlayerGameInfo[3];


    // Start is called before the first frame update
    void Start()
    {

        //���� ���� �ʱ�ȭ
        InitailizeGameSettings();
        
    }

    // Update is called once per frame
    void Update()
    {
        GamePlay();
    }

    void GamePlay()
    {

        switch (mainGameBaseInfo.nowGameStepEnum)
        {
            case NowGameStepEnum.GameEntrance:

                switch (mainGameBaseInfo.nowGameStepDetailEnum)
                {
                    case NowGameStepDetailEnum.GameEntrance_Ready:
                        CheckEntranceReady();
                        break;

                    case NowGameStepDetailEnum.GameEntrance_ShowReady:
                        StartEntranceShow();
                        break;

                    case NowGameStepDetailEnum.GameEntrance_Show:
                        break;

                    case NowGameStepDetailEnum.GameEntrance_Dialogue:
                        CheckEntranceDialogue();
                        break;

                    case NowGameStepDetailEnum.GameEntrance_Wait:
                        CheckEntranceWait();
                        break;
                }

                break;

            case NowGameStepEnum.MainGame_Initialize:

                switch (mainGameBaseInfo.nowGameStepDetailEnum)
                {
                    //�� ���� ���� �÷���
                    case NowGameStepDetailEnum.MainGame_InitialFirstSetting:
                        InitialFirstSetting();
                        break;

                    case NowGameStepDetailEnum.MainGame_InitialFirstPull:
                        //���·θ� �����ִ�.
                        //InitialFirstPull();
                        break;


                    case NowGameStepDetailEnum.MainGame_InitialFirstSelectionSetting:
                        InitialFirstSelectionSetting();
                        break;

                    case NowGameStepDetailEnum.MainGame_InitialFirstSelectionReady:
                        break;

                    case NowGameStepDetailEnum.MainGame_InitialFirstSelectionWaiting:
                        CheckEveryoneSelected();
                        break;



                    case NowGameStepDetailEnum.MainGame_InitialFirstPlayer:
                        //JudgeFirstPlayer();
                        break;

                    case NowGameStepDetailEnum.MainGame_InitialFirstDump:
                        DumpInitialStones();
                        break;

                    case NowGameStepDetailEnum.MainGame_InitialFirstEnd:
                        StartMainGame();
                        break;

                }

                break;

            case NowGameStepEnum.MainGame_Neutral:
                switch (mainGameBaseInfo.nowGameStepDetailEnum)
                {
                    case NowGameStepDetailEnum.MainGame_NeutralTurnCheck:
                        NeutralTurnChecking();
                        break;

                    case NowGameStepDetailEnum.MainGame_NeutralUserAllCheck:
                        NeutralUserAllChecking();
                        break;

                    case NowGameStepDetailEnum.MainGame_NeutralError:
                        NeutralErrorChecking();
                        break;

                    case NowGameStepDetailEnum.MainGame_NeutralRoundEnd:

                        break;
                }


                break;

            case NowGameStepEnum.MainGame_Other:
                switch (mainGameBaseInfo.nowGameStepDetailEnum)
                {
                    //��� �÷���
                    case NowGameStepDetailEnum.MainGame_OtherReady:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherWait:
                        OtherWaitingForSelect();
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherIntegration:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherDump:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherRecover:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherUtilize:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherEnd:
                        OtherEndTurn();
                        break;

                    //��� ���� �÷���
                    case NowGameStepDetailEnum.MainGame_OtherCompletion:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherAttackStep:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherSurrender:
                        break;

                    //����� ��縦 ���� �ܰ�
                    case NowGameStepDetailEnum.MainGame_OtherShow:
                        
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherDialogue:
                        break;
                }

                break;

            case NowGameStepEnum.MainGame_My:
                switch (mainGameBaseInfo.nowGameStepDetailEnum)
                {
                    //�ڽ� �÷���
                    //UI���� �Ϸ�
                    case NowGameStepDetailEnum.MainGame_MyReady:
                        //MyReadyShow();
                        break;

                    case NowGameStepDetailEnum.MainGame_MyWait:
                        //AnalyzeMyStonesAndActiveUI(bool tempActiveAll)
                        //�� �Լ��� ����� UI�� ����Ǵ� ��

                        //my control type�� ���� �ٸ��� ���� ��
                        MyWaitingForSelectUI();
                        break;

                    case NowGameStepDetailEnum.MainGame_MyIntegration:
                        break;

                    case NowGameStepDetailEnum.MainGame_MyDump:
                        break;

                    case NowGameStepDetailEnum.MainGame_MyRecover:
                        break;

                    case NowGameStepDetailEnum.MainGame_MyUtilize:
                        break;

                    case NowGameStepDetailEnum.MainGame_MyEnd:
                        MyEndTurn();
                        break;

                    //�ڽ� ���� �÷���
                    case NowGameStepDetailEnum.MainGame_MyCompletion:
                        break;

                    case NowGameStepDetailEnum.MainGame_MyAttackStep:
                        break;

                    case NowGameStepDetailEnum.MainGame_MySurrender:
                        break;

                    //����� ��縦 ���� �ܰ�
                    case NowGameStepDetailEnum.MainGame_MyShow:
                        break;

                    case NowGameStepDetailEnum.MainGame_MyDialogue:
                        break;
                }

                break;

            case NowGameStepEnum.EndGame:
                switch (mainGameBaseInfo.nowGameStepDetailEnum)
                {
                    case NowGameStepDetailEnum.EndGame_Show:
                        break;

                    case NowGameStepDetailEnum.EndGame_Dialogue:
                        break;

                    case NowGameStepDetailEnum.EndGame_Result:
                        break;

                    case NowGameStepDetailEnum.EndGame_Exit:
                        break;
                }

                break;

        }
    }

    [System.Serializable]
    public class CommonUserObjects
    {
        [Space(10)]
        [Header("�⺻ ��� GameObject")]
        //[Space(5)]
        public TableController TableController;
        [Space(5)]
        //�⺻ �߱� ������Ʈ
        public SliderController SliderController;

        [Space(5)]
        public CrystalController CrystalController;
        public GameObject CrystalShiningEffect;

        [Space(5)]
        public GameObject sliderHandler;

        [Space(15)]
        public StoneOnHandBrief[] StoneOnHandBriefsPair = new StoneOnHandBrief[2];
        [Space(5)]
        public Effect_StoneDissolve[] Effect_StoneDissolvesPair = new Effect_StoneDissolve[2];
   
    }

    [System.Serializable]
    public class ExternalGameObjects
    {
        [Space(5)]
        public GameObjectSpawner gameObjectSpawner;
        public StoneClickController stoneClickController;
        public DumpController dumpController;
        public AudioManagerInGame audioManagerInGame;

        [Space(15)]
        [Header("���� Effect")]
        [Space(5)]
        public Effect_Darker effect_Darker;
        
        [Space(5)]
        public List<GameObject> SquareFlashList;

        //�� �����
        //dissolve list�� �ְ� �����ϰ� �ı��Ѵ�.
        [Space(5)]
        //public GameObject Effect_StoneDissolve;
        private List<GameObject> Effect_StoneDissolveList;
        //public Effect_StoneDissolve effect_StoneDissolve;

        //�� ��¦�̸� ����Ǵ� ����Ʈ
        [Space(5)]
        public Effect_ShiningGathering effect_ShiningGathering;

        //�� ������ ��¦�̴� ����Ʈ
        [Space(5)]
        public GameObject MyTurnEffect;


        [Space(15)]
        [Header("Selection ���� GameObject")]
        [Space(5)]
        public GameObject SelectionNormalAll;
        public List<PlaneAltarController> SelectionControllerList;
        [Space(10)]
        public GameObject SelectionSmallAll;
        public List<PlaneAltarController> SelectionSmallControllerList;
        public GameObject SelectionSmallEffect;
        [Space(10)]
        public GameObject SelectionLargeAll;
        public List<PlaneAltarController> SelectionLargeControllerList;
        [Space(5)]
        public GameObject initialSelectionButton;
        public GameObject dumpExecutionButton;
        [Space(5)]
        public GameObject recoverExecutionButton;
        public GameObject integrationExecutionButton;

        //[Space(5)]
        //public GameObject selectionShow;
        //2���� �ش�
        //public List<StoneOnHandBrief> MyStoneOnHandBriefList;
        //6���� �ش�
        //public List<StoneOnHandBrief> OtherStoneOnHandBriefList;


        /*
        [Space(5)]
        [Header("Selection ���� GameObject")]
        [Space(5)]
        //�⺻ �߱� ������Ʈ
        public GameObject baseShinigObject;
        */
    }


}
