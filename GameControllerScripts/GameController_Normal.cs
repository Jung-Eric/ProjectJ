using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//using System;

public partial class GameController_Normal : MonoBehaviour
{

    [Header("다양한 외부 참조")][Space(5)]
    public ExternalGameObjects externalGameObjects;

    [Header("사용자의 참조")]
    public CommonUserObjects myCommonObjects;

    [Header("다른 사용자의 참조")]
    public CommonUserObjects[] otherCommonObjects = new CommonUserObjects[3];

    [Header("기본 info")] [Space(5)]
    public MainGameBaseInfo mainGameBaseInfo;

    [Header("유저 기본 info")] [Space(5)]
    public PlayerBaseInfo myPlayerBaseInfo;
    //유저는 게임 내 UI가 존재한다.
    public MyPlayerUI myPlayerUI;

    //항상 기본적으로 3명까지 받아야한다.
    public PlayerBaseInfo[] otherPlayerBaseInfo = new PlayerBaseInfo[3];

    [Header("헌지 플레이 세부 info")] [Space(5)]
    public PlayerGameInfo myPlayerGameInfo;
    [Space(5)]
    public PlayerGameInfoControl myPlayerGameInfoControl;

    [Space(5)]
    public PlayerGameInfo[] otherPlayerGameInfo = new PlayerGameInfo[3];


    // Start is called before the first frame update
    void Start()
    {

        //게임 설정 초기화
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
                    //매 라운드 시작 플레이
                    case NowGameStepDetailEnum.MainGame_InitialFirstSetting:
                        InitialFirstSetting();
                        break;

                    case NowGameStepDetailEnum.MainGame_InitialFirstPull:
                        //상태로만 남아있다.
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
                    //상대 플레이
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

                    //상대 공격 플레이
                    case NowGameStepDetailEnum.MainGame_OtherCompletion:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherAttackStep:
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherSurrender:
                        break;

                    //연출과 대사를 위한 단계
                    case NowGameStepDetailEnum.MainGame_OtherShow:
                        
                        break;

                    case NowGameStepDetailEnum.MainGame_OtherDialogue:
                        break;
                }

                break;

            case NowGameStepEnum.MainGame_My:
                switch (mainGameBaseInfo.nowGameStepDetailEnum)
                {
                    //자신 플레이
                    //UI연출 완료
                    case NowGameStepDetailEnum.MainGame_MyReady:
                        //MyReadyShow();
                        break;

                    case NowGameStepDetailEnum.MainGame_MyWait:
                        //AnalyzeMyStonesAndActiveUI(bool tempActiveAll)
                        //위 함수로 적용된 UI가 적용되는 중

                        //my control type에 따라 다르게 적용 중
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

                    //자신 공격 플레이
                    case NowGameStepDetailEnum.MainGame_MyCompletion:
                        break;

                    case NowGameStepDetailEnum.MainGame_MyAttackStep:
                        break;

                    case NowGameStepDetailEnum.MainGame_MySurrender:
                        break;

                    //연출과 대사를 위한 단계
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
        [Header("기본 기능 GameObject")]
        //[Space(5)]
        public TableController TableController;
        [Space(5)]
        //기본 발광 오브젝트
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
        [Header("공통 Effect")]
        [Space(5)]
        public Effect_Darker effect_Darker;
        
        [Space(5)]
        public List<GameObject> SquareFlashList;

        //돌 사라짐
        //dissolve list에 넣고 관리하고 파괴한다.
        [Space(5)]
        //public GameObject Effect_StoneDissolve;
        private List<GameObject> Effect_StoneDissolveList;
        //public Effect_StoneDissolve effect_StoneDissolve;

        //돌 반짝이며 흡수되는 이펙트
        [Space(5)]
        public Effect_ShiningGathering effect_ShiningGathering;

        //내 보석이 반짝이는 이펙트
        [Space(5)]
        public GameObject MyTurnEffect;


        [Space(15)]
        [Header("Selection 관련 GameObject")]
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
        //2개에 해당
        //public List<StoneOnHandBrief> MyStoneOnHandBriefList;
        //6개에 해당
        //public List<StoneOnHandBrief> OtherStoneOnHandBriefList;


        /*
        [Space(5)]
        [Header("Selection 관련 GameObject")]
        [Space(5)]
        //기본 발광 오브젝트
        public GameObject baseShinigObject;
        */
    }


}
