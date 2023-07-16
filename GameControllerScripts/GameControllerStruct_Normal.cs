using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public partial class GameController_Normal : MonoBehaviour
{
    #region enums

    //게임 진행 대략적 단계
    public enum NowGameStepEnum
    {

        GameEntrance,
        MainGame_Initialize,
        MainGame_Neutral,
        MainGame_Other,
        MainGame_My,
        EndGame,

    }

    //게임 진행 세부 단계
    public enum NowGameStepDetailEnum
    {
        //아직 본격 시작 전
        GameEntrance_Ready,

        //연출과 함께 게임 시작
        GameEntrance_ShowReady,
        //연출 중
        GameEntrance_Show,

        //개암 사적 사 대화
        GameEntrance_Dialogue,
        //게임 시작 시 대기
        GameEntrance_Wait,


        //선잡기 과정
        //고르고 버리고 대기하고 게임시작대기
        MainGame_InitialFirstSetting,
        MainGame_InitialFirstPull,

        MainGame_InitialFirstSelectionSetting,
        MainGame_InitialFirstSelectionReady,
        MainGame_InitialFirstSelectionWaiting,
        
        MainGame_InitialFirstPlayer,
        MainGame_InitialFirstDump,
        MainGame_InitialFirstEnd,

        //중립 처리
        //턴 이동 처리
        MainGame_NeutralTurnCheck,
        //모든 유저의 상태 및 네트워크를 확인 후 넘어간다.
        MainGame_NeutralUserAllCheck,
        //만약 강종이 발생할 경우 처리
        MainGame_NeutralError,

        //매 라우드 종료 처리
        //무효가 된 판도 여기로 귀결된다.
        MainGame_NeutralRoundEnd,

        //상대플레이 관련--------------------------------

        //상대 준비 확인
        MainGame_OtherReady,
        //시작 2장 드로우
        //MainGame_OtherPull,
        //상대 고민페이즈
        MainGame_OtherWait,
        MainGame_OtherIntegration,
        MainGame_OtherDump,
        MainGame_OtherRecover,
        MainGame_OtherUtilize,
        MainGame_OtherEnd,
        //완성 후 피해, End로 넘어간다.
        MainGame_OtherCompletion,
        MainGame_OtherAttackStep,

        //항복
        MainGame_OtherSurrender,

        //연출과 대사를 위한 단계
        MainGame_OtherShow,
        MainGame_OtherDialogue,


        //자신플레이 관련--------------------------------

        //자신 준비 호가인
        MainGame_MyReady,
        //시작 2장 드로우
        //MainGame_MyPull,
        //자신 고민페이즈
        MainGame_MyWait,
        MainGame_MyIntegration,
        MainGame_MyDump,
        MainGame_MyRecover,
        MainGame_MyUtilize,
        MainGame_MyEnd,
        //완성 후 피해, End로 넘어간다.
        MainGame_MyCompletion,
        MainGame_MyAttackStep,

        //항복
        MainGame_MySurrender,

        //연출과 대사를 위한 단계
        MainGame_MyShow,
        MainGame_MyDialogue,

        //게임 종료 단계--------------------------------
        EndGame_Show,
        EndGame_Dialogue,
        EndGame_Result,
        EndGame_Exit,

    }

    //custom game은 만들어진 덱으로 플레이
    //noraml game은 
    public enum GameType
    {
        storyCustomGame,
        storyNormalGame,
        
        versusNormalGame,
    }

    public enum MyControlType
    {
        none,
        dump,
        integration,
        recover,
        recoverDumpSelect,
        completion,
        useArcane,
    }

    public enum StoneForm
    {
        normalRevealed,
        normalConcealed,

        completionStair,
        completionSame,
    }

    public enum StoneType
    {
        arcane,
        element,
        optic,
        mineral,
    }

    //비교 목적의 참조 목적으로도 사용하는 수치
    public enum StoneDetailType
    {
        //완성된것 별도로
        completion,

        //165~
        arcane,

        //0~14
        element_fire,
        //15~29
        element_land,
        //30~44
        element_ocean,
        //45~59
        element_forest,
        //60~74
        element_purple,

        //75~89
        optic_light,
        //90~104
        optic_dark,
        //105~119
        optic_chaos,

        //120~134
        mineral_gold,
        //135~149
        mineral_silver,
        //150~164
        mineral_blood,

        //디폴트용 마지막
        last,

    }

    public enum ArcaneStoneType
    {
        none,

        //일단 기본적으로 5개 이상 있도록 한다.
        lightningStone,
        reverseStone,
        gravityStone,
        induceStone,
        resonanceStone,
        rippleStone,
        burstStone,

        //아무런 효과 없는 돌조각
        //이건 무조건 마지막이 된다.
        blank,
    }

    //위의 참조용 List를 위한 이름
    public enum MultiplierReferenceName
    {
        multiplier_1111,
        multiplier_a111,

        multiplier_211,
        multiplier_2a1,
        multiplier_A11,

        multiplier_31,
        multiplier_3a,

        multiplier_22,
        multiplier_A2,

        multiplier_4,
    }

    #endregion

    //핵심 게임 데이터, 다양한 세부 수치-------------------------------------------------------------------------------------------
    [System.Serializable]
    public class MainGameBaseInfo
    {
        //대략적인 단계
        public NowGameStepEnum nowGameStepEnum;

        //실질적인 세부적인 단계
        public NowGameStepDetailEnum nowGameStepDetailEnum;

        //현재 게임의 종류
        public GameType gameType;


        [Space(15)]

        //현재 기본적인 돌의 개수
        //신비석은 제외한다(기본적으로 165개가 유지된다)
        public int baseStoneCount;

        //신비석의 개수(기본적으로 1->5개 된다)
        //isArcane도 당연히 활성화
        public int arcaneStoneCount;

        //나중에 isArcane이 해금된다.
        //그 전에는 blank 하나만 들어간다.
        public bool isArcane;


        [Space(15)]

        //인게임 story에서만 사용한다.
        //유저대전에서는 서버에서 받아온다.
        public List<int> PiledStonesForStoryList;

        //story목적 신비석 List도 만든다.
        public List<int> PiledArcaneStonesRefForStoryList;

        [Space(15)]

        //버려진 돌 저장 개수
        //기본적으로 6개를 저장한다.
        //그 이후에는 파괴된다.
        public int savedDumpedCounts;

        //현재 버려진 돌들
        public List<StoneInfo> DumpedStonesList;

        [Space(15)]
        //현재 파괴된 돌들
        public List<StoneInfo> DestroyedStonesList;



        [Space(15)]
        [Header("게임 진행 일반 정보")] [Space(5)]
        //최초 플레이어
        [ReadOnly] public int firstPlayerNum;
        [ReadOnly] public bool firstPlayerFailedBefore;

        [Space(5)]

        //점수 배수
        public int scoreRatio;

        [Space(5)]
        //현재 플레이 중인 유저 번호
        public int playedTurnThisRound;
        public int nowPlayingPlayer;
        public int nextPlayingPlayer;

        [Space(5)]
        //현재 판 수
        public int nowRounds;

        //목표 판 수
        public int targetRounds;

        //게임 동안 총 턴 수
        public int allPlayedTurnCounts;

        [Space(10)]
        public MyControlType nowMyControlType;


        [Space(5)]
        //최근 합성에 성공한 유저
        //-1은 아무도 한 적 없을 때
        //그 외에는 0,1,2,3
        public int latestTurnUser = -1;
        public int latestCompletedUser;

        [Space(5)]
        [Header("기보별 점수 배수표")]
        [Space(5)]
        //점수 참조용 List
        public List<int> CommonMultiplierReferenceList = new List<int> { 2, 4, 3, 6, 12, 5, 10, 5, 20, 20 };
        
        
        #region entrance flow
        [Space(10)]
        [Header("Entrance 흐름 제어")] [Space(5)]
        public bool entranceReadyPass;
        public bool entranceShowPass;
        public bool entrancedialoguePass;
        public bool entranceWaitPass;

        [Space(5)]
        [Header("빠른 패스")] [Space(5)]
        public bool entranceAllPass;
        #endregion

        #region maingame initial
        [Space(10)]
        [Header("MainGame Initial 흐름 제어")] [Space(5)]

        //bool이 아닌 int로 바꿨다.
        public int initialSelectionFinishedPass;
        [Space(5)]
        public List<bool> initialSelectionFinishedList;

        // -1은 같은 점수 일 때 손해
        // 0은 무보정
        // 1은 같은 점수 일 때 이득
        public judgeType judgeBonus = judgeType.none;
        public enum judgeType
        {
            none,
            benefit,
            penalty,
        }


        #endregion


        #region maingame neutral
        [Space(10)]
        [Header("MainGame Initial 흐름 제어")]
        [Space(5)]
        public List<bool> neutralNetworkCheckingList;
        #endregion

        //#region maingame my avail functions


        //게임 최초 시작 시 설정, 목표 처리와 초기화
        public void ResetGameBaseInfo(int tempTargetRounds)
        {
            //목표한 라운드 수
            targetRounds = tempTargetRounds;

            nowRounds = 0;

            
            playedTurnThisRound = 0;
            allPlayedTurnCounts = 0;
        }

        public void ResetRoundBaseInfo()
        {
            nowRounds++;
            playedTurnThisRound = 0;
        }

    }
    //----------------------------------------------------------------------------------------------------------------------------

    //조합용 class
    // -2은 미적용, -1은 완성, 0~4는 해당 값만 미존재

    [System.Serializable]
    public class CompletionTypeElement
    {
        [ReadOnly] public List<int> completionSameList;

        [ReadOnly] public List<int> completionStairList;

        [ReadOnly] public int completionStairLarge;


        //초기화
        public void CompletionTypeElementReset()
        {
            completionSameList = new List<int>() { -2, -2, -2, -2, -2 };

            completionStairList = new List<int>() { -2, -2, -2 };

            completionStairLarge = -2;
        }
    }


    [System.Serializable]
    public class CompletionTypeOpticMineral
    {
        [ReadOnly] public List<int> completionSameList;

        [ReadOnly] public int completionStair;

        [ReadOnly] public List<int> completionSameLargeList;


        //초기화
        public void CompletionTypeOpticMineralReset()
        {
            completionSameList = new List<int>() { -2, -2, -2};

            completionStair = -2;

            completionSameLargeList = new List<int>() { -2, -2, -2 };
        }

    }

    [System.Serializable]
    public class CompletionTypeArcane
    {
        [ReadOnly] public int completionSame;

        [ReadOnly] public int completionSameLarge;

        public void CompletionTypeArcaneReset()
        {
            completionSame = -2;

            completionSameLarge = -2;
        }
    }

    [System.Serializable]
    public class CompletedStonesInfo
    {
        //자신 합성 횟수
        [ReadOnly] public int integratedStoneCount;

        //자신 복원 횟수
        [ReadOnly] public int recoveredStoneCount;


        [Space(5)]

        //현재 가능한 최대 점수
        //상대의 합성, 복원에 의한 점수는 추산하지 않는다.
        //이 점수를 이용해 기보들을 비교해 max값을 가져온다.
        [ReadOnly] public int maxAvailScore;

        [Space(5)]
        //만약 점수가 중복된다면 sub reference에 집어넣는다.
        [ReadOnly] public int resultReference;

        [Space(5)]
        //중복이 나올 경우 subreference에 집어넣는다.
        [ReadOnly] public List<int> subReference;

        [Space(5)]
        //참조 대상인 reference
        public List<availableGemListInfo> AvailableGemList;

    }

    [System.Serializable]
    public class availableGemListInfo
    {
        //원래 4개의 Gem에 대해서 연산

        [ReadOnly] public List<Gem> availableGems;

        [Space(5)]
        [ReadOnly] public int calculatedScore;

        //배수의 연산
        [ReadOnly] public int resultMultiplier;

        //모두 stair인지 체크
        [ReadOnly] public bool isAllStair;
        [ReadOnly] public bool isAllSame;

        [Space(5)]
        //나중에 점수 보정을 해야하니까 함수에 직접 넣지는 않는다.
        //나중에 함수를 통해 갱신하도록 만든다.
        [ReadOnly] public int[] TempMultiplierReferenceList = new int[10] { 2, 4, 3, 6, 12, 5, 10, 5, 20, 20};

        [Space(5)]
        //기타 다양한 점수
        [ReadOnly] public int baseGemScore = 2;

        //보너스 점수
        //나중에 변경 가능하다.
        [ReadOnly] public int sameGemBonusScore = 1;
        [ReadOnly] public int stairGemBonusScore = 0;

        [Space(5)]
        //원소 동일 보너스 점수
        //2중,3중,4중 로 나눠서 점수를 부여
        [ReadOnly] public List<int> elementIterationBonusScore = new List<int>{ 1, 2, 4 };

        [Space(5)]
        //공격 시 사용하려는 목적
        [ReadOnly] public StoneDetailType mainStoneDetailType;


        //multiplier 의 연산
        //AllStair 와 AllSame 의 연산
        //ElementIteration
        public void CalculateScoreInfo()
        {
            // 1 신비석
            // 2~6 원소석
            // 7~9 광암석
            // 10~12 철광석
            //StoneDetailType
            
            //12개
            List<int> tempList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


            // ElementIteration 참조형
            // 9종의 completion 종류
            // 나중에 2개 3개 4개를 확인한다.
            List<int> tempElementIteration = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };



            //3개씩 존재하나
            bool is3Iterated = false;
            bool is4Iterated = false;

            isAllStair = true;
            isAllSame = true;

            //Gem 정보를 참조해서 stair same 여부, 중첩개수를 체크한다.
            //tempElementIteration 도 채운다.
            for (int i=0; i< availableGems.Count; i++)
            {
                if(availableGems[i].isSame == 1)
                {
                    isAllStair = false;
                }
                //계단 또는 신비
                else
                {
                    isAllSame = false;
                }

                int tempRef = ((int)(availableGems[i].detailType)) - 1;

                if (availableGems[i].isLarge)
                {
                    tempList[tempRef] += 2;
                }
                else
                {
                    tempList[tempRef] += 1;
                }

                //element 타입이면
                if(availableGems[i].elementSubType != -1)
                {
                    //하나씩 증가
                    tempElementIteration[availableGems[i].elementSubType]++;
                }

            }

            //만약 전체개수가 1개이면 그냥 모두 false 처리한다.
            if (availableGems.Count < 2)
            {
                isAllStair = false;
                isAllSame = false;
            }


            //3중첩, 4중첩의 경우를 확인한다.
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i] == 3)
                {
                    is3Iterated = true;
                }
                else if (tempList[i] == 4)
                {
                    is4Iterated = true;
                }
            }


            //신비석 없는 경우
            if (tempList[0] == 0)
            {
                if(availableGems.Count == 4)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_1111];
                }
                else if (availableGems.Count == 3)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_211];
                }
                else if (availableGems.Count == 2)
                {
                    //3개가 있으면 multiplier_31
                    if (is3Iterated)
                    {
                        resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_31];
                    }
                    //없으면 multiplier_22
                    else
                    {
                        resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_22];
                    }
                }
                else if(availableGems.Count == 1)
                {
                    //사실 의미는 없는데 혹시나 해서 넣는다.
                    if (is4Iterated)
                    {
                        resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_4];
                    }
                    else
                    {
                        resultMultiplier = 1;
                    }
                }
                else
                {
                    resultMultiplier = 1;
                }
            }
            //신비석 1개
            else if(tempList[0] == 1)
            {
                if (availableGems.Count == 4)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_a111];
                }
                else if (availableGems.Count == 3)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_2a1];
                }
                else if(availableGems.Count == 2)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_3a];
                }
            }
            //신비석 2개
            else if (tempList[0] == 2)
            {
                if (availableGems.Count == 3)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_A11];
                }
                else if(availableGems.Count == 2)
                {
                    resultMultiplier = TempMultiplierReferenceList[(int)MultiplierReferenceName.multiplier_A2];
                }
                else
                {
                    resultMultiplier = 1;
                }
            }
            else
            {
                //오류 대비
                resultMultiplier = 1;
            }


            //-------------------------------------------------------------------

            //element bonus 연산
            // 1은 아무 의미 없다. 2,3,4에 대해 bonus 점수를 처리한다.
            for (int i=0; i< tempElementIteration.Count; i++)
            {
                //2번 중첩
                if (tempElementIteration[i] == 2)
                {
                    CheackAndChangeBonusScore(i, elementIterationBonusScore[0]);
                }
                //3번 중첩
                else if (tempElementIteration[i] == 3)
                {
                    CheackAndChangeBonusScore(i, elementIterationBonusScore[1]);
                }
                //4번 중첩
                else if (tempElementIteration[i] == 4)
                {
                    CheackAndChangeBonusScore(i, elementIterationBonusScore[2]);
                }
            }

        }

        //함수 길이 조절 때문에 분리한다.
        public void CheackAndChangeBonusScore(int tagetElementSubType, int targetScore)
        {
            for (int i = 0; i < availableGems.Count; i++)
            {
                if(availableGems[i].elementSubType == tagetElementSubType)
                {
                    availableGems[i].elementBonusScore = targetScore;
                }
            }
        }
    }


    #region Script Stucts

    [System.Serializable]
    public class Gem
    {
        //false면 소형, true면 대형
        public bool isLarge;

        // 1~12
        public StoneDetailType detailType;

        // same = 1, stair = 0, arcane = -1;
        public int isSame;

        // sub type
        // element 보너스 점수용 인자
        // -1은 그 외의 경우, 0~8은 element completion 종류
        public int elementSubType = -1;

        // 연산을 통해 추가 점수 부여
        public int elementBonusScore = 0;

    }

    [System.Serializable]
    public class AvailableStoneList
    {

    }

    [System.Serializable]
    public struct EntranceFlowControl
    {

    }

    [System.Serializable]
    public struct SelectionUtility
    {

    }

    [System.Serializable]
    public class StoneInfo
    {
        //현재 상태
        [ReadOnly] public StoneForm stoneForm;
        //만약 완성된 돌일 경우에는 순번을 부여한다.
        [ReadOnly] public int completedSeq;

        //4종
        [ReadOnly] public StoneType stoneType;

        //세부종류
        [ReadOnly] public StoneDetailType stoneDetailType;

        //0~4 or 0~2
        [ReadOnly] public int stoneNumber;

        //이건 추후 추가될 가능성이 높다.
        [ReadOnly] public ArcaneStoneType arcaneStoneType;

        //탐색용 지표
        //완성된 돌 0~9
        //신비석 10~19
        //일반 100 + (stone detail+1) x 10 + stone number
        [ReadOnly] public int stoneArrangeReference;

        //그냥 지금까지 받은 개수
        [ReadOnly] public int stoneAccumulatedReference;

        //생성자
        public StoneInfo(StoneDetailType tempStoneDetailTypeEnum, int tempStoneNumber, int subReference, int accReference)
        {
            //일단 세부 종류부터 적용
            stoneDetailType = tempStoneDetailTypeEnum;

            //세부 디테일의 범위에 따라 대략 디테일의 범위도 적용한다.
            //completion의 경우
            if (tempStoneDetailTypeEnum == StoneDetailType.completion)
            {
                //일단 잠시 보류
            }
            //신비석
            else if (tempStoneDetailTypeEnum == StoneDetailType.arcane)
            {
                stoneType = StoneType.arcane;
                stoneNumber = -2;
                arcaneStoneType = (ArcaneStoneType)tempStoneNumber;
                stoneArrangeReference = 10 + subReference;
            }
            //원소석
            else if ((int)tempStoneDetailTypeEnum < (int)StoneDetailType.optic_light)
            {
                stoneType = StoneType.element;
                stoneNumber = tempStoneNumber % 5;
                arcaneStoneType = ArcaneStoneType.none;
                stoneArrangeReference = 100 + (((int)stoneDetailType - (int)(StoneDetailType.element_fire)) * 10) + stoneNumber;
            }
            //광암석
            else if ((int)tempStoneDetailTypeEnum < (int)StoneDetailType.mineral_gold)
            {
                stoneType = StoneType.optic;
                stoneNumber = tempStoneNumber % 3;
                arcaneStoneType = ArcaneStoneType.none;
                stoneArrangeReference = 200 + (((int)stoneDetailType - (int)(StoneDetailType.optic_light)) * 10) + stoneNumber;
            }
            //광물석
            else if ((int)tempStoneDetailTypeEnum < (int)StoneDetailType.last)
            {
                stoneType = StoneType.mineral;
                stoneNumber = tempStoneNumber % 3;
                arcaneStoneType = ArcaneStoneType.none;
                stoneArrangeReference = 300 + (((int)stoneDetailType - (int)(StoneDetailType.mineral_gold)) * 10) + stoneNumber;
            }

            //받은 순차 계산
            stoneAccumulatedReference = accReference;
        }

    }

    [System.Serializable]
    public struct RecentHistory
    {
        //등급 정도
        public int rank;

        //등수(1~4)
        public int grade;

        //최고 피해
        public int maxDamage;
    }

    #endregion
}

