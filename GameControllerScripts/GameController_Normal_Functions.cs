using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameController_Normal : MonoBehaviour
{

    #region BaseFunctions
    public void InitailizeGameSettings()
    {
        //���� �ʱ�ȭ
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.GameEntrance;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.GameEntrance_Ready;

        //���� �Ϲ� ���� �ʱ�ȭ
        mainGameBaseInfo.firstPlayerNum = -2;
        mainGameBaseInfo.firstPlayerFailedBefore = false;


        mainGameBaseInfo.playedTurnThisRound = 0;
        mainGameBaseInfo.nowPlayingPlayer = -1;
        mainGameBaseInfo.nextPlayingPlayer = -1;

        mainGameBaseInfo.nowRounds = 0;


        //�帧���� ����
        mainGameBaseInfo.initialSelectionFinishedPass = 0;

        //��� �ʱ�ȭ
        mainGameBaseInfo.initialSelectionFinishedList.Clear();

        mainGameBaseInfo.initialSelectionFinishedList = new List<bool>() { false, false, false, false };

        //���� List ���� Ŭ����
        mainGameBaseInfo.PiledStonesForStoryList.Clear();
        mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Clear();
        mainGameBaseInfo.DumpedStonesList.Clear();
        mainGameBaseInfo.DestroyedStonesList.Clear();

        //��Ʈ��ũ ���� üũ
        mainGameBaseInfo.neutralNetworkCheckingList = new List<bool>() { false, false, false, false };

        //���� ���� ���� �ʱ�ȭ
        myPlayerGameInfo.ClearPlayerGameInfo();

        for (int i = 0; i < 3; i++)
        {
            otherPlayerGameInfo[i].ClearPlayerGameInfo();
        }

        //���� ��Ʈ�ѿ� ���� �ʱ�ȭ
        myPlayerGameInfoControl.InitializePlayerGameInfoControl();


        //material ���� �ʱ�ȭ
        //externalGameObjects.initailSelectMaterial = 
    }

    //���� ���� �� ���ο� ����
    //���ο� ���忡 ���� ����
    public void InitializeRoundSettings()
    {
        //���� �ʱ�ȭ
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Initialize;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSetting;


        //���� �Ϲ� ���� �ʱ�ȭ
        mainGameBaseInfo.firstPlayerNum = -2;
        mainGameBaseInfo.firstPlayerFailedBefore = false;


        mainGameBaseInfo.playedTurnThisRound = 0;
        mainGameBaseInfo.nowPlayingPlayer = -1;
        mainGameBaseInfo.nextPlayingPlayer = -1;

        //�翬�� round�� 1�� �߰��ȴ�.
        //mainGameBaseInfo.nowRounds = 0;


        //�帧���� ����
        mainGameBaseInfo.initialSelectionFinishedPass = 0;

        //��� �ʱ�ȭ
        mainGameBaseInfo.initialSelectionFinishedList.Clear();

        mainGameBaseInfo.initialSelectionFinishedList = new List<bool>() { false, false, false, false };

        //���� List ���� Ŭ����
        mainGameBaseInfo.PiledStonesForStoryList.Clear();
        mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Clear();
        mainGameBaseInfo.DumpedStonesList.Clear();
        mainGameBaseInfo.DestroyedStonesList.Clear();

        //���� ���� ���� �ʱ�ȭ
        myPlayerGameInfo.ClearPlayerGameInfo();

        for (int i = 0; i < 3; i++)
        {
            otherPlayerGameInfo[i].ClearPlayerGameInfo();
        }

        //���� ��Ʈ�ѿ� ���� �ʱ�ȭ
        myPlayerGameInfoControl.InitializePlayerGameInfoControl();
    }

    #endregion

    #region EntranceFunctions

    //Etrance flow �Լ�
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



        //ũ����Ż Ȱ��ȭ
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



        //���� �ܰ�� �Ѿ��.
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

        //���� �ܰ�� �Ѿ��.
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

    //���� ���� �ڷ�ƾ ����
    #region InitailPull Functions
    void InitialFirstSetting()
    {
        //���� �� ����
        //�ź� ���õ� �����Ѵ�.
        GenerateNormalPilesForStory();

        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstPull;

        //�ڵ����� �̰����� �Ѿ��.
        StartCoroutine(AllUsersPullInitialStones());
    }

    //storyNormalGame �� Piles ����
    //blank�� �ϳ��ִ� 166 / �ź��� �ִ� 170��
    void GenerateNormalPilesForStory()
    {

        if (mainGameBaseInfo.PiledStonesForStoryList.Count != 0)
        {
            mainGameBaseInfo.PiledStonesForStoryList.Clear();
        }

        int tempCount = 0;

        //�ϴ� ������ �����Ѵ�.
        //�ź� Ȱ�� ����
        if (mainGameBaseInfo.isArcane)
        {
            tempCount = mainGameBaseInfo.baseStoneCount + mainGameBaseInfo.arcaneStoneCount;
        }
        //�ź� ��Ȱ�� ����(blank �ϳ��� �߰�)
        else
        {
            tempCount = mainGameBaseInfo.baseStoneCount + 1;
        }

            

        for (int i = 0; i < tempCount; i++)
        {
            mainGameBaseInfo.PiledStonesForStoryList.Add(i);
        }

        //isArcane�� ����
        if (mainGameBaseInfo.isArcane)
        {
            //�ź񼮿� �迭 ����
            //1���� blank ������ ��� �����. (0�� none)
            for (int i = 1; i < (int)ArcaneStoneType.blank; i++)
            {
                mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Add(i);
            }
        }
        //isArcane�� ���� ����
        else
        {
            mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Add((int)ArcaneStoneType.blank);
        }

    }

    IEnumerator AllUsersPullInitialStones()
    {
        //���� 10���� ���� �̴´�.
        //�� ������ holding stone�� �߰��Ѵ�.

        //�� �� 10�� ���� �̱�
        for (int i = 0; i < 10; i++)
        {
            PullOneStone(0, false);
        }
        yield return null;

        //�ٸ� ������ �̱�
        for (int targetNum = 1; targetNum < 4; targetNum++)
        {
            for (int i = 0; i < 10; i++)
            {
                PullOneStone(targetNum, false);
            }
            yield return null;
        }

        
        //���� ���� ������ �����ϴ� ����
        //������Ʈ�� ������ ���� �����ش�.
        for(int i=0; i<10; i++)
        {
            //�ش� ������Ʈ�� ������ �� ����
            externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
        }
        yield return null;

        //���� ����
        
        for (int i = 0; i < 10; i++)
        {
            //�������� �ٸ��� �ٸ� ������ ���嵵 ó���Ѵ�.
            //my other ���ÿ� ó��
            externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(i);
            yield return new WaitForSeconds(0.14f);
        }

        yield return new WaitForSeconds(0.4f);

        //��� ������ٰ� �ٽ� ��迭 �ؼ� ��Ÿ����
        externalGameObjects.gameObjectSpawner.StoneDisappearAllFunction();
        yield return new WaitForSeconds(0.3f);
        //������� ����

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

        //��迭
        myPlayerGameInfo.RearrangeHoldingStones();
        //���� �� ����
        for (int i = 0; i < 10; i++)
        {
            //�ش� ������Ʈ�� ������ �� ����
            externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
        }
        yield return null;


        //�ٽ� ��Ÿ����.
        externalGameObjects.gameObjectSpawner.StoneAppearAllFunction();
        //��Ÿ���� ����
        
        /*
        for (int i = 0; i < 10; i++)
        {
            externalGameObjects.gameObjectSpawner.StoneAppearFromDownFunction(i);
            yield return new WaitForSeconds(0.08f);
        }
        yield return new WaitForSeconds(0.2f);
        */

        //��Ÿ�� ������ ���� ���� �������� �Ѿ��.
        yield return new WaitForSeconds(0.2f);

        //��� ���� Ȱ��ȭ�Ѵ�.
        //StoneMovableAllFunction
        externalGameObjects.gameObjectSpawner.StoneMovableAllFunction(true);


        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSelectionSetting;

        yield break;
    }

    //���� �̴´�.
    //0���� ����, 1,2,3 ������� �ð���� �ٸ� �����̴�.
    public void PullOneStone(int userNum, bool isCompletionCheck)
    {
        //���� �߿��� �������� �����Ѵ�.
        int tempRefNum = Random.Range(0, mainGameBaseInfo.PiledStonesForStoryList.Count);

        //�����ؼ� �����Ѵ�.
        int tempTargetNum = mainGameBaseInfo.PiledStonesForStoryList[tempRefNum];
        mainGameBaseInfo.PiledStonesForStoryList.RemoveAt(tempRefNum);


        //�������� �޾ƿ� ��
        int tempArcaneRefNum = -1;
        int tempTargetArcaneNum = -1;

        double tempdouble = 0;

        int tempTypeNum = 0;

        //Debug.Log(tempTargetNum);


        StoneInfo tempStone;

        //�Ϲ� ����
        //165������ ���� �̾��� ���
        if (tempTargetNum < mainGameBaseInfo.baseStoneCount)
        {
            //mainGameBaseInfo.PiledArcaneStonesRefForStoryList;
            //��ȣ�� �ؼ��ؼ� �ִ´�.
            tempdouble = tempTargetNum / 15;
            //���� ����
            tempTypeNum = (int)System.Math.Truncate(tempdouble);

            //15���� ���� ������
            int remainder = tempTargetNum % 15;

            //Debug.Log(tempRefNum);
            //Debug.Log(tempTypeNum);


            if (userNum == 0)
            {
                tempStone = new StoneInfo((StoneDetailType)(tempTypeNum + 2), remainder, -1, myPlayerGameInfo.accumulatedCounts);
                //���۷����� �߰�
                myPlayerGameInfo.HoldingStoneReferenceCounts[tempTypeNum + 2]++;
            }
            else
            {
                tempStone = new StoneInfo((StoneDetailType)(tempTypeNum + 2), remainder, -1, otherPlayerGameInfo[userNum - 1].accumulatedCounts);
                //���۷����� �߰�
                otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[tempTypeNum + 2]++;
            }

        }
        //�ź� ����
        else
        {
            if (mainGameBaseInfo.isArcane)
            {
                //���� ���� ������ �ź��� �̴´�.
                tempArcaneRefNum = Random.Range(0, mainGameBaseInfo.PiledArcaneStonesRefForStoryList.Count);

                //�����ؼ� �����Ѵ�.
                tempTargetArcaneNum = mainGameBaseInfo.PiledArcaneStonesRefForStoryList[tempArcaneRefNum];
                mainGameBaseInfo.PiledArcaneStonesRefForStoryList.RemoveAt(tempArcaneRefNum);
            }
            //blankó��
            else
            {
                tempTargetArcaneNum = 8;
            }

            //�ź��� �������� �ٸ��� ó���Ѵ�.
            if (userNum == 0)
            {

                tempStone = new StoneInfo(StoneDetailType.arcane, tempTargetArcaneNum, myPlayerGameInfo.nowHoldingArcaneCount, myPlayerGameInfo.accumulatedCounts);

                //�ź� ���۷����� �߰�
                myPlayerGameInfo.HoldingStoneReferenceCounts[1]++;

                myPlayerGameInfo.nowHoldingArcaneCount++;

            }
            else
            {

                tempStone = new StoneInfo(StoneDetailType.arcane, tempTargetArcaneNum, otherPlayerGameInfo[userNum - 1].nowHoldingArcaneCount, otherPlayerGameInfo[userNum - 1].accumulatedCounts);

                //�ź� ���۷����� �߰�
                otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[1]++;

                otherPlayerGameInfo[userNum - 1].nowHoldingArcaneCount++;

            }

        }

        if (userNum == 0)
        {
            myPlayerGameInfo.accumulatedCounts++;
            myPlayerGameInfo.HoldingStone.Add(tempStone);


            //myPlayerGameInfo.RearrangeHoldingStones();

            //completion ����
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

            //completion ����
            if (isCompletionCheck)
            {
                //�̰� other�� �������� �ʾұ⿡ ���� �ȵȴ�.
                //otherPlayerGameInfo[userNum - 1].RenewCompletionInfo(tempStone.stoneDetailType);
            }
        }

    }

    #endregion


    //���� ���� ���� �ڷ�ƾ ����
    void InitialFirstSelectionSetting()
    {
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstSelectionReady;

        StartCoroutine(InitialFirstSelectionReady());
    }

    //�ڷ�ƾ ����
    IEnumerator InitialFirstSelectionReady()
    {
        //���� ���� �� ��� ���
        yield return new WaitForSeconds(0.3f);

        //���� ����
        CrystalAllControl(0, -1);

        yield return new WaitForSeconds(0.2f);
   
        //slider�� Ȱ��ȭ
        SliderHandlerOn(-1, false);
        //darker Ȱ��ȭ
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.darker;

        //���� Ȱ��ȭ
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
            //crystal ���� ��������
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
            //crystal ������ ����
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
            //crystal ��Ȱ��ȭ
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
    

    //���� ���� (altar Ȱ��ȭ)
    //drag �Ǵ°� �ν��ؼ� 
    void CheckEveryoneSelected()
    {
        //Debug.Log("dd");
        //üũ �� �� �� ���õǸ� ��ư Ȱ��ȭ
        if ((externalGameObjects.SelectionControllerList[0].nowAppliedStoneRefNum !=-1) && (externalGameObjects.SelectionControllerList[1].nowAppliedStoneRefNum != -1))
        {
            externalGameObjects.initialSelectionButton.SetActive(true);
        }
        //�� �ܿ��� ��Ȱ��ȭ
        else
        {
            externalGameObjects.initialSelectionButton.SetActive(false);
        }

        //4�� ��� ���ý� �Ѿ��.
        if (mainGameBaseInfo.initialSelectionFinishedPass == 4)
        {
            externalGameObjects.initialSelectionButton.SetActive(false);

            //externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;
            //���� ��� �������� �� �����ߴ��� Ȯ���Ѵٸ� �������� �Ѿ��.
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstPlayer;

            StartCoroutine(InitialFirstPlayerShow());
        }

    }


    IEnumerator InitialFirstPlayerShow()
    {

        //���� �� �巡�� ��, �ٸ��͵� �̹��� �ٲ���
        #region SelfDrag and SpriteChange
        externalGameObjects.stoneClickController.thisPlayingNow = false;

        //�ΰ� ���ÿ� Ȱ��ȭ�Ǽ� �ڵ����� selection���� ��ġ�� ��������.
        //externalGameObjects.gameObjectSpawner
        externalGameObjects.SelectionControllerList[0].gameObject.SetActive(false);
        externalGameObjects.SelectionControllerList[1].gameObject.SetActive(false);


        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[0]].draggedTargetPosition = myCommonObjects.StoneOnHandBriefsPair[0].transform.position;
        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[0]].stoneNowStatus = StoneOnHand.StoneNowStatusEnum.normalDragControl_SelectionShrink_SelfDragged;

        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[1]].draggedTargetPosition = myCommonObjects.StoneOnHandBriefsPair[1].transform.position;
        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[1]].stoneNowStatus = StoneOnHand.StoneNowStatusEnum.normalDragControl_SelectionShrink_SelfDragged;

        yield return new WaitForSeconds(0.5f);

        
        //6���� ���� ���� �� �̹��� ����
        for (int i=0; i<3; i++)
        {
            for (int j = 1; j >= 0; j--)
            {
                //���� ����ִ� ���� �����
                externalGameObjects.gameObjectSpawner.OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[j].gameObject.SetActive(false);

                //���Ӱ� ����
                otherCommonObjects[i].StoneOnHandBriefsPair[j].ActiveShowObjects(0);
                //externalGameObjects.OtherStoneOnHandBriefList[(2 * i) + j].ActiveShowObjects();

                //select�� 0�� 1���� ������ ����
                otherCommonObjects[i].StoneOnHandBriefsPair[j].ChangeSprite((int)otherPlayerGameInfo[i].HoldingStone[otherPlayerGameInfo[i].SelectStonesRef[j]].stoneDetailType, otherPlayerGameInfo[i].HoldingStone[otherPlayerGameInfo[i].SelectStonesRef[j]].stoneNumber);
                //externalGameObjects.OtherStoneOnHandBriefList[(2 * i) + j].ChangeSprite((int)otherPlayerGameInfo[i].HoldingStone[otherPlayerGameInfo[i].SelectStonesRef[j]].stoneDetailType, otherPlayerGameInfo[i].HoldingStone[otherPlayerGameInfo[i].SelectStonesRef[j]].stoneNumber);
                
                yield return new WaitForSeconds(0.2f);
            }

        }
        #endregion

        yield return new WaitForSeconds(0.5f);


        //ȸ���ؼ� ���� ����, ���͵� ��������� ��ȯ
        #region Rotate And My Change
        //���� �� �� ȸ��
        for (int i = 0; i < 3; i++)
        {
            for (int j = 1; j >= 0; j--)
            {
                otherCommonObjects[i].StoneOnHandBriefsPair[j].ActiveRotationObjects();
                //externalGameObjects.OtherStoneOnHandBriefList[(2 * i) + j].ActiveRotationObjects();

                yield return new WaitForSeconds(0.2f);
            }

        }

        //����Ʈ �߻��� ���ؼ� �̷��� ó���Ѵ�.
        //�ڵ����� ��� ���� ���� ������Ʈ�� ��Ȱ��ȭ ��Ų��.
        //�ٽ� ���Ӱ� Ȱ��ȭ�Ѵ�.
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

        //���� �ǰ�
        JudgeFirstPlayer(mainGameBaseInfo.judgeBonus);

        yield return new WaitForSeconds(0.1f);

        //�簢�� ����Ʈ
        #region square effect
        //�������� ����� ����Ʈ �߻�
        //���� ������ ���� ����Ʈ ������
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

        //���� �߻��� ���� �ӽù���
        //���� ���� �߰�
        if(mainGameBaseInfo.firstPlayerNum == -1)
        {
            mainGameBaseInfo.firstPlayerNum = 1;
            //Debug.Log("���� ���, ���� �߰� �ʿ�");
            //yield break;
        }


        //���� ���� �߰� ����Ʈ
        //���� ������ �����ϸ� ������ ����Ǵ� ����
        if(mainGameBaseInfo.firstPlayerNum == 0)
        {

            myCommonObjects.Effect_StoneDissolvesPair[0].StartDissolveStone();
            myCommonObjects.Effect_StoneDissolvesPair[1].StartDissolveStone();

        }
        //�̿� �����
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
        //����Ʈ Ȱ��ȭ
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

        //dissolve �ı���
        yield return new WaitForSeconds(3.4f);

        externalGameObjects.audioManagerInGame.ChangeSFXAndPlay(AudioManagerInGame.SFXname.StoneBurn);

        yield return new WaitForSeconds(1.2f);

        //ȭ�� �ٽ� ���
        externalGameObjects.effect_Darker.nowDarkStatus = Effect_Darker.DarkStatus.none;

        //ũ����Ż�� dark->off �� �ǵ�����.
        for (int i = 0; i < 4; i++)
        {
            //�����ϸ� pass
            //�ٸ��� ����
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
        //����Ʈ ��� ��Ȱ��ȭ
        for (int i=0; i<3; i++)
        {
            externalGameObjects.SquareFlashList[i].SetActive(false);
            otherCommonObjects[i].CrystalShiningEffect.SetActive(false);
        }

        externalGameObjects.effect_ShiningGathering.gameObject.SetActive(false);
        myCommonObjects.CrystalShiningEffect.SetActive(false);
        #endregion

        //������ ���� ���� ���� �������� ���� �ְ� �ð�������� ����
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstDump;

        yield break;
    }

    //���� 2�� �����ؼ� ���� ��
    void JudgeFirstPlayer(MainGameBaseInfo.judgeType tempJudgeType)
    {
        //������ ���
        myPlayerGameInfo.CalFirstPlayerScore();

        //�ٸ� ���� ���
        for (int i = 0; i < 3; i++)
        {
            otherPlayerGameInfo[i].CalFirstPlayerScore();
        }

        //-1 �̸� �ְ������� �ߺ��Ǵ� ���
        //int tempFirstPlayerNum = 0;
        int tempMaxScore = myPlayerGameInfo.firstPlayerScore;

        mainGameBaseInfo.firstPlayerNum = 0;


        if(tempJudgeType == MainGameBaseInfo.judgeType.none)
        {
            //���� ���1�� ��
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
            //���� ���1�� ��
            if (myPlayerGameInfo.firstPlayerScore <= otherPlayerGameInfo[0].firstPlayerScore)
            {
                mainGameBaseInfo.firstPlayerNum = 1;
                tempMaxScore = otherPlayerGameInfo[0].firstPlayerScore;
            }
        }


        //���1 �׸��� 2 ��
        //���2 �׸��� 3 ��
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

    //���� �� 
    void DumpInitialStones()
    {
        //�Ϲ����� ��� ���� �����Ǿ��� ���
        //���� ���� ��� ������ ������ �����Ѵ�.
        //���� ���� 7,8���� ��ġ�ϸ鼭 ������ �� ���Եȴ�.
        //�������� ���� ������ ������ Dumped�� �̵���Ų��.
        //2�� �����ϸ� ��ȿó���Ѵ�.

        //���� ������ ���
        if (mainGameBaseInfo.firstPlayerNum > -1)
        {
            //���� �߽����� �ձ۰� ������.
            //�ϴ� ����ڴ� �ı��Ѵ�.
            DestroySelectedStone(mainGameBaseInfo.firstPlayerNum);

            //DumpSelectedStone
            for (int i = mainGameBaseInfo.firstPlayerNum+1; i < mainGameBaseInfo.firstPlayerNum + 4; i++)
            {
                DumpSelectedStone(i % 4);
            }


            //���� gameObject ������ ó��
            #region dump gameObject
            //��� ������ ������
            if (mainGameBaseInfo.firstPlayerNum != 0)
            {
                myCommonObjects.StoneOnHandBriefsPair[0].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[(mainGameBaseInfo.firstPlayerNum * 2) - 2].transform, 0);

                myCommonObjects.StoneOnHandBriefsPair[1].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[(mainGameBaseInfo.firstPlayerNum * 2) - 1].transform, 0);

            }

            for (int i=0; i<3; i++)
            {
                for(int j=0; j<2; j++)
                {
                    //�ٸ� �͵鸸 ���ó��
                    if(mainGameBaseInfo.firstPlayerNum -1 != i)
                    {
                        otherCommonObjects[i].StoneOnHandBriefsPair[j].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[(6 + j + ((mainGameBaseInfo.firstPlayerNum - 1 - i) * 2)) % 8].transform, 0);
                    }
                }
            }
            #endregion

            //������ ���� �ܰ�� �̵�
            mainGameBaseInfo.nextPlayingPlayer = mainGameBaseInfo.firstPlayerNum;
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_InitialFirstEnd;
        }
        //���� �������� �ʾҴٸ� ������ �ٽ� �̵��Ѵ�.
        else
        {
            //�׳� �� �ı��ع�����.
            for (int i = 0; i < 4; i++)
            {
                DestroySelectedStone(i);
            }

            //���� �ʱ�ȭ
            mainGameBaseInfo.initialSelectionFinishedPass = 0;

            //������ ���� ������ ������ ��ȿ�� �Ѵ�.
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

    //���� ������.
    //0���� ����, 1,2,3 ������� �ð���� �ٸ� �����̴�.

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

        //0�� ������
        if (userNum == 0)
        {
            StoneInfo tempStone = myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[0]];

            mainGameBaseInfo.DumpedStonesList.Add(tempStone);

            myPlayerGameInfo.HoldingStone.RemoveAt(myPlayerGameInfo.SelectStonesRef[0]);

            //�߰������� reference�� ���ҽ�Ų��.
            myPlayerGameInfo.HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }
        else
        {
            StoneInfo tempStone = otherPlayerGameInfo[userNum - 1].HoldingStone[otherPlayerGameInfo[userNum - 1].SelectStonesRef[0]];

            mainGameBaseInfo.DumpedStonesList.Add(tempStone);

            otherPlayerGameInfo[userNum - 1].HoldingStone.RemoveAt(otherPlayerGameInfo[userNum - 1].SelectStonesRef[0]);

            //�߰������� reference�� ���ҽ�Ų��.
            otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }

        //���� �� 1�� ������
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

        //1�� ������
        if (userNum == 0)
        {
            StoneInfo tempStone = myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[1] + tempVal];

            mainGameBaseInfo.DumpedStonesList.Add(tempStone);

            myPlayerGameInfo.HoldingStone.RemoveAt(myPlayerGameInfo.SelectStonesRef[1] + tempVal);

            //�߰������� reference�� ���ҽ�Ų��.
            myPlayerGameInfo.HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }
        else
        {
            StoneInfo tempStone = otherPlayerGameInfo[userNum - 1].HoldingStone[otherPlayerGameInfo[userNum - 1].SelectStonesRef[1] + tempVal];

            mainGameBaseInfo.DumpedStonesList.Add(tempStone);

            otherPlayerGameInfo[userNum - 1].HoldingStone.RemoveAt(otherPlayerGameInfo[userNum - 1].SelectStonesRef[1] + tempVal);

            //�߰������� reference�� ���ҽ�Ų��.
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
        //0�� �ı�
        if (userNum == 0)
        {
            StoneInfo tempStone = myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[0]];

            mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

            myPlayerGameInfo.HoldingStone.RemoveAt(myPlayerGameInfo.SelectStonesRef[0]);

            //�߰������� reference�� ���ҽ�Ų��.
            myPlayerGameInfo.HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }
        else
        {
            StoneInfo tempStone = otherPlayerGameInfo[userNum - 1].HoldingStone[otherPlayerGameInfo[userNum - 1].SelectStonesRef[0]];

            mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

            otherPlayerGameInfo[userNum - 1].HoldingStone.RemoveAt(otherPlayerGameInfo[userNum - 1].SelectStonesRef[0]);

            //�߰������� reference�� ���ҽ�Ų��.
            otherPlayerGameInfo[userNum - 1].HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }

        //���� �� 1�� ������
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

        //1�� ������
        if (userNum == 0)
        {
            StoneInfo tempStone = myPlayerGameInfo.HoldingStone[myPlayerGameInfo.SelectStonesRef[1] + tempVal];

            mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

            myPlayerGameInfo.HoldingStone.RemoveAt(myPlayerGameInfo.SelectStonesRef[1] + tempVal);

            //�߰������� reference�� ���ҽ�Ų��.
            myPlayerGameInfo.HoldingStoneReferenceCounts[(int)tempStone.stoneDetailType]--;
        }
        else
        {
            StoneInfo tempStone = otherPlayerGameInfo[userNum - 1].HoldingStone[otherPlayerGameInfo[userNum - 1].SelectStonesRef[1] + tempVal];

            mainGameBaseInfo.DestroyedStonesList.Add(tempStone);

            otherPlayerGameInfo[userNum - 1].HoldingStone.RemoveAt(otherPlayerGameInfo[userNum - 1].SelectStonesRef[1] + tempVal);

            //�߰������� reference�� ���ҽ�Ų��.
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

    //MainGame�� ����
    void StartMainGame()
    {
        //���� �ð� �� ������ ���� ���ؼ� �����Ѵ�.
        Invoke("ChangeSelectedToDump",2.6f);

        mainGameBaseInfo.latestTurnUser = -1;

        //���� ���¸� �����Ѵ�.
        //��Ʈ��ũ ȯ���� �����ϰ� ������ ���ٰ� ���������� �����Ѵ�.
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Neutral;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralTurnCheck;
    }

    //�̹����� ��ü�Ѵ�.
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
    //�� ����
    void NeutralTurnChecking()
    {
        //���� ��ٸ� Ư�� ���
        if (mainGameBaseInfo.gameType == GameType.versusNormalGame)
        {
            //��Ʈ��ũ ��� ���·� �ѱ��.
            mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralUserAllCheck;
        }
        //�ƴ� ��쿡�� ������ ���� �ѱ��.
        else
        {
            PlayingTurnChange();
        }
    }

    void NeutralUserAllChecking()
    {
        //����� ������ ��쿡�� ó���Ѵ�.
        //��Ʈ��ũ ��Ȯ��
        //������ ������ ������ ���缭 ���� �����ش�.

        //���� ����� ���ٸ� �Ϻη� �����̸� �÷��� ��ٷ��ش�.

        //���� �ð����� ��⸦ �����.
        //mainGameBaseInfo.neutralNetworkCheckingList[0] = true;
        //PlayingTurnChange();

    }

    //�������� ������ error ������ ��쿡 ��ġ�� ����
    void NeutralErrorChecking()
    {



    }


    //Ư�� �÷��̾�� ���� �ѱ��.
    void PlayingTurnChange()
    {
        //���� ������ �ѱ�� �������
        if (mainGameBaseInfo.nextPlayingPlayer == 0)
        {
            //�÷��̾� ��ȣ ����
            mainGameBaseInfo.nowPlayingPlayer = 0;

            //������ �ڷ�ƾ ���� 
            StartCoroutine(MyReadyShow());
        }
        //��� ������ �ѱ�� �������
        else
        {
            //Debug.Log("����");
            //�÷��̾� ��ȣ ����
            mainGameBaseInfo.nowPlayingPlayer = mainGameBaseInfo.nextPlayingPlayer;

            //������ �ڷ�ƾ ����
            StartCoroutine(OtherReadyShow());
        }
    }

    #endregion

    #region MyMainGameFunctions

    IEnumerator MyReadyShow()
    {
        //���� �̵�
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_My;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_MyReady;

        yield return new WaitForSeconds(1.0f);

        //Ȥ�ö� �̹� �迭�Ǿ� �ִٸ� ���ġ ���� �ʴ´�.
        if (!CheckMyArranged())
        {
            //���� ���¿��� �ƹ��� ��ȭ ���� 3,4�� ����� �����ϴ� ������ �Ѵ�.
            //��� ������ٰ� �ٽ� ��迭 �ؼ� ��Ÿ����
            externalGameObjects.gameObjectSpawner.StoneDisappearAllFunction();

            yield return new WaitForSeconds(0.4f);

            //�������� �̹��� �� ��ġ ��ȯ
            #region inner change

            //�ϴ� ���� ��Ȱ��ȭ
            for (int i = 0; i < 10; i++)
            {
                externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i].gameObject.SetActive(false);
            }

            //�켱 ��迭�� �ƹ��� �������� ����.
            //myPlayerGameInfo.RearrangeHoldingStones();

            //���� �� ����
            for (int i = 0; i < myPlayerGameInfo.HoldingStone.Count; i++)
            {
                //3�̻��� 
                if (i >= 3)
                {
                    if (i + 2 < 10)
                    {
                        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i + 2].gameObject.SetActive(true);

                        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i + 2].ResetStone();
                        externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[i + 2].SetAllActive(true);

                        //�ش� ������Ʈ�� ������ �� ����
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

                //3,4���� ����д�.
                //externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[myPlayerGameInfo.SelectStonesRef[i]].gameObject.SetActive(false);


                //�ش� ������Ʈ�� ������ �� ����
                //externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
            }
            #endregion

            yield return null;
            //�ٽ� ��Ÿ����.
            externalGameObjects.gameObjectSpawner.StoneAppearAllFunction();
        }

        //�� �� ũ����Ż ����
        //�Ҹ��� ����Ʈ �߻�
        myCommonObjects.CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;
        externalGameObjects.audioManagerInGame.ChangeSFXAndPlay(AudioManagerInGame.SFXname.AlarmPositive);

        yield return null;

        //�ý��� ������ ���� �̴´�.
        //���� ���� ���� �� �����Ѵ�.
        MyPullStones();

        #region 3,4 setting and appear
        yield return new WaitForSeconds(0.1f);

        //����Ʈ�� �ߵ����ѳ��´�.
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

        //��� ������ٰ� �ٽ� ��迭 �ؼ� ��Ÿ����
        #region new arrange
        externalGameObjects.gameObjectSpawner.StoneDisappearAllFunction();
        yield return new WaitForSeconds(0.3f);
        //������� ����

        //��迭
        myPlayerGameInfo.RearrangeHoldingStones();

        yield return null;
        
        //Completion�� Reference�� �����Ѵ�.------------------------
        //���� Integration Recover�� ���� index�� �������ش�.
        myPlayerGameInfo.RenewCompletionInfoAll();
        //----------------------------------------------------------

        yield return null;

        //���� �� ����
        for (int i = 0; i < myPlayerGameInfo.HoldingStone.Count; i++)
        {
            //�ش� ������Ʈ�� ������ �� ����
            externalGameObjects.gameObjectSpawner.MyBoardArea_HandList.InnerGameObjectList[i].GetComponent<StoneOnHand>().ChangeSprite((int)myPlayerGameInfo.HoldingStone[i].stoneDetailType, myPlayerGameInfo.HoldingStone[i].stoneNumber);
        }
        yield return null;


        //�ٽ� ��Ÿ����.
        externalGameObjects.gameObjectSpawner.StoneAppearAllFunction();
        #endregion

        yield return null;

        //��� �� ��Ʈ�� ����
        externalGameObjects.gameObjectSpawner.StoneMovableAllFunction(true);

        //
        yield return null;
        CalculateRecoverDumpIndex();

        //���� ������ �ִ��� ���� ������ �����Ѵ�.
        //�� ������ ����� �������� ����
        yield return new WaitForSeconds(0.2f);
        AnalyzeMyStonesAndActiveUI(true);

        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_MyWait;

        externalGameObjects.stoneClickController.thisPlayingNow = true;

        yield break;

    }

    //�迭 ������ Ȯ���Ѵ�.
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
        //Ȥ�ó� ������ ���� �ʴ� ���
        else if (myPlayerGameInfo.HoldingStone.Count >= 9)
        {
            return false;
        }
        //4�� �̻�
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

    //������ 2�� ��ο� �Ѵ�.
    void MyPullStones()
    {

        PullOneStone(0, false);

        PullOneStone(0, false);

        //myPlayerGameInfo.RenewCompletionInfoAll();

    }


    //���� �ڽ��� �и� �м��Ѵ�. ���������� UI�� Ȱ��ȭ�Ѵ�.
    //�������� ������ ��ư�� �־����� ������ ��Ʈ��


    //�̸� ���� wait���� ����� UI�� Ȱ��ȭ��Ų��.
    //�ռ� ���� UI
    //���� ���� UI
    //�ź� ���� UI
    //���� ���� UI
    void AnalyzeMyStonesAndActiveUI(bool tempActiveAll)
    {

        if (tempActiveAll)
        {
            //�⺻ Ȱ��ȭ
            myPlayerUI.UIAll.SetActive(true);

            //���� ���¸� �м��ؼ� �κ������� Ȱ��ȭ �Ѵ�.

            //���� completion�� 0�̻��� ���� ���� ��쿡 Ȱ��ȭ
            //���� ���Ӱ� index�� �ش��ϴ� ����Ʈ�� ���� ó��
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
            
            //���� completion�� -1�� ��쿡 Ȱ��ȭ
            //���� ���Ӱ� index�� �ش��ϴ� ����Ʈ�� ���� ó��
            if(myPlayerGameInfo.IntegrationIndexList.Count > 0)
            {
                myPlayerUI.integrationButton.SetActive(true);
            }
            else
            {
                myPlayerUI.integrationButton.SetActive(false);
            }

            //������ �̰� �� Ȱ��ȭ �ǱⰡ ��ƴ�.
            //�⺻������ 2���� �����ϱ� ������ Ȱ��ȭ��� ���� ��
            if(myPlayerGameInfo.HoldingStone.Count >= 2)
            {
                myPlayerUI.dumpButton.SetActive(true);
            }
            else
            {
                myPlayerUI.dumpButton.SetActive(false);
            }
            

            //�ź� Ȱ���ư
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

    //Index�� dump�� ������� dump�� index�� �����Ѵ�.
    //RecoverAvailIndexList * 10 + DumpedStonesList ���� �����Ѵ�.
    void CalculateRecoverDumpIndex()
    {
        //�̰��� �������̴�.
        for(int i=0; i< mainGameBaseInfo.DumpedStonesList.Count; i++)
        {
            //dump stone�� ���� �м�
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

            //���� ���� ��ȣ
            int tempDumpNum = mainGameBaseInfo.DumpedStonesList[i].stoneNumber;

            //�ϴ� �⺻���� Ÿ���� �´��� Ȯ���Ѵ�.
            for (int j=0; j< myPlayerGameInfo.RecoverAvailIndexList.Count; j++)
            {
                //�ش� ���� ���� ���̸�(������ ������)
                //tempType�� ��ǻ� ��ȣ�� ������ ����̴�.
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
                            //������ ���
                            if(tempDumpNum == tempType)
                            {
                                myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                            }
                        }
                        else if(tempType < 8)
                        {
                            //��� ���
                            if (tempDumpNum == myPlayerGameInfo.CompletionTypeElementsList[tempNum2].completionStairList[tempType-5])
                            {
                                myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                            }
                            //�̹� �ϼ��� �� ���
                            else if(myPlayerGameInfo.CompletionTypeElementsList[tempNum2].completionStairList[tempType - 5] == -1)
                            {
                                if(tempDumpNum < (tempType - 5 + 3) && (tempType - 5) <= tempDumpNum)
                                {
                                    myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                                }
                            }
                        }
                        //��� ��ü
                        else if(tempType == 8)
                        {
                            //��� ���
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
                            //������ ���
                            if(tempDumpNum == tempType)
                            {
                                myPlayerGameInfo.RecoverDumpIndexRefList.Add(myPlayerGameInfo.RecoverAvailIndexList[j] * 10 + i);
                            }
                        }
                        else if(tempType == 3)
                        {
                            //��� ���
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
                            //������ ���
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


    //�ڽ� ����ϸ鼭 ��� ������
    //Analye�� ���� Ȱ��ȭ�� UI�� ���� �۾��Ѵ�.
    void MyWaitingForSelectUI()
    {

        switch (mainGameBaseInfo.nowMyControlType)
        {
            case MyControlType.dump:

                if ((externalGameObjects.SelectionControllerList[0].nowAppliedStoneRefNum != -1) && (externalGameObjects.SelectionControllerList[1].nowAppliedStoneRefNum != -1))
                {
                    externalGameObjects.dumpExecutionButton.SetActive(true);
                }
                //�� �ܿ��� ��Ȱ��ȭ
                else
                {
                    externalGameObjects.dumpExecutionButton.SetActive(false);
                }

                break;

            //�� ���� �ܰ�
            case MyControlType.recoverDumpSelect:

                break;

            case MyControlType.recover:

                //���� plane�� ����� ���� ���� ����
                if (CalculateIntegrationSmall(1) >= 0)
                {
                    externalGameObjects.integrationExecutionButton.SetActive(true);
                    externalGameObjects.SelectionSmallEffect.SetActive(true);

                    //��� ����
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

                //���� plane�� ����� ���� ���� ����
                if (CalculateIntegrationSmall(0) >= 0)
                {
                    externalGameObjects.integrationExecutionButton.SetActive(true);
                    externalGameObjects.SelectionSmallEffect.SetActive(true);

                    //��� ����
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


    //�ռ� index
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

        //������ ��� ������ ���� ����
        if(tempNum >= -1)
        {
            //���⼭ �ϼ��� ���� ������ ��ȯ�Ѵ�.

            //1���� ���� dump�� �����Ѵ�.
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

                    //���� same stair�� �����Ѵ�.
                    if (myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[0].nowAppliedStoneRefNum].stoneDetailType == StoneDetailType.arcane)
                    {
                        //�ź� �ݿ�
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


                        //same�� ���
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

                        //�ٸ��ͳ��� ������ ��ȯ
                        if(myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[1].nowAppliedStoneRefNum].stoneNumber == myPlayerGameInfo.HoldingStone[externalGameObjects.SelectionSmallControllerList[0].nowAppliedStoneRefNum].stoneNumber)
                        {
                            return -1;
                        }
                        //stair�� ���
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
                        //3�� ����� ���� �ִ� ���
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
        //���� �ð� �� ������ ���� ���ؼ� �����Ѵ�.
        Invoke("ChangeSelectedToDump", 2.8f);

        mainGameBaseInfo.latestTurnUser = mainGameBaseInfo.nowPlayingPlayer;

        //���� �÷��� ���� ����
        mainGameBaseInfo.nextPlayingPlayer = 1;

        //Ŭ�� �Ұ����ϰ� �ٲٱ�
        externalGameObjects.stoneClickController.thisPlayingNow = false;

        //���� ���¸� �����Ѵ�.
        //��Ʈ��ũ ȯ���� �����ϰ� ������ ���ٰ� ���������� �����Ѵ�.
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Neutral;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralTurnCheck;
    }

    #endregion

    #region OtherMainGameFunctions

    IEnumerator OtherReadyShow()
    {
        //���� �̵�
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Other;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_OtherReady;

        yield return new WaitForSeconds(1.0f);

        //�ý��� ������ ���� �̴´�.
        //���� ���� ���� �� �����Ѵ�.
        //MyPullStones();
        OtherPullStones(mainGameBaseInfo.nowPlayingPlayer);


        //ũ����Ż ������
        CrystalAllControl(1, -1);

        otherCommonObjects[mainGameBaseInfo.nowPlayingPlayer - 1].CrystalController.nowCrystalStatus = CrystalController.CrystalStatus.crystalActiveOn_MyTurn;

        //���� �� �̴� ����
        //0�� ��Ÿ����
        externalGameObjects.gameObjectSpawner.OtherBoardAreaTransformHandList[mainGameBaseInfo.nowPlayingPlayer - 1].OtherBoardAreaInnerTransformList[0].gameObject.SetActive(true);
        //externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[0].SetAllActive(true);
        externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(0, mainGameBaseInfo.nowPlayingPlayer);

        yield return new WaitForSeconds(0.2f);

        //1�� ��Ÿ����
        externalGameObjects.gameObjectSpawner.OtherBoardAreaTransformHandList[mainGameBaseInfo.nowPlayingPlayer - 1].OtherBoardAreaInnerTransformList[1].gameObject.SetActive(true);
        //externalGameObjects.gameObjectSpawner.MyBoardAreaStoneOnHandList[1].SetAllActive(true);
        externalGameObjects.gameObjectSpawner.StoneAppearFromRightFunction(1, mainGameBaseInfo.nowPlayingPlayer);


        yield return new WaitForSeconds(0.3f);

        //���� ���� ��迭
        otherPlayerGameInfo[mainGameBaseInfo.nowPlayingPlayer - 1].RearrangeHoldingStones();

        yield return null;

        //Completion�� Reference�� �����Ѵ�.------------------------
        //���� Integration Recover�� ���� index�� �������ش�.
        otherPlayerGameInfo[mainGameBaseInfo.nowPlayingPlayer - 1].RenewCompletionInfoAll();
        //----------------------------------------------------------

        //yield return null;

        //CalculateRecoverDumpIndex();

        //���� ������ �ִ��� ���� ������ �����Ѵ�.
        //�� ������ ����� �������� ����
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
        //Ư�� �ð� ���� Dump�� �Ѵ�.
        OtherDumpExecution(mainGameBaseInfo.nowPlayingPlayer - 1);

    }

    public void OtherDumpExecution(int userNum)
    {
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_OtherDump;

        StartCoroutine(OtherDumpExecutionCor(userNum));
    }

    IEnumerator OtherDumpExecutionCor(int userNum)
    {
        //�켱 �⺻���� select�� �����Ѵ�.
        OtherRandomSelect(userNum);

        yield return new WaitForSeconds(1.2f);


        //�⺻ �� ����
        for (int i = 1; i >= 0; i--)
        {

            externalGameObjects.gameObjectSpawner.OtherBoardAreaTransformHandList[userNum].OtherBoardAreaInnerTransformList[i].gameObject.SetActive(false);
            //���Ӱ� ����
            otherCommonObjects[userNum].StoneOnHandBriefsPair[i].gameObject.SetActive(true);

            //select�� 0�� 1���� ������ ����
            otherCommonObjects[userNum].StoneOnHandBriefsPair[i].ChangeSprite((int)otherPlayerGameInfo[userNum].HoldingStone[otherPlayerGameInfo[userNum].SelectStonesRef[i]].stoneDetailType, otherPlayerGameInfo[userNum].HoldingStone[otherPlayerGameInfo[userNum].SelectStonesRef[i]].stoneNumber);

            otherCommonObjects[userNum].StoneOnHandBriefsPair[i].ActiveShowObjects(1);

            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(0.6f);

        
        externalGameObjects.dumpController.StartCreate2AndDestroy2();

        //�������� ������ �ִϸ��̼�
        for (int i = 0; i < 2; i++)
        {
            //otherCommonObjects[userNum].StoneOnHandBriefsPair[i].gameObject.SetActive(false);
            otherCommonObjects[userNum].StoneOnHandBriefsPair[i].SetAbandonTargetTransform(externalGameObjects.dumpController.TargetGameObjectList[i].transform, 0);

        }

        yield return null;

        //���� dump�� �����ϰ� select�� �ʱ�ȭ�Ѵ�.
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
        //���� �ð� �� ������ ���� ���ؼ� �����Ѵ�.
        Invoke("ChangeSelectedToDump", 2.8f);

        mainGameBaseInfo.latestTurnUser = mainGameBaseInfo.nowPlayingPlayer;

        //���� �÷��� ���� ����
        mainGameBaseInfo.nextPlayingPlayer = mainGameBaseInfo.nowPlayingPlayer + 1;
        if (mainGameBaseInfo.nextPlayingPlayer >= 4)
        {
            mainGameBaseInfo.nextPlayingPlayer = mainGameBaseInfo.nextPlayingPlayer % 4;
        }

        //Ŭ�� �Ұ����ϰ� �ٲٱ�
        externalGameObjects.stoneClickController.thisPlayingNow = false;

        //���� ���¸� �����Ѵ�.
        //��Ʈ��ũ ȯ���� �����ϰ� ������ ���ٰ� ���������� �����Ѵ�.
        mainGameBaseInfo.nowGameStepEnum = NowGameStepEnum.MainGame_Neutral;
        mainGameBaseInfo.nowGameStepDetailEnum = NowGameStepDetailEnum.MainGame_NeutralTurnCheck;
    }

    #endregion

    #region EngameFunctions

    #endregion


}