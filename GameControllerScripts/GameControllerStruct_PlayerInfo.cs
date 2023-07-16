using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameController_Normal : MonoBehaviour
{

    //다양한 유저 기본정보
    [System.Serializable]
    public class PlayerBaseInfo
    {
        //현재 장착한 외형 정보
        public int avatarNum;
        public int droneNum;
        public int stageNum;

        //true면 일반 유저, 아니면 AI
        public bool isPlayer;

        //일반 유저가 아니면 만들어진 데이터이다.
        //기본적으로 서버로부터 받아오게 된다.
        public List<RecentHistory> recentHistoryList;

    }

    [System.Serializable]
    public class MyPlayerUI
    {

        //일단 과거 UI
        /*
        //종료 버튼
        public GameObject finishButton;

        //합성 버튼들은 여러번 눌러서 교환할 수 있다.
        public GameObject integrationButtonIdentical;
        public GameObject integrationButtonIdentical_Large;
        public GameObject integrationButtonSequential;
        public GameObject integrationButtonSequential_Large;

        */

        //전체 UI GameObject
        public GameObject UIAll;

        //합성한다.
        public GameObject integrationButton;

        //떠다니는 6개의 돌 중 하나를 선택한다.
        public GameObject recoverButton;


        //신비석은 버리기 처리 시 
        //신비석 활용 버튼
        //slelect 0번으로 돌 하나 골라서 버린다.
        //public GameObject utilizeButton;
        

        //버리기 버튼(select 활성화)
        public GameObject dumpButton;
        //바로 버리기
        public GameObject dumpExecutionButton;

        [Space(10)]
        //되돌리기 버튼
        public GameObject returnUI;

        /*
        //다른 기능들을 눌렀을 때 기본 상태로 돌아온다.
        //돌아오기 버튼
        public GameObject returnButton;

        //재배치
        //이는 우하단에 배치되어있다.
        public GameObject rearrangeButton;
        */
    }

    //다양한 유저 기본정보
    [System.Serializable]
    public class PlayerGameInfo
    {
        //현재 획득 점수
        public int nowScore;

        [Space(5)]
        public bool isConnected;

        [Space(5)]
        //현재 살아있는지
        public bool isAlive;

        [Space(5)]
        //현재 완성된 돌의 개수
        public int nowCompletedStoneCount;
        //현재 가져온 신비석 개수
        public int nowHoldingArcaneCount;


        [Space(10)]
        //현재 해당 기능이 가능한지


        [Space(5)]
        //현재 가지고 있는 돌에 대한 세부 정보
        public List<StoneInfo> HoldingStone;

        //참조용 list
        //참조용 수치를 통해 빠르고 편하게 연산
        //기본적으로는 0으로만 이뤄져 있다.
        //완성(1) 신비석(1) 기본(5+3+3) 1+1+5+3+3 13종류
        [Space(10)]
        [Header("참조용 reference / 0 : 완성 / 1 : 신비석 / 2~6 : 원소석 / 7~9 : 광암석 / 10~12 : 철광석")]
        [Space(5)]
        public List<int> HoldingStoneReferenceCounts;

        //holding stone의 기본적인 reference를 제공한다.
        //my 유저만 이를 사용한다.
        #region rearrange


        //기본제공 arrange
        //일단 이걸 기준으로 작업하도록 하자
        //이게 게임에 보여지는 기준이다.
        [Space(5)]
        public List<int> HoldingStoneArrangeReference_Base;

        [Space(5)]
        bool isHoldStoneArrangeCalculated_Element;
        //실질적인 참조
        public List<int> HoldingStoneArrangeReference_Element;

        //일단 연산이 되었는지 확인한다.
        bool isHoldStoneArrangeCalculated_Optic;
        //실질적인 참조
        public List<int> HoldingStoneArrangeReference_Optic;

        //일단 연산이 되었는지 확인한다.
        bool isHoldStoneArrangeCalculated_Mineral;
        //실질적인 참조
        public List<int> HoldingStoneArrangeReference_Mineral;


        //나중에 완성된 것을 통한 reference도 만든다.
        bool isHoldStoneArrangeCalculated_AmountDescending;
        //실질적인 참조
        public List<int> HoldingStoneArrangeReference_AmountDescending;
        #endregion


        //현재까지 얻은 돌의 개수(같은 레퍼런스 때문)
        public int accumulatedCounts;

        [Space(5)]
        //기본적으로 선택한 돌들
        public List<int> SelectStonesRef;
        //public List<StoneInfo> selectedStones;

        //두 돌의 점수(first player selection)
        public int firstPlayerScore;


        //완성된 다양한 보석 종류
        [Space(10)]
        [Header("게임 진행 일반 정보")]
        [Space(5)]
        
        public List<int> RecoverAvailIndexList;
        public List<int> RecoverDumpIndexRefList;
        [Space(5)]
        public List<int> IntegrationIndexList;

        [Space(5)]
        public CompletionTypeArcane completionTypeArcane;
        public List<CompletionTypeElement> CompletionTypeElementsList;
        public List<CompletionTypeOpticMineral> CompletionTypeOpticList;
        public List<CompletionTypeOpticMineral> CompletionTypeMineralList;

        //완성 정보 종류
        [Space(10)]
        [Header("점수 산출 관련 정보")]
        [Space(5)]
        //점수 관련 총괄 정보
        public CompletedStonesInfo completedStonesInfo;


        //기본적인 생성자.
        public PlayerGameInfo()
        {
            nowCompletedStoneCount = 0;
        }

        //돌의 합성 선언 시
        public void PlayerStoneIntegration()
        {
            //돌의 제거
            nowCompletedStoneCount++;
        }

        //first selection 점수 연산
        public void CalFirstPlayerScore()
        {

            //제대로 뽑은 손패가 있을 때
            //혹시라도 전에 제대로 안 버렸으면 -99뜬다.
            if (SelectStonesRef.Count == 2)
            {
                //만약 둘의 타입이 같다면
                if (HoldingStone[SelectStonesRef[0]].stoneDetailType == HoldingStone[SelectStonesRef[1]].stoneDetailType)
                {
                    //같은 번호
                    if (HoldingStone[SelectStonesRef[0]].stoneNumber == HoldingStone[SelectStonesRef[1]].stoneNumber)
                    {
                        firstPlayerScore = 80;
                    }
                    //연속됨(원소)
                    else if (HoldingStone[SelectStonesRef[0]].stoneType == StoneType.element)
                    {
                        if (HoldingStone[SelectStonesRef[0]].stoneNumber + 1 == HoldingStone[SelectStonesRef[1]].stoneNumber)
                        {
                            firstPlayerScore = 60;
                        }
                        //연속됨
                        else if (HoldingStone[SelectStonesRef[0]].stoneNumber == HoldingStone[SelectStonesRef[1]].stoneNumber + 1)
                        {
                            firstPlayerScore = 60;
                        }
                        else
                        {
                            firstPlayerScore = 40;
                        }
                    }
                    //그 외 광암 철광 연속
                    else
                    {
                        firstPlayerScore = 40;
                    }

                }
                else if (HoldingStone[SelectStonesRef[0]].stoneType == HoldingStone[SelectStonesRef[1]].stoneType)
                {
                    firstPlayerScore = 20;
                    CalFirstPlayerScoreSub();
                }
                else
                {
                    firstPlayerScore = 0;
                    CalFirstPlayerScoreSub();
                }

            }
            else
            {
                //혹시나 예외처리
                firstPlayerScore = -99;
            }

        }

        //함수 단축용
        public void CalFirstPlayerScoreSub()
        {
            //패의 숫자 크기 적용
            int tempStoneNum0 = 0;
            int tempStoneNum1 = 0;

            //원소만 숫자를 반영한다.
            //광암 신비는 무조건 1로 취급한다.
            if (HoldingStone[SelectStonesRef[0]].stoneType == StoneType.element)
            {
                tempStoneNum0 = HoldingStone[SelectStonesRef[0]].stoneNumber + 1;
            }
            else
            {
                tempStoneNum0 = 1;
            }


            if (HoldingStone[SelectStonesRef[1]].stoneType == StoneType.element)
            {
                tempStoneNum1 = HoldingStone[SelectStonesRef[1]].stoneNumber + 1;
            }
            else
            {
                tempStoneNum1 = 1;
            }

            firstPlayerScore = firstPlayerScore + tempStoneNum0 + tempStoneNum1;
        }

        //=============================================================================================
        //게임 초기화 함수와 라운드 초기화 함수

        #region clear function

        //게임 초가화 함수
        public void ClearPlayerGameInfo()
        {
            nowScore = 0;
            isAlive = true;

            nowCompletedStoneCount = 0;
            nowHoldingArcaneCount = 0;
            accumulatedCounts = 0;

            HoldingStone.Clear();
            ResetHoldingStoneReferenceCounts();

            SelectStonesRef.Clear();
            firstPlayerScore = 0;

            //부가적인 배치 초기화
            ResetIsHoldStoneArrangeCalculated();
            ResetHoldingArrangeReference();

            //탐색 초기화
            CompletionReset();

        }

        //라운드 초기화
        //점수는 초기화 하면 안된다.
        //죽은 상태도 복구할 필요 없다.
        public void ClearPlayerRoundInfo()
        {

            nowCompletedStoneCount = 0;
            nowHoldingArcaneCount = 0;
            accumulatedCounts = 0;

            HoldingStone.Clear();
            ResetHoldingStoneReferenceCounts();

            SelectStonesRef.Clear();
            firstPlayerScore = 0;

            //부가적인 배치 초기화
            ResetIsHoldStoneArrangeCalculated();
            ResetHoldingArrangeReference();

            //탐색 초기화
            CompletionReset();

        }
        #endregion

        //=============================================================================================

        //현재 유저가 들고있는 돌을 순서대로 정리
        //최초 사용하는 함수
        public void RearrangeHoldingStones()
        {

            for (int i = 0; i < HoldingStone.Count; i++)
            {

                StoneInfo tempStone = HoldingStone[i];

                for (int j = 0; j < i; j++)
                {
                    if (tempStone.stoneArrangeReference < HoldingStone[j].stoneArrangeReference)
                    {
                        HoldingStone.RemoveAt(i);

                        HoldingStone.Insert(j, tempStone);

                        break;
                    }
                }

            }
        }


        public void RearrangeHoldingStonesMinor()
        {

            //HoldingStone.Insert(null);

        }

        //reference 새로 초기화
        public void ResetIsHoldStoneArrangeCalculated()
        {
            isHoldStoneArrangeCalculated_Element = false;
            isHoldStoneArrangeCalculated_Optic = false;
            isHoldStoneArrangeCalculated_Mineral = false;
            isHoldStoneArrangeCalculated_AmountDescending = false;
        }

        public void ResetHoldingArrangeReference()
        {
            HoldingStoneArrangeReference_Element.Clear();
            HoldingStoneArrangeReference_Optic.Clear();
            HoldingStoneArrangeReference_Mineral.Clear();
            HoldingStoneArrangeReference_AmountDescending.Clear();
        }

        //=============================================================================================
        //전체적인 completion check 용 함수들

        #region Completion Check

        //완성 가능한 정보를 갱신
        //PullOneStone(0) 이곳에 이 함수를 넣는다.
        //드로우 된 돌의 정보에 따라서 재배치한다.
        //일단 이 함수는 생략
        public void RenewCompletionInfo(StoneDetailType targetDetailType)
        {
            int tempNum = (int)targetDetailType;

            int tempMainType;

            //해당 타입 중 몇번째에 해당하는지
            //land : 1 / chaos : 2 / 
            int tempSubTypeNum;

            #region typeIfElse
            if(tempNum == 0)
            {
                //완성된 것의 경우
                tempMainType = -1;
                tempSubTypeNum = -1;
            }
            else if (tempNum == 1)
            {
                tempMainType = 0;
                tempSubTypeNum = tempNum;
            }
            else if (tempNum < 7)
            {
                tempMainType = 1;
                tempSubTypeNum = tempNum - 2;
            }
            else if (tempNum < 10)
            {
                tempMainType = 2;
                tempSubTypeNum = tempNum - 7;
            }
            else if (tempNum < 13)
            {
                tempMainType = 3;
                tempSubTypeNum = tempNum - 10;
            }
            else
            {
                tempMainType = -1;
                tempSubTypeNum = 0;
            }
            #endregion


            switch (tempMainType)
            {
                //신비석
                case 0:
                    CalculateAracaneReference();
                    break;

                //원소석
                case 1:
                    CalculateElementReference(tempSubTypeNum, tempNum);
                    break;

                //광암석
                case 2:
                    CalculateOpticReference(tempSubTypeNum, tempNum);
                    break;

                //철광석
                case 3:
                    CalculateMineralReference(tempSubTypeNum, tempNum);
                    break;

                default:

                    break;

            }

        }

        //초기에 전체 갱신
        //일단 개별 갱신은 생략, 이 함수가 현재 기본 갱신함수
        public void RenewCompletionInfoAll()
        {
            RecoverAvailIndexList.Clear();
            RecoverDumpIndexRefList.Clear();

            IntegrationIndexList.Clear();

            CalculateAracaneReference();

            //원소석 갱신
            for(int i=(int)(StoneDetailType.element_fire); i< (int)(StoneDetailType.optic_light); i++)
            {
                CalculateElementReference(i- (int)(StoneDetailType.element_fire), i);
            }

            //광암석 갱신
            for (int i = (int)(StoneDetailType.optic_light); i < (int)(StoneDetailType.mineral_gold); i++)
            {
                CalculateOpticReference(i - (int)(StoneDetailType.optic_light), i);
            }

            //철광석 갱신
            for (int i = (int)(StoneDetailType.mineral_gold); i < (int)(StoneDetailType.last); i++)
            {
                CalculateMineralReference(i - (int)(StoneDetailType.mineral_gold), i);
            }

        }

        public void CalculateAracaneReference()
        {
            //int startValue = HoldingStoneReferenceCountsAccumulation(1);

            if (HoldingStoneReferenceCounts[1] > 1)
            {

                if (HoldingStoneReferenceCounts[1] == 2)
                {
                    //1개가 비어있는 상태
                    completionTypeArcane.completionSame = 0;
                    completionTypeArcane.completionSameLarge = -2;

                    RecoverAvailIndexList.Add(0);
                }
                else if(HoldingStoneReferenceCounts[1] == 3)
                {
                    //완성된 상태
                    completionTypeArcane.completionSame = -1;
                    completionTypeArcane.completionSameLarge = -2;

                    RecoverAvailIndexList.Add(0);

                    IntegrationIndexList.Add(0);
                }
                else if(HoldingStoneReferenceCounts[1] == 4)
                {
                    completionTypeArcane.completionSame = -1;
                    completionTypeArcane.completionSameLarge = 0;

                    RecoverAvailIndexList.Add(0);
                    RecoverAvailIndexList.Add(1);

                    IntegrationIndexList.Add(0);
                }
                else if (HoldingStoneReferenceCounts[1] >= 5)
                {
                    completionTypeArcane.completionSame = -1;
                    completionTypeArcane.completionSameLarge = -1;

                    RecoverAvailIndexList.Add(0);
                    RecoverAvailIndexList.Add(1);

                    IntegrationIndexList.Add(0);
                    IntegrationIndexList.Add(1);
                }
                else
                {
                    completionTypeArcane.completionSame = -2;
                    completionTypeArcane.completionSameLarge = -2;
                }

                return;
            }
            else
            {
                return;
            }

        }

        public void CalculateElementReference(int subNum, int originRef)
        {
            int startValue = HoldingStoneReferenceCountsAccumulation(originRef);

            //2개 이상일 때에만 활성화
            //그 외에는 발동필요 없다.
            if (HoldingStoneReferenceCounts[originRef] > 1)
            {

                //completionSameList에 대한 연산
                //completionSameList를 위한 SameCheckList를 생성

                #region completion same
                List<int> SameCheckList = new List<int>() { 0,0,0,0,0 };

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    SameCheckList[HoldingStone[i].stoneNumber]++;

                }

                //5개의 completionSameList
                for (int i=0; i<5; i++)
                {
                    if(SameCheckList[i] > 1)
                    {
                        if(SameCheckList[i] == 2)
                        {
                            //same check list에 따라 변형한다.
                            //미완성인 부분
                            CompletionTypeElementsList[subNum].completionSameList[i] = i;

                            RecoverAvailIndexList.Add(100 + subNum * 10 + i);

                        }
                        else if(SameCheckList[i] >= 3)
                        {
                            CompletionTypeElementsList[subNum].completionSameList[i] = -1;

                            RecoverAvailIndexList.Add(100 + subNum * 10 + i);
                            IntegrationIndexList.Add(100 + subNum * 10 + i);
                        }
                        else
                        {
                            CompletionTypeElementsList[subNum].completionSameList[i] = -2;
                        }
                    }
                }
                #endregion

                //=======================================================================================

                //completionStairList / completionStairLarge 에 대한 연산
                // 012 / 123 / 234 / 01234 에 대한 조사

                #region completion stair
                List<bool> StairCheckList = new List<bool>() { false, false, false, false, false };
                int tempCount = 0;
                int tempRef = -2;

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    StairCheckList[HoldingStone[i].stoneNumber] = true;

                }


                // 012  123  234 의 연산
                for (int subRef = 0; subRef < 3; subRef++)
                {
                    tempCount = 0;
                    tempRef = -2;

                    for (int i = 0 + subRef; i < 3 + subRef; i++)
                    {
                        if (StairCheckList[i])
                        {
                            tempCount++;
                        }
                        else
                        {
                            tempRef = i;
                        }
                    }

                    if(tempCount == 3)
                    {
                        CompletionTypeElementsList[subNum].completionStairList[subRef] = -1;

                        RecoverAvailIndexList.Add(100 + subNum * 10 + subRef + 5);
                        IntegrationIndexList.Add(100 + subNum * 10 + subRef + 5);
                    }
                    else if(tempCount == 2)
                    {
                        CompletionTypeElementsList[subNum].completionStairList[subRef] = tempRef;

                        RecoverAvailIndexList.Add(100 + subNum * 10 + subRef + 5);
                    }
                    else
                    {
                        CompletionTypeElementsList[subNum].completionStairList[subRef] = -2;
                    }
                }

                // 01234 의 연산
                tempCount = 0;
                tempRef = -2;

                for (int i = 0; i < 5; i++)
                {
                    if (StairCheckList[i])
                    {
                        tempCount++;
                    }
                    else
                    {
                        tempRef = i;
                    }
                }

                if(tempCount == 5)
                {
                    CompletionTypeElementsList[subNum].completionStairLarge = -1;

                    RecoverAvailIndexList.Add(100 + subNum * 10 + 8);
                    IntegrationIndexList.Add(100 + subNum * 10 + 8);
                }
                else if(tempCount == 4)
                {
                    CompletionTypeElementsList[subNum].completionStairLarge = tempRef;

                    RecoverAvailIndexList.Add(100 + subNum * 10 + 8);
                }
                else
                {
                    CompletionTypeElementsList[subNum].completionStairLarge = -2;
                }


                #endregion

            }

        }

        public void CalculateOpticReference(int subNum, int originRef)
        {
            int startValue = HoldingStoneReferenceCountsAccumulation(originRef);
            //Debug.Log(startValue);

            //2개 이상일 때에만 활성화
            //그 외에는 발동필요 없다.
            if (HoldingStoneReferenceCounts[originRef] > 1)
            {

                //completionSameList에 대한 연산
                //completionSameList를 위한 SameCheckList를 생성

                #region completion same
                List<int> SameCheckList = new List<int>() { 0, 0, 0 };

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    SameCheckList[HoldingStone[i].stoneNumber]++;

                }

                //3가지 종류
                for (int i = 0; i < 3; i++)
                {
                    if (SameCheckList[i] > 1)
                    {
                        if (SameCheckList[i] == 2)
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = i;
                            //CompletionTypeOpticList[subNum].completionSameList[i] = subNum;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = -2;

                            RecoverAvailIndexList.Add(200 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] == 3)
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = -1;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = -2;

                            RecoverAvailIndexList.Add(200 + subNum * 10 + i);
                            IntegrationIndexList.Add(200 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] == 4)
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = -1;
                            //CompletionTypeOpticList[subNum].completionSameLargeList[i] = subNum;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = i;

                            RecoverAvailIndexList.Add(200 + subNum * 10 + i);
                            RecoverAvailIndexList.Add(200 + subNum * 10 + 4 + i);
                            IntegrationIndexList.Add(200 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] >= 5)
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = -1;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = -1;

                            RecoverAvailIndexList.Add(200 + subNum * 10 + i);
                            RecoverAvailIndexList.Add(200 + subNum * 10 + 4 + i);
                            IntegrationIndexList.Add(200 + subNum * 10 + i);
                            IntegrationIndexList.Add(200 + subNum * 10 + 4 + i);
                        }
                        else
                        {
                            CompletionTypeOpticList[subNum].completionSameList[i] = -2;
                            CompletionTypeOpticList[subNum].completionSameLargeList[i] = -2;
                        }
                    }
                }
                #endregion

                //=======================================================================================

                //completionStairList 에 대한 연산
                // 012 에 대한 조사

                #region completion stair
                List<bool> StairCheckList = new List<bool>() { false, false, false };
                int tempCount = 0;
                int tempRef = -2;

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    StairCheckList[HoldingStone[i].stoneNumber] = true;

                }

                // 012  의 연산
                tempCount = 0;
                tempRef = -2;

                for (int i = 0; i < 3; i++)
                {
                    if (StairCheckList[i])
                    {
                        tempCount++;
                    }
                    else
                    {
                        tempRef = i;
                    }
                }

                if (tempCount == 3)
                {
                    CompletionTypeOpticList[subNum].completionStair = -1;

                    RecoverAvailIndexList.Add(200 + subNum * 10 + 3);
                    IntegrationIndexList.Add(200 + subNum * 10 + 3);

                }
                else if (tempCount == 2)
                {
                    CompletionTypeOpticList[subNum].completionStair = tempRef;

                    RecoverAvailIndexList.Add(200 + subNum * 10 + 3);
                }
                else
                {
                    CompletionTypeOpticList[subNum].completionStair = -2;
                }
                

                #endregion

            }


        }

        public void CalculateMineralReference(int subNum, int originRef)
        {
            int startValue = HoldingStoneReferenceCountsAccumulation(originRef);

            //2개 이상일 때에만 활성화
            //그 외에는 발동필요 없다.
            if (HoldingStoneReferenceCounts[originRef] > 1)
            {

                //completionSameList에 대한 연산
                //completionSameList를 위한 SameCheckList를 생성

                #region completion same
                List<int> SameCheckList = new List<int>() { 0, 0, 0 };

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    SameCheckList[HoldingStone[i].stoneNumber]++;

                }

                //3가지 종류
                for (int i = 0; i < 3; i++)
                {
                    if (SameCheckList[i] > 1)
                    {
                        if (SameCheckList[i] == 2)
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = i;
                            //CompletionTypeMineralList[subNum].completionSameList[i] = subNum;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = -2;

                            RecoverAvailIndexList.Add(300 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] == 3)
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = -1;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = -2;

                            RecoverAvailIndexList.Add(300 + subNum * 10 + i);
                            IntegrationIndexList.Add(300 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] == 4)
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = -1;
                            //CompletionTypeMineralList[subNum].completionSameLargeList[i] = subNum;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = i;

                            RecoverAvailIndexList.Add(300 + subNum * 10 + i);
                            RecoverAvailIndexList.Add(300 + subNum * 10 + 4 + i);
                            IntegrationIndexList.Add(300 + subNum * 10 + i);
                        }
                        else if (SameCheckList[i] >= 5)
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = -1;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = -1;

                            RecoverAvailIndexList.Add(300 + subNum * 10 + i);
                            RecoverAvailIndexList.Add(300 + subNum * 10 + 4 + i);
                            IntegrationIndexList.Add(300 + subNum * 10 + i);
                            IntegrationIndexList.Add(300 + subNum * 10 + 4 + i);
                        }
                        else
                        {
                            CompletionTypeMineralList[subNum].completionSameList[i] = -2;
                            CompletionTypeMineralList[subNum].completionSameLargeList[i] = -2;
                        }
                    }
                }
                #endregion

                //=======================================================================================

                //completionStairList 에 대한 연산
                // 012 에 대한 조사

                #region completion stair
                List<bool> StairCheckList = new List<bool>() { false, false, false };
                int tempCount = 0;
                int tempRef = -2;

                for (int i = startValue; i < startValue + HoldingStoneReferenceCounts[originRef]; i++)
                {

                    StairCheckList[HoldingStone[i].stoneNumber] = true;

                }

                // 012  의 연산
                tempCount = 0;
                tempRef = -2;

                for (int i = 0; i < 3; i++)
                {
                    if (StairCheckList[i])
                    {
                        tempCount++;
                    }
                    else
                    {
                        tempRef = i;
                    }
                }

                if (tempCount == 3)
                {
                    CompletionTypeMineralList[subNum].completionStair = -1;

                    RecoverAvailIndexList.Add(300 + subNum * 10 + 3);
                    IntegrationIndexList.Add(300 + subNum * 10 + 3);
                }
                else if (tempCount == 2)
                {
                    CompletionTypeMineralList[subNum].completionStair = tempRef;

                    RecoverAvailIndexList.Add(300 + subNum * 10 + 3);
                }
                else
                {
                    CompletionTypeMineralList[subNum].completionStair = -2;
                }


                #endregion

            }

        }


        public void CompletionReset()
        {
            //값 초기화
            CompletionTypeElementsList.Clear();
            CompletionTypeOpticList.Clear();
            CompletionTypeMineralList.Clear();

            RecoverAvailIndexList.Clear();
            IntegrationIndexList.Clear();

            //내부 값 초기화
            completionTypeArcane.CompletionTypeArcaneReset();

            for (int i = 0; i < 5; i++)
            {
                CompletionTypeElement tempElement = new CompletionTypeElement();
                tempElement.CompletionTypeElementReset();
                CompletionTypeElementsList.Add(tempElement);
            }

            for (int i = 0; i < 3; i++)
            {
                CompletionTypeOpticMineral tempElement = new CompletionTypeOpticMineral();
                tempElement.CompletionTypeOpticMineralReset();
                CompletionTypeOpticList.Add(tempElement);
            }

            for (int i = 0; i < 3; i++)
            {
                CompletionTypeOpticMineral tempElement = new CompletionTypeOpticMineral();
                tempElement.CompletionTypeOpticMineralReset();
                CompletionTypeMineralList.Add(tempElement);
            }
        }


        #endregion

        //=============================================================================================


        //특정 지표값의 누적값을 받아온다.
        public int HoldingStoneReferenceCountsAccumulation(int tempRef)
        {
            int tempRes = 0;

            for (int i = 0; i < tempRef; i++)
            {
                tempRes = tempRes + HoldingStoneReferenceCounts[i];
            }

            return tempRes;
        }

        public void ResetHoldingStoneReferenceCounts()
        {
            HoldingStoneReferenceCounts.Clear();

            for (int i = 0; i < 13; i++)
            {
                HoldingStoneReferenceCounts.Add(0);
            }
        }

    }

    //추가적인 유저의 컨트롤에 대한 정보
    //이건 my player에만 해당하는 정보이다.
    [System.Serializable]
    public class PlayerGameInfoControl
    {

        //현재 드래그 중인 오브젝트 번호
        // -1은 조종하지 않음
        // -2은 돌이 돌아가는 중
        // 0~9 는 조종중인 돌 정보
        public int nowDraggingObjectNum;
        [Space(5)]
        public int tempSelectedDumpNum = -1;
        public PlayerGameInfoControl()
        {
            nowDraggingObjectNum = -1;
        }

        public void InitializePlayerGameInfoControl()
        {
            nowDraggingObjectNum = -1;
        }

    }

    //유저가 가진 돌에 대한 정보
    //게임 플레이의 핵심적은 class
    [System.Serializable]
    public class PlayerStoneInfo
    {


    }
}
