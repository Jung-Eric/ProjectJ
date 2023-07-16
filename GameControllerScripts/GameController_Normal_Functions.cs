using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameController_Normal : MonoBehaviour
{

    #region BaseFunctions
    public void InitailizeGameSettings()
    {
        //게임 초기화
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.GameEntrance;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.GameEntrance_Ready;

        //게임 일반 정보 초기화
        mainGameBaseInfo.firstPlayerNum = -2;
        mainGameBaseInfo.firstPlayerFailedBefore = false;


        mainGameBaseInfo.playedTurnThisRound = 0;
        mainGameBaseInfo.nowPlayingPlayer = -1;
        mainGameBaseInfo.nextPlayingPlayer = -1;

        mainGameBaseInfo.nowRounds = 0;


        //흐름제어 인자
        mainGameBaseInfo.initialSelectionFinishedPass = 0;

        //요소 초기화
        mainGameBaseInfo.initialSelectionFinishedList.Clear();

        mainGameBaseInfo.initialSelectionFinishedList = new List<bool>() { false, false, false, false };

        //전반 List 전부 클리어
        mainGameBaseInfo.PiledStonesForStoryList.Clear();
        mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Clear();
        mainGameBaseInfo.DumpedStonesList.Clear();
        mainGameBaseInfo.DestroyedStonesList.Clear();

        //네트워크 연결 체크
        mainGameBaseInfo.neutralNetworkCheckingList = new List<bool>() { false, false, false, false };

        //가진 돌에 대한 초기화
        myPlayerGameInfo.ClearPlayerGameInfo();

        for (int i = 0; i < 3; i++)
        {
            otherPlayerGameInfo[i].ClearPlayerGameInfo();
        }

        //나의 컨트롤에 대한 초기화
        myPlayerGameInfoControl.InitializePlayerGameInfoControl();


        //material 전부 초기화
        //externalGameObjects.initailSelectMaterial = 
    }

    //라운드 종료 후 새로운 세팅
    //새로운 라운드에 대한 세팅
    public void InitializeRoundSettings()
    {
        //게임 초기화
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Initialize;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSetting;


        //게임 일반 정보 초기화
        mainGameBaseInfo.firstPlayerNum = -2;
        mainGameBaseInfo.firstPlayerFailedBefore = false;


        mainGameBaseInfo.playedTurnThisRound = 0;
        mainGameBaseInfo.nowPlayingPlayer = -1;
        mainGameBaseInfo.nextPlayingPlayer = -1;

        //당연히 round는 1씩 추가된다.
        //mainGameBaseInfo.nowRounds = 0;


        //흐름제어 인자
        mainGameBaseInfo.initialSelectionFinishedPass = 0;

        //요소 초기화
        mainGameBaseInfo.initialSelectionFinishedList.Clear();

        mainGameBaseInfo.initialSelectionFinishedList = new List<bool>() { false, false, false, false };

        //전반 List 전부 클리어
        mainGameBaseInfo.PiledStonesForStoryList.Clear();
        mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Clear();
        mainGameBaseInfo.DumpedStonesList.Clear();
        mainGameBaseInfo.DestroyedStonesList.Clear();

        //가진 돌에 대한 초기화
        myPlayerGameInfo.ClearPlayerGameInfo();

        for (int i = 0; i < 3; i++)
        {
            otherPlayerGameInfo[i].ClearPlayerGameInfo();
        }

        //나의 컨트롤에 대한 초기화
        myPlayerGameInfoControl.InitializePlayerGameInfoControl();
    }

    #endregion

    #region EntranceFunctions

    //Etrance flow 함수
    void CheckEntranceReady()
    {
        if (mainGameBaseInfo.entranceReadyPass)
        {
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.GameEntrance_ShowReady;
            mainGameBaseInfo.entranceReadyPass = false;
        }

        if (mainGameBaseInfo.entranceAllPass)
        {
            mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Initialize;
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSetting;
            mainGameBaseInfo.entranceAllPass = false;
        }

    }

    void StartEntranceShow()
    {
        
        if (mainGameBaseInfo.entranceShowPass)
        {
            mainGameBaseInfo.entranceShowPass = false;

            SkipShow();            
        }
        else
        {
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.GameEntrance_Show;

            StartCoroutine(EntranceShow());
        }

        /*
        if (mainGameBaseInfo.entranceAllPass)
        {
            mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Initialize;
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSetting;
            mainGameBaseInfo.entranceAllPass = false;
        }
        */
    }


    IEnumerator EntranceShow()
    {
        float tempWaiting = 0.7f;

        yield return new WaitForSeconds(tempWaiting);

        otherCommonObjects[0].TableController.nowTableStatus = TableController.TableStatus.appear;

        yield return new WaitForSeconds(tempWaiting);

        otherCommonObjects[2].TableController.nowTableStatus = TableController.TableStatus.appear;

        yield return new WaitForSeconds(tempWaiting);

        otherCommonObjects[1].TableController.nowTableStatus = TableController.TableStatus.appear;

        yield return new WaitForSeconds(tempWaiting);

        myCommonObjects.TableController.nowTableStatus = TableController.TableStatus.appear;

        yield return new WaitForSeconds(1.1f);



        //크리스탈 활성화
        for (int i = 0; i < 3; i++)
        {
            otherCommonObjects[i].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;
        }

        myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;

        yield return new WaitForSeconds(0.3f);


        for (int i=0; i<3; i++)
        {
            otherCommonObjects[i].SliderController.gameObject.SetActive(true);
        }

        myCommonObjects.SliderController.gameObject.SetActive(true);



        //다음 단계로 넘어간다.
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.GameEntrance_Dialogue;

        yield break;
    }

    public void SkipShow()
    {

        otherCommonObjects[0].TableController.nowTableStatus = TableController.TableStatus.appear;

        otherCommonObjects[2].TableController.nowTableStatus = TableController.TableStatus.appear;

        otherCommonObjects[1].TableController.nowTableStatus = TableController.TableStatus.appear;

        myCommonObjects.TableController.nowTableStatus = TableController.TableStatus.appear;

        for (int i = 0; i < 3; i++)
        {
            otherCommonObjects[i].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;
        }

        myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;

        for (int i = 0; i < 3; i++)
        {
            otherCommonObjects[i].SliderController.gameObject.SetActive(true);
        }

        myCommonObjects.SliderController.gameObject.SetActive(true);

        //다음 단계로 넘어간다.
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.GameEntrance_Dialogue;
    }

    void CheckEntranceDialogue()
    {
        if (mainGameBaseInfo.entrancedialoguePass)
        {
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.GameEntrance_Wait;
            mainGameBaseInfo.entrancedialoguePass = false;
        }

        if (mainGameBaseInfo.entranceAllPass)
        {
            mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Initialize;
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSetting;
            mainGameBaseInfo.entranceAllPass = false;
        }
    }

    void CheckEntranceWait()
    {
        if (mainGameBaseInfo.entranceWaitPass)
        {
            mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Initialize;
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSetting;
            mainGameBaseInfo.entranceWaitPass = false;
        }

        if (mainGameBaseInfo.entranceAllPass)
        {
            mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Initialize;
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSetting;
            mainGameBaseInfo.entranceAllPass = false;
        }
    }

    #endregion

    #region InitialFunctions

    //시작 관련 코루틴 실행
    #region InitailPull Functions
    void InitialFirstSetting()
    {
        //최조 돌 세팅
        //신비석 세팅도 같이한다.
        GenerateNormalPilesForStory();

        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstPull;

        //자동으로 뽑가까지 넘어간다.
        StartCoroutine(AllUsersPullInitialStones());
    }

    //storyNormalGame 의 Piles 생성
    //blank가 하나있는 166 / 신비석이 있는 170개
    void GenerateNormalPilesForStory()
    {

        if (mainGameBaseInfo.PiledStonesForStoryList.Count != 0)
        {
            mainGameBaseInfo.PiledStonesForStoryList.Clear();
        }

        int tempCount = 0;

        //일단 개수를 조정한다.
        //신비석 활성 버전
        if (mainGameBaseInfo.isArcane)
        {
            tempCount = mainGameBaseInfo.baseStoneCount + mainGameBaseInfo.arcaneStoneCount;
        }
        //신비석 비활성 버전(blank 하나만 추가)
        else
        {
            tempCount = mainGameBaseInfo.baseStoneCount + 1;
        }

            

        for (int i = 0; i < tempCount; i++)
        {
            mainGameBaseInfo.PiledStonesForStoryList.Add(i);
        }

        //isArcane의 버전
        if (mainGameBaseInfo.isArcane)
        {
            //신비석용 배열 생성
            //1부터 blank 전까지 모두 담고본다. (0은 none)
            for (int i = 1; i < (int)ArcaneStoneType.blank; i++)
            {
                mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Add(i);
            }
        }
        //isArcane이 없는 버전
        else
        {
            mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Add((int)ArcaneStoneType.blank);
        }

    }

    IEnumerator AllUsersPullInitialStones()
    {
        //최초 10개의 돌을 뽑는다.
        //각 유저의 holding stone을 추가한다.

        //내 돌 10개 랜덤 뽑기
        for (int i = 0; i < 10; i++)
        {
            PullOneStone(0, false);
        }
        yield return null;

        //다른 유저들 뽑기
        for (int targetNum = 1; targetNum < 4; targetNum++)
        {
            for (int i = 0; i < 10; i++)
            {
                PullOneStone(targetNum, false);
            }
            yield return null;
        }

        
        //최초 뽑은 돌들이 등장하는 연출
        //오브젝트를 생성한 다음 보여준다.
        for(int i=0; i<10; i++)
        {
            //해당 오브젝트에 접근해 값 변경
            externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
        }
        yield return null;

        //등장 연출
        
        for (int i = 0; i < 10; i++)
        {
            //이전과는 다르게 다른 돌들의 등장도 처리한다.
            //my other 동시에 처리
            externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(i);
            yield return new WaitForSeconds(0.14f);
        }

        yield return new WaitForSeconds(0.4f);

        //잠깐 사라졌다가 다시 재배열 해서 나타난다
        externalGameObjects.gameObjectSpawner.StoneDisappearAllFunction();
        yield return new WaitForSeconds(0.3f);
        //사라지는 연출

        /*
        for (int i = 5; i < 10; i++)
        {
            externalGameObjects.gameObjectSpawner.StoneDisappearToDownFunction(i);
            yield return new WaitForSeconds(0.08f);
        }

        for (int i = 0; i < 5; i++)
        {
            externalGameObjects.gameObjectSpawner.StoneDisappearToDownFunction(i);
            yield return new WaitForSeconds(0.08f);
        }

        yield return new WaitForSeconds(0.3f);
        */

        //재배열
        myPlayerGameInfo.RearrangeHoldingStones();
        //내부 값 변경
        for (int i = 0; i < 10; i++)
        {
            //해당 오브젝트에 접근해 값 변경
            externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
        }
        yield return null;


        //다시 나타난다.
        externalGameObjects.gameObjectSpawner.StoneAppearAllFunction();
        //나타나는 연출
        
        /*
        for (int i = 0; i < 10; i++)
        {
            externalGameObjects.gameObjectSpawner.StoneAppearFromDownFunction(i);
            yield return new WaitForSeconds(0.08f);
        }
        yield return new WaitForSeconds(0.2f);
        */

        //나타난 다음에 다음 게임 스텝으로 넘어간다.
        yield return new WaitForSeconds(0.2f);

        //모든 돌을 활성화한다.
        //StoneMovableAllFunction
        externalGameObjects.gameObjectSpawner.StoneMovableAllFunction(true);


        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSelectionSetting;

        yield break;
    }

    //돌을 뽑는다.
    //0번은 본인, 1,2,3 순서대로 시계방향 다른 유저이다.
    public void PullOneStone(int userNum, bool isCompletionCheck)
    {
        //더미 중에서 무작위로 선택한다.
        int tempRefNum = Random.Range(0, mainGameBaseInfo.PiledStonesForStoryList.Count);

        //추출해서 제거한다.
        int tempTargetNum = mainGameBaseInfo.PiledStonesForStoryList[tempRefNum];
        mainGameBaseInfo.PiledStonesForStoryList.RemoveAt(tempRefNum);


        //랜덤으로 받아올 값
        int tempArcaneRefNum = -1;
        int tempTargetArcaneNum = -1;

        double tempdouble = 0;

        int tempTypeNum = 0;

        //Debug.Log(tempTargetNum);


        StoneInfo tempStone;

        //일반 연산
        //165이하의 값을 뽑았을 경우
        if (tempTargetNum < mainGameBaseInfo.baseStoneCount)
        {
            //mainGameBaseInfo.PiledArcaneStonesRefForStoryList;
            //번호를 해석해서 넣는다.
            tempdouble = tempTargetNum / 15;
            //내림 연산
            tempTypeNum = (int)System.Math.Truncate(tempdouble);

            //15개로 나눈 나머지
            int remainder = tempTargetNum % 15;

            //Debug.Log(tempRefNum);
            //Debug.Log(tempTypeNum);


            if (userNum == 0)
            {
                tempStone = new StoneInfo((StoneDetailType)(tempTypeNum + 2), remainder, -1, myPlayerGameInfo.accumulatedCounts);
                //레퍼런스도 추가
                myPlayerGameInfo.HoldingStoneReferenceCounts[tempTypeNum + 2]++;
            }
            else
            {
                tempStone = new StoneInfo((StoneDetailType)(tempTypeNum + 2), remainder, -1, otherPlayerGameInfo[userNum - 1].accumulatedCounts);
                //레퍼런스도 추가
                otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[tempTypeNum + 2]++;
            }

        }
        //신비석 연산
        else
        {
            if (mainGameBaseInfo.isArcane)
            {
                //현재 남은 무작위 신비석을 뽑는다.
                tempArcaneRefNum = Random.Range(0, mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Count);

                //추출해서 제거한다.
                tempTargetArcaneNum = mainGameBaseInfo.PiledArcaneStonesRefForStoryList[tempArcaneRefNum];
                mainGameBaseInfo.PiledArcaneStonesRefForStoryList.RemoveAt(tempArcaneRefNum);
            }
            //blank처리
            else
            {
                tempTargetArcaneNum = 8;
            }

            //신비석은 유저별로 다르게 처리한다.
            if (userNum == 0)
            {

                tempStone = new StoneInfo(StoneDetailType.arcane, tempTargetArcaneNum, myPlayerGameInfo.nowHoldingArcaneCount, myPlayerGameInfo.accumulatedCounts);

                //신비석 레퍼런스도 추가
                myPlayerGameInfo.HoldingStoneReferenceCounts[1]++;

                myPlayerGameInfo.nowHoldingArcaneCount++;

            }
            else
            {

                tempStone = new StoneInfo(StoneDetailType.arcane, tempTargetArcaneNum, otherPlayerGameInfo[userNum - 1].nowHoldingArcaneCount, otherPlayerGameInfo[userNum - 1].accumulatedCounts);

                //신비석 레퍼런스도 추가
                otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[1]++;

                otherPlayerGameInfo[userNum - 1].nowHoldingArcaneCount++;

            }

        }

        if (userNum == 0)
        {
            myPlayerGameInfo.accumulatedCounts++;
            myPlayerGameInfo.HoldingStone.Add(tempStone);


            //myPlayerGameInfo.RearrangeHoldingStones();

            //completion 갱신
            if (isCompletionCheck)
            {
                //myPlayerGameInfo.RenewCompletionInfoAll();
                //myPlayerGameInfo.RenewCompletionInfo(tempStone.stoneDetailType);
            }

        }
        else
        {
            otherPlayerGameInfo[userNum - 1].accumulatedCounts++;
            otherPlayerGameInfo[userNum - 1].HoldingStone.Add(tempStone);

            //completion 갱신
            if (isCompletionCheck)
            {
                //이건 other이 정리되지 않았기에 쓰면 안된다.
                //otherPlayerGameInfo[userNum - 1].RenewCompletionInfo(tempStone.stoneDetailType);
            }
        }

    }

    #endregion


    //선택 시작 관련 코루틴 실행
    void InitialFirstSelectionSetting()
    {
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSelectionReady;

        StartCoroutine(InitialFirstSelectionReady());
    }

    //코루틴 실행
    IEnumerator InitialFirstSelectionReady()
    {
        //게임 시작 시 잠시 대기
        yield return new WaitForSeconds(0.3f);

        //전부 끄기
        CrystalAllControl(0, -1);

        yield return new WaitForSeconds(0.2f);
   
        //slider바 활성화
        SliderHandlerOn(-1, false);
        //darker 활성화
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //제단 활성화
        //externalGameObjects.initialSelectionAltar.SetActive(true);

        externalGameObjects.SelectionControllerList[0].ColorSetting(0);
        externalGameObjects.SelectionControllerList[0].gameObject.SetActive(true);
        externalGameObjects.SelectionControllerList[0].planeAltarActive = true;

        externalGameObjects.SelectionControllerList[1].ColorSetting(0);
        externalGameObjects.SelectionControllerList[1].gameObject.SetActive(true);
        externalGameObjects.SelectionControllerList[1].planeAltarActive = true;

        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSelectionWaiting;
        yield break;
    }

    public void CrystalAllControl(int tempMode, int exeptionNum)
    {
        if (tempMode == 0)
        {
            //crystal 완전 꺼버리기
            if(exeptionNum != 0)
            {
                myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalDark;
            }
            
            for (int i = 0; i < 3; i++)
            {
                if(exeptionNum != i + 1)
                {
                    otherCommonObjects[i].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalDark;
                }
            }
        }
        else if (tempMode == 1)
        {
            //crystal 가볍게 끄기
            if (exeptionNum != 0)
            {
                myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalOff;
            }

            for (int i = 0; i < 3; i++)
            {
                if (exeptionNum != i + 1)
                {
                    otherCommonObjects[i].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalOff;
                }
            }
        }
        else if (tempMode == 2)
        {
            //crystal 비활성화
            if (exeptionNum != 0)
            {
                myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;
            }

            for (int i = 0; i < 3; i++)
            {
                if (exeptionNum != i + 1)
                {
                    otherCommonObjects[i].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;
                }
            }

        }


    }


    public void SliderHandlerOn(int tempTarget,bool tempOn)
    {
        if(tempTarget == -1)
        {
            myCommonObjects.sliderHandler.SetActive(tempOn);

            for (int i = 0; i < 3; i++)
            {
                otherCommonObjects[i].sliderHandler.SetActive(tempOn);
            }

        }
        else
        {

            if(tempTarget == 0)
            {
                myCommonObjects.sliderHandler.SetActive(tempOn);
            }
            else if(tempTarget >= 1 && tempTarget <= 3)
            {
                otherCommonObjects[tempTarget].sliderHandler.SetActive(tempOn);
            }

        }
        
    }
    

    //선택 연출 (altar 활성화)
    //drag 되는걸 인식해서 
    void CheckEveryoneSelected()
    {
        //Debug.Log("dd");
        //체크 중 둘 다 선택되면 버튼 활성화
        if ((externalGameObjects.SelectionControllerList[0].nowAppliedStoneRefNum !=-1) && (externalGameObjects.SelectionControllerList[1].nowAppliedStoneRefNum != -1))
        {
            externalGameObjects.initialSelectionButton.SetActive(true);
        }
        //그 외에는 비활성화
        else
        {
            externalGameObjects.initialSelectionButton.SetActive(false);
        }

        //4명 모두 선택시 넘어간다.
        if (mainGameBaseInfo.initialSelectionFinishedPass == 4)
        {
            externalGameObjects.initialSelectionButton.SetActive(false);

            //externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;
            //만약 모든 유저들이 뭐 선택했는지 확인한다면 다음으로 넘어간다.
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstPlayer;

            StartCoroutine(InitialFirstPlayerShow());
        }

    }


    IEnumerator InitialFirstPlayerShow()
    {

        //선택 후 드래그 됨, 다른것들 이미지 바꿔줌
        #region SelfDrag and SpriteChange
        externalGameObjects.stoneClickController.thisPlayingNow = false;

        //두개 동시에 활성화되서 자동으로 selection으로 위치로 끌려간다.
        //externalGameObjects.gameObjectSpawner
        externalGameObjects.SelectionControllerList[0].gameObject.SetActive(false);
        externalGameObjects.SelectionControllerList[1].gameObject.SetActive(false);


        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[0]].draggedTargetPosition = myCommonObjects.StoneOnHandBriefsPair[0].transform.position;
        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[0]].stoneNowStatus = StoneOnHand.StoneNowStatusEnum.normalDragControl_SelectionShrink_SelfDragged;

        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[1]].draggedTargetPosition = myCommonObjects.StoneOnHandBriefsPair[1].transform.position;
        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[1]].stoneNowStatus = StoneOnHand.StoneNowStatusEnum.normalDragControl_SelectionShrink_SelfDragged;

        yield return new WaitForSeconds(0.5f);

        
        //6개에 대한 등장 및 이미지 변경
        for (int i=0; i<3; i++)
        {
            for (int j = 1; j >= 0; j--)
            {
                //기존 들고있던 돌이 사라짐
                externalGameObjects.gameObjectSpawner.OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[j].gameObject.SetActive(false);

                //새롭게 등장
                otherCommonObjects[i].StoneOnHandBriefsPair[j].ActiveShowObjects(0);
                //externalGameObjects.OtherStoneOnHandBriefList[(2 * i) + j].ActiveShowObjects();

                //select의 0번 1번의 정보를 적용
                otherCommonObjects[i].StoneOnHandBriefsPair[j].ChangeSprite((int)otherPlayerGameInfo[i].HoldingStone[otherPlayerGameInfo[i].SelectStonesRef[j]].stoneDetailType, otherPlayerGameInfo[i].HoldingStone[otherPlayerGameInfo[i].SelectStonesRef[j]].stoneNumber);
                //externalGameObjects.OtherStoneOnHandBriefList[(2 * i) + j].ChangeSprite((int)otherPlayerGameInfo[i].HoldingStone[otherPlayerGameInfo[i].SelectStonesRef[j]].stoneDetailType, otherPlayerGameInfo[i].HoldingStone[otherPlayerGameInfo[i].SelectStonesRef[j]].stoneNumber);
                
                yield return new WaitForSeconds(0.2f);
            }

        }
        #endregion

        yield return new WaitForSeconds(0.5f);


        //회전해서 등장 현출, 내것도 연출용으로 전환
        #region Rotate And My Change
        //등장 후 돌 회전
        for (int i = 0; i < 3; i++)
        {
            for (int j = 1; j >= 0; j--)
            {
                otherCommonObjects[i].StoneOnHandBriefsPair[j].ActiveRotationObjects();
                //externalGameObjects.OtherStoneOnHandBriefList[(2 * i) + j].ActiveRotationObjects();

                yield return new WaitForSeconds(0.2f);
            }

        }

        //이펙트 발생을 위해서 이렇게 처리한다.
        //자동으로 끌어간 다음 기존 오브젝트는 비활성화 시킨다.
        //다시 새롭게 활성화한다.
        for (int i = 0; i < 2; i++)
        {
            myCommonObjects.StoneOnHandBriefsPair[i].gameObject.SetActive(true);

            myCommonObjects.StoneOnHandBriefsPair[i].ImmediateApplyObjects((int)myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[i]].stoneDetailType, myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[i]].stoneNumber);

            myCommonObjects.StoneOnHandBriefsPair[i].gameObject.SetActive(true);


            //externalGameObjects.MyStoneOnHandBriefList[i].gameObject.SetActive(true);

            //externalGameObjects.MyStoneOnHandBriefList[i].ImmediateApplyObjects((int)myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[i]].stoneDetailType, myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[i]].stoneNumber);

            externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[i]].gameObject.SetActive(false);

        }
        #endregion

        //점수 판결
        JudgeFirstPlayer(mainGameBaseInfo.judgeBonus);

        yield return new WaitForSeconds(0.1f);

        //사각형 이펙트
        #region square effect
        //본격적인 사격형 이펙트 발생
        //점수 판정에 따라 이펙트 보여줌
        if (mainGameBaseInfo.firstPlayerNum == 0)
        {
            externalGameObjects.SquareFlashList[0].SetActive(true);
        }
        else if(mainGameBaseInfo.firstPlayerNum == 1)
        {
            if (externalGameObjects.SquareFlashList[1].transform.localPosition.x > 0)
            {
                externalGameObjects.SquareFlashList[1].transform.localPosition = new Vector3(-externalGameObjects.SquareFlashList[1].transform.localPosition.x, externalGameObjects.SquareFlashList[1].transform.localPosition.y, externalGameObjects.SquareFlashList[1].transform.localPosition.z);
            }

            externalGameObjects.SquareFlashList[1].SetActive(true);
        }
        else if (mainGameBaseInfo.firstPlayerNum == 2)
        {
            externalGameObjects.SquareFlashList[2].SetActive(true);
        }
        else if (mainGameBaseInfo.firstPlayerNum == 3)
        {
            if (externalGameObjects.SquareFlashList[1].transform.localPosition.x < 0)
            {
                externalGameObjects.SquareFlashList[1].transform.localPosition = new Vector3(-externalGameObjects.SquareFlashList[1].transform.localPosition.x, externalGameObjects.SquareFlashList[1].transform.localPosition.y, externalGameObjects.SquareFlashList[1].transform.localPosition.z);
            }
            
            externalGameObjects.SquareFlashList[1].SetActive(true);
        }
        #endregion

        yield return new WaitForSeconds(1.0f);

        //동점 발생에 대한 임시방편
        //추후 연출 추가
        if(mainGameBaseInfo.firstPlayerNum == -1)
        {
            mainGameBaseInfo.firstPlayerNum = 1;
            //Debug.Log("동일 경우, 추후 추가 필요");
            //yield break;
        }


        //시작 유저 추가 이펙트
        //상자 빛나기 시작하며 보석에 흡수되는 연출
        if(mainGameBaseInfo.firstPlayerNum == 0)
        {

            myCommonObjects.Effect_StoneDissolvesPair[0].StartDissolveStone();
            myCommonObjects.Effect_StoneDissolvesPair[1].StartDissolveStone();

        }
        //이외 사라짐
        else
        {

            otherCommonObjects[mainGameBaseInfo.firstPlayerNum - 1].Effect_StoneDissolvesPair[0].StartDissolveStone();
            otherCommonObjects[mainGameBaseInfo.firstPlayerNum - 1].Effect_StoneDissolvesPair[1].StartDissolveStone();

        }

        yield return new WaitForSeconds(0.2f);

        if(mainGameBaseInfo.firstPlayerNum == 0)
        {
            myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_InitialSelection;
            myCommonObjects.CrystalShiningEffect.SetActive(true);
        }
        else if(mainGameBaseInfo.firstPlayerNum > 0 && mainGameBaseInfo.firstPlayerNum < 4)
        {
            otherCommonObjects[mainGameBaseInfo.firstPlayerNum-1].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_InitialSelection;
            otherCommonObjects[mainGameBaseInfo.firstPlayerNum - 1].CrystalShiningEffect.SetActive(true);
        }


        yield return new WaitForSeconds(0.7f);
        //이펙트 활성화
        externalGameObjects.effect_ShiningGathering.gameObject.SetActive(true);

        //externalGameObjects.SquareFlashList[0]

        if (mainGameBaseInfo.firstPlayerNum == 0)
        {
            externalGameObjects.effect_ShiningGathering.SetSetting(externalGameObjects.SquareFlashList[0].transform.position, myCommonObjects.CrystalController.transform);
        }
        else if(mainGameBaseInfo.firstPlayerNum == 1)
        {
            externalGameObjects.effect_ShiningGathering.SetSetting(externalGameObjects.SquareFlashList[1].transform.position, otherCommonObjects[0].CrystalController.transform);
        }
        else if (mainGameBaseInfo.firstPlayerNum == 2)
        {
            externalGameObjects.effect_ShiningGathering.SetSetting(externalGameObjects.SquareFlashList[2].transform.position, otherCommonObjects[1].CrystalController.transform);
        }
        else if (mainGameBaseInfo.firstPlayerNum == 3)
        {
            externalGameObjects.effect_ShiningGathering.SetSetting(externalGameObjects.SquareFlashList[1].transform.position, otherCommonObjects[2].CrystalController.transform);
        }

        //dissolve 파괴음
        yield return new WaitForSeconds(3.4f);

        externalGameObjects.audioManagerInGame.ChangeSFXAndPlay(AudioManagerInGame.SFXname.StoneBurn);

        yield return new WaitForSeconds(1.2f);

        //화면 다시 밝게
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;

        //크리스탈을 dark->off 로 되돌린다.
        for (int i = 0; i < 4; i++)
        {
            //동일하면 pass
            //다르면 변경
            if (mainGameBaseInfo.firstPlayerNum != i)
            {

                if (i == 0)
                {
                    myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalOff;
                }
                else
                {
                    otherCommonObjects[i - 1].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalOff;
                }

            }

        }

        #region effect all off
        //이펙트 모두 비활성화
        for (int i=0; i<3; i++)
        {
            externalGameObjects.SquareFlashList[i].SetActive(false);
            otherCommonObjects[i].CrystalShiningEffect.SetActive(false);
        }

        externalGameObjects.effect_ShiningGathering.gameObject.SetActive(false);
        myCommonObjects.CrystalShiningEffect.SetActive(false);
        #endregion

        //점수를 비교해 가장 높은 유저에게 선을 주고 시계방향으로 진행
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstDump;

        yield break;
    }

    //각자 2개 선택해서 점수 비교
    void JudgeFirstPlayer(MainGameBaseInfo.judgeType tempJudgeType)
    {
        //내점수 계산
        myPlayerGameInfo.CalFirstPlayerScore();

        //다른 유저 계산
        for (int i = 0; i < 3; i++)
        {
            otherPlayerGameInfo[i].CalFirstPlayerScore();
        }

        //-1 이면 최고점수가 중복되는 경우
        //int tempFirstPlayerNum = 0;
        int tempMaxScore = myPlayerGameInfo.firstPlayerScore;

        mainGameBaseInfo.firstPlayerNum = 0;


        if(tempJudgeType == MainGameBaseInfo.judgeType.none)
        {
            //본인 상대1번 비교
            if (myPlayerGameInfo.firstPlayerScore < otherPlayerGameInfo[0].firstPlayerScore)
            {
                mainGameBaseInfo.firstPlayerNum = 1;
                tempMaxScore = otherPlayerGameInfo[0].firstPlayerScore;
            }
            else if (myPlayerGameInfo.firstPlayerScore == otherPlayerGameInfo[0].firstPlayerScore)
            {
                mainGameBaseInfo.firstPlayerNum = -1;
            }

        }
        else if(tempJudgeType == MainGameBaseInfo.judgeType.benefit)
        {
            //본인 상대1번 비교
            if (myPlayerGameInfo.firstPlayerScore <= otherPlayerGameInfo[0].firstPlayerScore)
            {
                mainGameBaseInfo.firstPlayerNum = 1;
                tempMaxScore = otherPlayerGameInfo[0].firstPlayerScore;
            }
        }


        //상대1 그리고 2 비교
        //상대2 그리고 3 비교
        if(tempJudgeType == MainGameBaseInfo.judgeType.none)
        {
            for (int i = 0; i < 2; i++)
            {
                if (tempMaxScore < otherPlayerGameInfo[i + 1].firstPlayerScore)
                {
                    mainGameBaseInfo.firstPlayerNum = i + 2;
                    tempMaxScore = otherPlayerGameInfo[i + 1].firstPlayerScore;
                }
                else if (tempMaxScore == otherPlayerGameInfo[i + 1].firstPlayerScore)
                {
                    mainGameBaseInfo.firstPlayerNum = -1;
                }
            }
        }
        else if(tempJudgeType == MainGameBaseInfo.judgeType.benefit)
        {
            for (int i = 0; i < 2; i++)
            {
                if (tempMaxScore <= otherPlayerGameInfo[i + 1].firstPlayerScore)
                {
                    mainGameBaseInfo.firstPlayerNum = i + 2;
                    tempMaxScore = otherPlayerGameInfo[i + 1].firstPlayerScore;
                }
            }
        }

    }

    //선택 후 
    void DumpInitialStones()
    {
        //일반적인 경우 선이 결정되었을 경우
        //뽑은 돌을 모두 버리고 게임을 시작한다.
        //선의 돌은 7,8번에 위치하면서 가져올 수 없게된다.
        //유저들이 최초 선택한 돌들을 Dumped로 이동시킨다.
        //2번 실패하면 무효처리한다.

        //선이 결정된 경우
        if (mainGameBaseInfo.firstPlayerNum > -1)
        {
            //선을 중심으로 둥글게 버린다.
            //일단 당사자는 파괴한다.
            DestroySelectedStone(mainGameBaseInfo.firstPlayerNum);

            //DumpSelectedStone
            for (int i = mainGameBaseInfo.firstPlayerNum+1; i < mainGameBaseInfo.firstPlayerNum + 4; i++)
            {
                DumpSelectedStone(i % 4);
            }


            //실제 gameObject 버리기 처리
            #region dump gameObject
            //모두 가져다 버리기
            if (mainGameBaseInfo.firstPlayerNum != 0)
            {
                myCommonObjects.StoneOnHandBriefsPair[0].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[(mainGameBaseInfo.firstPlayerNum * 2) - 2].transform, 0);

                myCommonObjects.StoneOnHandBriefsPair[1].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[(mainGameBaseInfo.firstPlayerNum * 2) - 1].transform, 0);

            }

            for (int i=0; i<3; i++)
            {
                for(int j=0; j<2; j++)
                {
                    //다른 것들만 폐기처분
                    if(mainGameBaseInfo.firstPlayerNum -1 != i)
                    {
                        otherCommonObjects[i].StoneOnHandBriefsPair[j].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[(6 + j + ((mainGameBaseInfo.firstPlayerNum - 1 - i) * 2)) % 8].transform, 0);
                    }
                }
            }
            #endregion

            //본격적 게임 단계로 이동
            mainGameBaseInfo.nextPlayingPlayer = mainGameBaseInfo.firstPlayerNum;
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstEnd;
        }
        //선이 결정되지 않았다면 스텝을 다시 이동한다.
        else
        {
            //그냥 다 파괴해버린다.
            for (int i = 0; i < 4; i++)
            {
                DestroySelectedStone(i);
            }

            //인자 초기화
            mainGameBaseInfo.initialSelectionFinishedPass = 0;

            //실패한 적이 있으면 게임을 무효로 한다.
            if (mainGameBaseInfo.firstPlayerFailedBefore)
            {
                mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralRoundEnd;
            }
            else
            {
                mainGameBaseInfo.firstPlayerFailedBefore = true;
                mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSelectionWaiting;
            }

        }
    }

    //돌을 버린다.
    //0번은 본인, 1,2,3 순서대로 시계방향 다른 유저이다.

    public void DumpSelectedStone(int userNum)
    {
        if(mainGameBaseInfo.DumpedStonesList.Count >= 6)
        {
            for(int i=0; i<2; i++)
            {

                StoneInfo tempStone = mainGameBaseInfo.DumpedStonesList[0];

                mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

                mainGameBaseInfo.DumpedStonesList.RemoveAt(0);

            }
        }

        //0번 버리기
        if (userNum == 0)
        {
            StoneInfo tempStone = myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[0]];

            mainGameBaseInfo.DumpedStonesList.Add(tempStone);

            myPlayerGameInfo.HoldingStone.RemoveAt(myPlayerGameInfo.SelectStonesRef[0]);

            //추가적으로 reference도 감소시킨다.
            myPlayerGameInfo.HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }
        else
        {
            StoneInfo tempStone = otherPlayerGameInfo[userNum - 1].HoldingStone[otherPlayerGameInfo[userNum - 1].SelectStonesRef[0]];

            mainGameBaseInfo.DumpedStonesList.Add(tempStone);

            otherPlayerGameInfo[userNum - 1].HoldingStone.RemoveAt(otherPlayerGameInfo[userNum - 1].SelectStonesRef[0]);

            //추가적으로 reference도 감소시킨다.
            otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }

        //보정 후 1번 버리기
        int tempVal = 0;
        if (userNum == 0)
        {
            if (myPlayerGameInfo.SelectStonesRef[0] < myPlayerGameInfo.SelectStonesRef[1])
            {
                tempVal = -1;
            }
        }
        else
        {
            if (otherPlayerGameInfo[userNum - 1].SelectStonesRef[0] < otherPlayerGameInfo[userNum - 1].SelectStonesRef[1])
            {
                tempVal = -1;
            }
        }

        //1번 버리기
        if (userNum == 0)
        {
            StoneInfo tempStone = myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[1] + tempVal];

            mainGameBaseInfo.DumpedStonesList.Add(tempStone);

            myPlayerGameInfo.HoldingStone.RemoveAt(myPlayerGameInfo.SelectStonesRef[1] + tempVal);

            //추가적으로 reference도 감소시킨다.
            myPlayerGameInfo.HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }
        else
        {
            StoneInfo tempStone = otherPlayerGameInfo[userNum - 1].HoldingStone[otherPlayerGameInfo[userNum - 1].SelectStonesRef[1] + tempVal];

            mainGameBaseInfo.DumpedStonesList.Add(tempStone);

            otherPlayerGameInfo[userNum - 1].HoldingStone.RemoveAt(otherPlayerGameInfo[userNum - 1].SelectStonesRef[1] + tempVal);

            //추가적으로 reference도 감소시킨다.
            otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }


        if (userNum == 0)
        {
            myPlayerGameInfo.SelectStonesRef.Clear();
        }
        else
        {
            otherPlayerGameInfo[userNum - 1].SelectStonesRef.Clear();
        }

    }

    public void DestroySelectedStone(int userNum)
    {
        //0번 파괴
        if (userNum == 0)
        {
            StoneInfo tempStone = myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[0]];

            mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

            myPlayerGameInfo.HoldingStone.RemoveAt(myPlayerGameInfo.SelectStonesRef[0]);

            //추가적으로 reference도 감소시킨다.
            myPlayerGameInfo.HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }
        else
        {
            StoneInfo tempStone = otherPlayerGameInfo[userNum - 1].HoldingStone[otherPlayerGameInfo[userNum - 1].SelectStonesRef[0]];

            mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

            otherPlayerGameInfo[userNum - 1].HoldingStone.RemoveAt(otherPlayerGameInfo[userNum - 1].SelectStonesRef[0]);

            //추가적으로 reference도 감소시킨다.
            otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }

        //보정 후 1번 버리기
        int tempVal = 0;
        if (userNum == 0)
        {
            if (myPlayerGameInfo.SelectStonesRef[0] < myPlayerGameInfo.SelectStonesRef[1])
            {
                tempVal = -1;
            }
        }
        else
        {
            if (otherPlayerGameInfo[userNum - 1].SelectStonesRef[0] < otherPlayerGameInfo[userNum - 1].SelectStonesRef[1])
            {
                tempVal = -1;
            }
        }

        //1번 버리기
        if (userNum == 0)
        {
            StoneInfo tempStone = myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[1] + tempVal];

            mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

            myPlayerGameInfo.HoldingStone.RemoveAt(myPlayerGameInfo.SelectStonesRef[1] + tempVal);

            //추가적으로 reference도 감소시킨다.
            myPlayerGameInfo.HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }
        else
        {
            StoneInfo tempStone = otherPlayerGameInfo[userNum - 1].HoldingStone[otherPlayerGameInfo[userNum - 1].SelectStonesRef[1] + tempVal];

            mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

            otherPlayerGameInfo[userNum - 1].HoldingStone.RemoveAt(otherPlayerGameInfo[userNum - 1].SelectStonesRef[1] + tempVal);

            //추가적으로 reference도 감소시킨다.
            otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }


        if (userNum == 0)
        {
            myPlayerGameInfo.SelectStonesRef.Clear();
        }
        else
        {
            otherPlayerGameInfo[userNum - 1].SelectStonesRef.Clear();
        }

    }

    //MainGame의 시작
    void StartMainGame()
    {
        //일정 시간 후 버려진 돌에 대해서 교대한다.
        Invoke("ChangeSelectedToDump",2.6f);

        mainGameBaseInfo.latestTurnUser = -1;

        //유저 생태를 점검한다.
        //네트워크 환경을 점검하고 문제가 없다고 본격적으로 진행한다.
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Neutral;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralTurnCheck;
    }

    //이미지를 대체한다.
    void ChangeSelectedToDump()
    {

        if(mainGameBaseInfo.latestTurnUser == -1)
        {
            
            for (int j = 0; j < 2; j++)
            {
                myCommonObjects.StoneOnHandBriefsPair[j].gameObject.SetActive(false);
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    otherCommonObjects[i].StoneOnHandBriefsPair[j].gameObject.SetActive(false);
                }
            }


        }
        else
        {
            if(mainGameBaseInfo.latestTurnUser == 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    myCommonObjects.StoneOnHandBriefsPair[j].gameObject.SetActive(false);
                }
            }
            else
            {
                for (int j = 0; j < 2; j++)
                {
                    otherCommonObjects[mainGameBaseInfo.latestTurnUser - 1].StoneOnHandBriefsPair[j].gameObject.SetActive(false);
                }
            }
        }

        

        for(int i=0; i<6; i++)
        {
            externalGameObjects.dumpController.TargetGameObjectList[i].GetComponent<StoneOnHandBrief>().enabled = true;
            externalGameObjects.dumpController.TargetGameObjectList[i].GetComponent<StoneOnHandBrief>().ActiveShowObjects(-1);
            if(i%2 == 0)
            {
                externalGameObjects.dumpController.TargetGameObjectList[i].GetComponent<StoneOnHandBrief>().ChangeSprite((int)mainGameBaseInfo.DumpedStonesList[4 - i].stoneDetailType, mainGameBaseInfo.DumpedStonesList[4 - i].stoneNumber);
            }
            else
            {
                externalGameObjects.dumpController.TargetGameObjectList[i].GetComponent<StoneOnHandBrief>().ChangeSprite((int)mainGameBaseInfo.DumpedStonesList[6 - i].stoneDetailType, mainGameBaseInfo.DumpedStonesList[6 - i].stoneNumber);
            } 
        }

    }

    #endregion

    #region NeutralWorks
    //턴 시작
    void NeutralTurnChecking()
    {
        //턴을 기다릴 특수 경우
        if (mainGameBaseInfo.gameType == GameType.versusNormalGame)
        {
            //네트워크 대기 상태로 넘긴다.
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralUserAllCheck;
        }
        //아닐 경우에는 순순히 턴을 넘긴다.
        else
        {
            PlayingTurnChange();
        }
    }

    void NeutralUserAllChecking()
    {
        //여기는 대전의 경우에만 처리한다.
        //네트워크 재확인
        //위에서 결정된 순서에 맞춰서 턴을 정해준다.

        //만약 사람이 없다면 일부러 딜레이를 늘려서 기다려준다.

        //일정 시간동안 대기를 만든다.
        //mainGameBaseInfo.neutralNetworkCheckingList[0] = true;
        //PlayingTurnChange();

    }

    //여러가지 이유로 error 상태일 경우에 위치한 공간
    void NeutralErrorChecking()
    {



    }


    //특정 플레이어로 턴을 넘긴다.
    void PlayingTurnChange()
    {
        //나의 턴으로 넘기고 수행시작
        if (mainGameBaseInfo.nextPlayingPlayer == 0)
        {
            //플레이어 번호 지정
            mainGameBaseInfo.nowPlayingPlayer = 0;

            //순차적 코루틴 실행 
            StartCoroutine(MyReadyShow());
        }
        //상대 턴으로 넘기고 수행시작
        else
        {
            //Debug.Log("보류");
            //플레이어 번호 지정
            mainGameBaseInfo.nowPlayingPlayer = mainGameBaseInfo.nextPlayingPlayer;

            //순차적 코루틴 실행
            StartCoroutine(OtherReadyShow());
        }
    }

    #endregion

    #region MyMainGameFunctions

    IEnumerator MyReadyShow()
    {
        //상태 이동
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_My;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_MyReady;

        yield return new WaitForSeconds(1.0f);

        //혹시라도 이미 배열되어 있다면 재배치 하지 않는다.
        if (!CheckMyArranged())
        {
            //현재 상태에서 아무런 변화 없이 3,4만 남기고 정렬하는 연출을 한다.
            //잠깐 사라졌다가 다시 재배열 해서 나타난다
            externalGameObjects.gameObjectSpawner.StoneDisappearAllFunction();

            yield return new WaitForSeconds(0.4f);

            //내부적인 이미지 및 배치 전환
            #region inner change

            //일단 전부 비활성화
            for (int i = 0; i < 10; i++)
            {
                externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].gameObject.SetActive(false);
            }

            //우선 재배열은 아무런 변경점이 없다.
            //myPlayerGameInfo.RearrangeHoldingStones();

            //내부 값 변경
            for (int i = 0; i < myPlayerGameInfo.HoldingStone.Count; i++)
            {
                //3이상은 
                if (i >= 3)
                {
                    if (i + 2 < 10)
                    {
                        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i + 2].gameObject.SetActive(true);

                        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i + 2].ResetStone();
                        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i + 2].SetAllActive(true);

                        //해당 오브젝트에 접근해 값 변경
                        externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i + 2].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
                    }
                }
                else
                {
                    externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].gameObject.SetActive(true);

                    externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].ResetStone();
                    externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].SetAllActive(true);

                    externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
                }

                //externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].gameObject.SetActive(false);

                //3,4번은 비워둔다.
                //externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[i]].gameObject.SetActive(false);


                //해당 오브젝트에 접근해 값 변경
                //externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
            }
            #endregion

            yield return null;
            //다시 나타난다.
            externalGameObjects.gameObjectSpawner.StoneAppearAllFunction();
        }

        //내 턴 크리스탈 시작
        //소리와 이펙트 발생
        myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;
        externalGameObjects.audioManagerInGame.ChangeSFXAndPlay(AudioManagerInGame.SFXname.AlarmPositive);

        yield return null;

        //시스템 상으로 돌을 뽑는다.
        //뽑은 돌은 연출 후 정렬한다.
        MyPullStones();

        #region 3,4 setting and appear
        yield return new WaitForSeconds(0.1f);

        //이펙트도 발동시켜놓는다.
        externalGameObjects.MyTurnEffect.SetActive(false);
        yield return null;
        externalGameObjects.MyTurnEffect.SetActive(true);


        yield return new WaitForSeconds(0.3f);

        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[3].gameObject.SetActive(true);
        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[3].SetAllActive(true);
        externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[3].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[myPlayerGameInfo.HoldingStone.Count - 2].stoneDetailType, myPlayerGameInfo.HoldingStone[myPlayerGameInfo.HoldingStone.Count - 2].stoneNumber);
        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[3].ResetStone();
        //externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(3);
        externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(3, 0);

        yield return new WaitForSeconds(0.14f);

        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[4].gameObject.SetActive(true);
        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[4].SetAllActive(true);
        externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[4].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[myPlayerGameInfo.HoldingStone.Count - 1].stoneDetailType, myPlayerGameInfo.HoldingStone[myPlayerGameInfo.HoldingStone.Count - 1].stoneNumber);
        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[4].ResetStone();
        //externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(4);
        externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(4, 0);

        #endregion


        yield return new WaitForSeconds(0.8f);

        //잠깐 사라졌다가 다시 재배열 해서 나타난다
        #region new arrange
        externalGameObjects.gameObjectSpawner.StoneDisappearAllFunction();
        yield return new WaitForSeconds(0.3f);
        //사라지는 연출

        //재배열
        myPlayerGameInfo.RearrangeHoldingStones();

        yield return null;
        
        //Completion의 Reference를 연산한다.------------------------
        //또한 Integration Recover에 대한 index를 연산해준다.
        myPlayerGameInfo.RenewCompletionInfoAll();
        //----------------------------------------------------------

        yield return null;

        //내부 값 변경
        for (int i = 0; i < myPlayerGameInfo.HoldingStone.Count; i++)
        {
            //해당 오브젝트에 접근해 값 변경
            externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
        }
        yield return null;


        //다시 나타난다.
        externalGameObjects.gameObjectSpawner.StoneAppearAllFunction();
        #endregion

        yield return null;

        //모든 돌 컨트롤 가능
        externalGameObjects.gameObjectSpawner.StoneMovableAllFunction(true);

        //
        yield return null;
        CalculateRecoverDumpIndex();

        //현재 가능한 최대의 돌의 개수를 판정한다.
        //각 돌별로 몇개까지 가능한지 판정
        yield return new WaitForSeconds(0.2f);
        AnalyzeMyStonesAndActiveUI(true);

        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_MyWait;

        externalGameObjects.stoneClickController.thisPlayingNow = true;

        yield break;

    }

    //배열 유무를 확인한다.
    bool CheckMyArranged()
    {
        bool tempBool = true;


        if(myPlayerGameInfo.HoldingStone.Count <= 3)
        {
            for(int i=0; i< myPlayerGameInfo.HoldingStone.Count; i++)
            {
                if (! externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].gameObject.activeSelf)
                {
                    return false;
                }
            }
        }
        //혹시나 개수가 맞지 않는 경우
        else if (myPlayerGameInfo.HoldingStone.Count >= 9)
        {
            return false;
        }
        //4개 이상
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (!externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].gameObject.activeSelf)
                {
                    return false;
                }
            }

            if (externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[3].gameObject.activeSelf)
            {
                return false;
            }

            if (externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[4].gameObject.activeSelf)
            {
                return false;
            }

            for(int i = 5; i < 2 + myPlayerGameInfo.HoldingStone.Count; i++)
            {
                if (!externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].gameObject.activeSelf)
                {
                    return false;
                }
            }
        }


        return tempBool;
    }

    //덱에서 2장 드로우 한다.
    void MyPullStones()
    {

        PullOneStone(0, false);

        PullOneStone(0, false);

        //myPlayerGameInfo.RenewCompletionInfoAll();

    }


    //현재 자신의 패를 분석한다. 최종적으로 UI를 활성화한다.
    //이전에는 일일히 버튼이 있었지만 이제는 컨트롤


    //이를 통해 wait에서 사용할 UI를 활성화시킨다.
    //합성 가능 UI
    //복원 가능 UI
    //신비석 가능 UI
    //종료 가능 UI
    void AnalyzeMyStonesAndActiveUI(bool tempActiveAll)
    {

        if (tempActiveAll)
        {
            //기본 활성화
            myPlayerUI.UIAll.SetActive(true);

            //현재 상태를 분석해서 부분적으로 활성화 한다.

            //현재 completion중 0이상인 것이 있을 경우에 활성화
            //이제 새롭게 index에 해당하는 리스트를 만들어서 처리
            if(myPlayerGameInfo.RecoverAvailIndexList.Count > 0)
            {
                if(myPlayerGameInfo.RecoverDumpIndexRefList.Count > 0)
                {
                    myPlayerUI.recoverButton.SetActive(true);
                }
                else
                {
                    myPlayerUI.recoverButton.SetActive(false);
                }
            }
            else
            {
                myPlayerUI.recoverButton.SetActive(false);
            }
            
            //현재 completion중 -1일 경우에 활성화
            //이제 새롭게 index에 해당하는 리스트를 만들어서 처리
            if(myPlayerGameInfo.IntegrationIndexList.Count > 0)
            {
                myPlayerUI.integrationButton.SetActive(true);
            }
            else
            {
                myPlayerUI.integrationButton.SetActive(false);
            }

            //솔직히 이건 안 활성화 되기가 어렵다.
            //기본적으로 2개를 뽑으니까 무조건 활성화라고 보면 됨
            if(myPlayerGameInfo.HoldingStone.Count >= 2)
            {
                myPlayerUI.dumpButton.SetActive(true);
            }
            else
            {
                myPlayerUI.dumpButton.SetActive(false);
            }
            

            //신비석 활용버튼
            //myPlayerUI.arcaneButton.SetActive(true);
        }
        else
        { 
            myPlayerUI.UIAll.SetActive(false);

            myPlayerUI.integrationButton.SetActive(false);
            myPlayerUI.recoverButton.SetActive(false);
            myPlayerUI.dumpButton.SetActive(false);

        }
    }

    //Index와 dump를 기반으로 dump의 index를 연산한다.
    //RecoverAvailIndexList * 10 + DumpedStonesList 으로 구성한다.
    void CalculateRecoverDumpIndex()
    {
        //이것이 안정적이다.
        for(int i=0; i< mainGameBaseInfo.DumpedStonesList.Count; i++)
        {
            //dump stone에 대한 분석
            int tempNum1 = (int)(mainGameBaseInfo.DumpedStonesList[i].stoneType);

            int tempNum2 = 0;

            if(tempNum1 == 0)
            {
                tempNum2 = 0;
            }
            else
            {
                tempNum2 = (int)(mainGameBaseInfo.DumpedStonesList[i].stoneDetailType);

                if(tempNum2 >= 10)
                {
                    tempNum2 -= 10;
                }
                else if(tempNum2 >= 7)
                {
                    tempNum2 -= 7;
                }
                else if(tempNum2 >= 2)
                {
                    tempNum2 -= 2;
                }
            }
  

            int tempRes = tempNum1 * 100 + tempNum2 * 10;

            //버린 돌의 번호
            int tempDumpNum = mainGameBaseInfo.DumpedStonesList[i].stoneNumber;

            //일단 기본적인 타입이 맞는지 확인한다.
            for (int j=0; j< myPlayerGameInfo.RecoverAvailIndexList.Count; j++)
            {
                //해당 범위 내의 값이면(종류가 같으면)
                //tempType은 사실상 번호별 조합의 경우이다.
                int tempType = myPlayerGameInfo.RecoverAvailIndexList[j] - tempRes;

                if (tempType >= 0 && tempType < 10)
                {

                    if (tempNum1 == 0)
                    {
                        myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                    }
                    else if(tempNum1 == 1)
                    {
                        if(tempType < 5)
                        {
                            //동일한 경우
                            if(tempDumpNum == tempType)
                            {
                                myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                            }
                        }
                        else if(tempType < 8)
                        {
                            //계단 경우
                            if (tempDumpNum == myPlayerGameInfo.CompletionTypeElementsList[tempNum2].completionStairList[tempType-5])
                            {
                                myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                            }
                            //이미 완성이 된 경우
                            else if(myPlayerGameInfo.CompletionTypeElementsList[tempNum2].completionStairList[tempType - 5] == -1)
                            {
                                if(tempDumpNum < (tempType - 5 + 3) && (tempType - 5) <= tempDumpNum)
                                {
                                    myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                                }
                            }
                        }
                        //계단 전체
                        else if(tempType == 8)
                        {
                            //계단 경우
                            if (tempDumpNum == myPlayerGameInfo.CompletionTypeElementsList[tempNum2].completionStairLarge || myPlayerGameInfo.CompletionTypeElementsList[tempNum2].completionStairLarge == -1)
                            {
                                myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                            }
                        }
                    }
                    else if(tempNum1 == 2 || tempNum1 == 3)
                    {
                        if(tempType < 3)
                        {
                            //동일한 경우
                            if(tempDumpNum == tempType)
                            {
                                myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                            }
                        }
                        else if(tempType == 3)
                        {
                            //계단 경우
                            if(tempNum1 == 2)
                            {
                                if (tempDumpNum == myPlayerGameInfo.CompletionTypeOpticList[tempNum2].completionStair)
                                {
                                    myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                                }
                                else if (myPlayerGameInfo.CompletionTypeOpticList[tempNum2].completionStair == -1)
                                {
                                    myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                                }
                            }
                            else if(tempNum1 == 3)
                            {
                                if (tempDumpNum == myPlayerGameInfo.CompletionTypeMineralList[tempNum2].completionStair)
                                {
                                    myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                                }
                                else if (myPlayerGameInfo.CompletionTypeMineralList[tempNum2].completionStair == -1)
                                {
                                    myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                                }
                            }
                        }
                        else if(tempType < 7)
                        {
                            //동일한 경우
                            if (tempDumpNum == tempType-4)
                            {
                                myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                            }
                        }
                    }
                    
                }
            }

 
        }

    }


    //자신 고민하면서 대기 페이즈
    //Analye를 통해 활성화된 UI를 통해 작업한다.
    void MyWaitingForSelectUI()
    {

        switch (mainGameBaseInfo.nowMyControlType)
        {
            case MyControlType.dump:

                if ((externalGameObjects.SelectionControllerList[0].nowAppliedStoneRefNum != -1) && (externalGameObjects.SelectionControllerList[1].nowAppliedStoneRefNum != -1))
                {
                    externalGameObjects.dumpExecutionButton.SetActive(true);
                }
                //그 외에는 비활성화
                else
                {
                    externalGameObjects.dumpExecutionButton.SetActive(false);
                }

                break;

            //돌 선택 단계
            case MyControlType.recoverDumpSelect:

                break;

            case MyControlType.recover:

                //현재 plane에 적용된 돌에 대한 연산
                if (CalculateIntegrationSmall(1) >= 0)
                {
                    externalGameObjects.integrationExecutionButton.SetActive(true);
                    externalGameObjects.SelectionSmallEffect.SetActive(true);

                    //밝게 빛남
                    for(int i=0; i<3; i++)
                    {
                        externalGameObjects.SelectionSmallControllerList[i].nowPlaneStatus = PlaneAltarController.PlaneStatusEnum.selectionMax;
                    }
                }
                else
                {
                    externalGameObjects.integrationExecutionButton.SetActive(false);
                    externalGameObjects.SelectionSmallEffect.SetActive(false);
                }

                break;

            case MyControlType.integration:

                //현재 plane에 적용된 돌에 대한 연산
                if (CalculateIntegrationSmall(0) >= 0)
                {
                    externalGameObjects.integrationExecutionButton.SetActive(true);
                    externalGameObjects.SelectionSmallEffect.SetActive(true);

                    //밝게 빛남
                    for (int i = 0; i < 3; i++)
                    {
                        externalGameObjects.SelectionSmallControllerList[i].nowPlaneStatus = PlaneAltarController.PlaneStatusEnum.selectionMax;
                    }
                }
                else
                {
                    externalGameObjects.integrationExecutionButton.SetActive(false);
                    externalGameObjects.SelectionSmallEffect.SetActive(false);
                }

                break;

            case MyControlType.none:

                break;

        }

    }


    //합성 index
    private int CalculateIntegrationSmall(int calType)
    {

        int tempNum = -4;

        if(calType == 1)
        {
            for (int i = 0; i < 2; i++)
            {

                if (externalGameObjects.SelectionSmallControllerList[i].nowAppliedStoneRefNum != -1)
                {
                    tempNum++;
                }
                else
                {
                    return tempNum;
                }

            }

            tempNum = -1;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {

                if (externalGameObjects.SelectionSmallControllerList[i].nowAppliedStoneRefNum != -1)
                {
                    tempNum++;
                }
                else
                {
                    return tempNum;
                }

            }
        }

        //정상일 경우 종류에 대해 연산
        if(tempNum >= -1)
        {
            //여기서 완성된 돌의 종류도 반환한다.

            //1번의 경우는 dump를 조사한다.
            int tempStoneType;

            if (calType == 1)
            {
                if(myPlayerGameInfoControl.tempSelectedDumpNum % 2 == 0)
                {
                    tempStoneType = (int)mainGameBaseInfo.DumpedStonesList[4 - myPlayerGameInfoControl.tempSelectedDumpNum].stoneDetailType;
                }
                else
                {
                    tempStoneType = (int)mainGameBaseInfo.DumpedStonesList[6 - myPlayerGameInfoControl.tempSelectedDumpNum].stoneDetailType;
                }

                //tempStoneType = (int)mainGameBaseInfo.DumpedStonesList[myPlayerGameInfoControl.tempSelectedDumpNum].stoneDetailType;
                //Debug.Log("stone2ref : " + myPlayerGameInfoControl.tempSelectedDumpNum);
                //Debug.Log("stone2 : "+tempStoneType);
            }
            else
            {
                tempStoneType = (int)myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[2].nowAppliedStoneRefNum].stoneDetailType;
            }

            //Debug.Log("stone1 : "+(int)myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[1].nowAppliedStoneRefNum].stoneDetailType);
            if (tempStoneType == (int)myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[1].nowAppliedStoneRefNum].stoneDetailType)
            {

                //Debug.Log("stone0 : " + (int)myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[0].nowAppliedStoneRefNum].stoneDetailType);
                if (tempStoneType == (int)myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[0].nowAppliedStoneRefNum].stoneDetailType)
                {

                    //이제 same stair를 구분한다.
                    if (myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[0].nowAppliedStoneRefNum].stoneDetailType == StoneDetailType.arcane)
                    {
                        //신비석 반영
                        return 0;
                    }
                    else
                    {
                        int tempStoneNum;

                        if (calType == 1)
                        {
                            if (myPlayerGameInfoControl.tempSelectedDumpNum % 2 == 0)
                            {
                                tempStoneNum = (int)mainGameBaseInfo.DumpedStonesList[4 - myPlayerGameInfoControl.tempSelectedDumpNum].stoneNumber;
                            }
                            else
                            {
                                tempStoneNum = (int)mainGameBaseInfo.DumpedStonesList[6 - myPlayerGameInfoControl.tempSelectedDumpNum].stoneNumber;
                            }
                            //tempStoneNum = (int)mainGameBaseInfo.DumpedStonesList[myPlayerGameInfoControl.tempSelectedDumpNum].stoneNumber;
                            //Debug.Log("stone2 num : " + tempStoneNum);
                        }
                        else
                        {
                            tempStoneNum = (int)myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[2].nowAppliedStoneRefNum].stoneNumber;
                        }


                        //same의 경우
                        //Debug.Log("stone1 num : " + myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[1].nowAppliedStoneRefNum].stoneNumber);
                        if (tempStoneNum == myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[1].nowAppliedStoneRefNum].stoneNumber)
                        {

                            //Debug.Log("stone0 num : " + myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[0].nowAppliedStoneRefNum].stoneNumber);
                            if (tempStoneNum == myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[0].nowAppliedStoneRefNum].stoneNumber)
                            {

                                tempNum = (tempStoneType << 5);
                                tempNum += (tempStoneNum + 1);

                                //Debug.Log(tempNum);

                                return tempNum;
                            }
                            else
                            {
                                return -1;
                            }
                        }

                        //다른것끼리 같으면 반환
                        if(myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[1].nowAppliedStoneRefNum].stoneNumber == myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[0].nowAppliedStoneRefNum].stoneNumber)
                        {
                            return -1;
                        }
                        //stair의 경우
                        //tempStoneNum

                        int tempSum = 0;

                        if(calType == 1)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                tempSum += myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[i].nowAppliedStoneRefNum].stoneNumber;
                            }

                            tempSum += tempStoneNum;
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                tempSum += myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[i].nowAppliedStoneRefNum].stoneNumber;
                            }
                        }
 
                        

                        int tempDiv = 0;

                        if(tempSum % 3 != 0)
                        {
                            return -1;
                        }
                        //3의 배수의 합이 있는 경우
                        else
                        {
                            tempDiv = tempSum / 3;
                         
                            for(int i=0; i<3; i++)
                            {

                                int tempCal;

                                if (calType == 1)
                                {
                                    if(i == 2)
                                    {
                                        tempCal = tempStoneNum - tempDiv;
                                    }
                                    else
                                    {
                                        tempCal = myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[i].nowAppliedStoneRefNum].stoneNumber - tempDiv;
                                    }
                                }
                                else
                                {
                                    tempCal = myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[i].nowAppliedStoneRefNum].stoneNumber - tempDiv;
                                }

                                if (tempCal == -1 || tempCal == 0 || tempCal == 1)
                                {

                                }
                                else
                                {
                                    return -1;
                                }
                            }

                            tempNum = (tempStoneType << 5);
                            tempNum += (tempDiv << 3);

                            //Debug.Log(tempNum);
                            return tempNum;
                        }

                    }


                }
            }


        }


        return tempNum;

    }

    void MyEndTurn()
    {
        //일정 시간 후 버려진 돌에 대해서 교대한다.
        Invoke("ChangeSelectedToDump", 2.8f);

        mainGameBaseInfo.latestTurnUser = mainGameBaseInfo.nowPlayingPlayer;

        //다음 플레이 유저 지정
        mainGameBaseInfo.nextPlayingPlayer = 1;

        //클릭 불가능하게 바꾸기
        externalGameObjects.stoneClickController.thisPlayingNow = false;

        //유저 생태를 점검한다.
        //네트워크 환경을 점검하고 문제가 없다고 본격적으로 진행한다.
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Neutral;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralTurnCheck;
    }

    #endregion

    #region OtherMainGameFunctions

    IEnumerator OtherReadyShow()
    {
        //상태 이동
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Other;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_OtherReady;

        yield return new WaitForSeconds(1.0f);

        //시스템 상으로 돌을 뽑는다.
        //뽑은 돌은 연출 후 정렬한다.
        //MyPullStones();
        OtherPullStones(mainGameBaseInfo.nowPlayingPlayer);


        //크리스탈 보여짐
        CrystalAllControl(1, -1);

        otherCommonObjects[mainGameBaseInfo.nowPlayingPlayer - 1].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;

        //최초 돌 뽑는 연출
        //0번 나타나기
        externalGameObjects.gameObjectSpawner.OtherBoardAreaTransformHandList[mainGameBaseInfo.nowPlayingPlayer - 1].OtherBoardAreaInnerTransformList[0].gameObject.SetActive(true);
        //externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[0].SetAllActive(true);
        externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(0, mainGameBaseInfo.nowPlayingPlayer);

        yield return new WaitForSeconds(0.2f);

        //1번 나타나기
        externalGameObjects.gameObjectSpawner.OtherBoardAreaTransformHandList[mainGameBaseInfo.nowPlayingPlayer - 1].OtherBoardAreaInnerTransformList[1].gameObject.SetActive(true);
        //externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[1].SetAllActive(true);
        externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(1, mainGameBaseInfo.nowPlayingPlayer);


        yield return new WaitForSeconds(0.3f);

        //뽑은 돌의 재배열
        otherPlayerGameInfo[mainGameBaseInfo.nowPlayingPlayer - 1].RearrangeHoldingStones();

        yield return null;

        //Completion의 Reference를 연산한다.------------------------
        //또한 Integration Recover에 대한 index를 연산해준다.
        otherPlayerGameInfo[mainGameBaseInfo.nowPlayingPlayer - 1].RenewCompletionInfoAll();
        //----------------------------------------------------------

        //yield return null;

        //CalculateRecoverDumpIndex();

        //현재 가능한 최대의 돌의 개수를 판정한다.
        //각 돌별로 몇개까지 가능한지 판정
        yield return new WaitForSeconds(0.2f);
        //AnalyzeMyStonesAndActiveUI(true);

        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_OtherWait;

        externalGameObjects.stoneClickController.thisPlayingNow = false;

        yield break;

    }

    //
    public void OtherPullStones(int userNum)
    {
        PullOneStone(userNum, false);

        PullOneStone(userNum, false);
    }

    public void OtherWaitingForSelect()
    {
        //특정 시간 이후 Dump를 한다.
        OtherDumpExecution(mainGameBaseInfo.nowPlayingPlayer - 1);

    }

    public void OtherDumpExecution(int userNum)
    {
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_OtherDump;

        StartCoroutine(OtherDumpExecutionCor(userNum));
    }

    IEnumerator OtherDumpExecutionCor(int userNum)
    {
        //우선 기본적인 select를 수행한다.
        OtherRandomSelect(userNum);

        yield return new WaitForSeconds(1.2f);


        //기본 밑 세팅
        for (int i = 1; i >= 0; i--)
        {

            externalGameObjects.gameObjectSpawner.OtherBoardAreaTransformHandList[userNum].OtherBoardAreaInnerTransformList[i].gameObject.SetActive(false);
            //새롭게 등장
            otherCommonObjects[userNum].StoneOnHandBriefsPair[i].gameObject.SetActive(true);

            //select의 0번 1번의 정보를 적용
            otherCommonObjects[userNum].StoneOnHandBriefsPair[i].ChangeSprite((int)otherPlayerGameInfo[userNum].HoldingStone[otherPlayerGameInfo[userNum].SelectStonesRef[i]].stoneDetailType, otherPlayerGameInfo[userNum].HoldingStone[otherPlayerGameInfo[userNum].SelectStonesRef[i]].stoneNumber);

            otherCommonObjects[userNum].StoneOnHandBriefsPair[i].ActiveShowObjects(1);

            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(0.6f);

        
        externalGameObjects.dumpController.StartCreate2AndDestroy2();

        //실질적인 버리기 애니메이션
        for (int i = 0; i < 2; i++)
        {
            //otherCommonObjects[userNum].StoneOnHandBriefsPair[i].gameObject.SetActive(false);
            otherCommonObjects[userNum].StoneOnHandBriefsPair[i].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[i].transform, 0);

        }

        yield return null;

        //정보 dump를 수행하고 select도 초기화한다.
        DumpSelectedStone(userNum + 1);
        
        yield return null;

        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_OtherEnd;

        yield break;
    }

    public void OtherRandomSelect(int userNum)
    {

        for (int i = 0; i < 2; i++)
        {

            otherPlayerGameInfo[userNum].SelectStonesRef.Add(i);

        }

    }

    public void OtherEndTurn()
    {
        //일정 시간 후 버려진 돌에 대해서 교대한다.
        Invoke("ChangeSelectedToDump", 2.8f);

        mainGameBaseInfo.latestTurnUser = mainGameBaseInfo.nowPlayingPlayer;

        //다음 플레이 유저 지정
        mainGameBaseInfo.nextPlayingPlayer = mainGameBaseInfo.nowPlayingPlayer + 1;
        if (mainGameBaseInfo.nextPlayingPlayer >= 4)
        {
            mainGameBaseInfo.nextPlayingPlayer = mainGameBaseInfo.nextPlayingPlayer % 4;
        }

        //클릭 불가능하게 바꾸기
        externalGameObjects.stoneClickController.thisPlayingNow = false;

        //유저 생태를 점검한다.
        //네트워크 환경을 점검하고 문제가 없다고 본격적으로 진행한다.
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Neutral;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralTurnCheck;
    }

    #endregion

    #region EngameFunctions

    #endregion


}