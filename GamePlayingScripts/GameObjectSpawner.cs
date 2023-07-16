using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameObjectSpawner : MonoBehaviour
{
    [Space(5)]
    [Header("등장 effect")]
    //[Space(5)]
    //public int appearTargetNum = 0;
    [Space(5)]
    public float appearSpeed = 3f;
    public float appearDistance = 70f;

    [Space(5)]
    public float appearRotationSpeed = 2f;
    public float appearRotation = 40f;

    [Space(5)]
    public float appearSpeed_other = 6f;
    public float appearDistance_other = 140f;

    [Space(5)]
    public float appearAccel = 1.05f;
    private float tempAppearSpeed;
    private float tempAppearRotationSpeed;
    private float tempAppearSpeed_other;

    [Space(5)]
    public float appearTiming = 0.1f;
    public float appearTiming_2 = 0.4f;
    public float appearTiming_3 = 0.2f;
    public float forceBreakTiming = 2f;

    [Space(15)]
    [Header("Stone Image info")]
    //참조를 편하게 하기 위해 그냥 한 곳에 넣기로 한다.
    [Space(5)]
    public List<StoneImageList> ImageListAll;
    /*
    public StoneImageList CompleteImageListAll;
    [Space(5)]
    public StoneImageList ArcaneImageListAll;
    [Space(5)]
    public List<StoneImageList> ElementImageListAll;
    [Space(5)]
    public List<StoneImageList> OpticImageListAll;
    [Space(5)]
    public List<StoneImageList> MineralImageListAll;
    */

    [Space(15)]
    [Header("Gem List info")]
    [Space(5)]
    public List<GameObject> GemObjectList;
    //public List<Mesh> GemMeshList;
    [Space(5)]
    public List<GameObject> ParticleObjectList;
    [Space(5)]
    public List<Material> GemMaterialList;

    [Space(15)]
    //여기에 게임 오브젝트를 child로 넣어준다.
    public GameObject myBoardArea_Hand;
    public List<GameObject> OtherBoardArea_Hand;

    //참조용 기본적인 오브젝트 위치
    public BoardAreaInnerList MyBoardArea_HandList;

    //부가적인 참조
    [ReadOnly] public List<StoneOnHand> MyBoardAreaStoneOnHandList;
    [ReadOnly] public List<Transform> MyBoardAreaTransformList;

    [Space(5)]
    //public List<BoardAreaInnerList> OtherBoardArea_HandList;
    public List<OtherBoardAreaTransformList> OtherBoardAreaTransformHandList;

    /*
    [Space(15)]
    [Header("StoneObjectBase")]
    [Space(5)]
    public GameObject StonePrefab;
    public GameObject MainSymbol;
    public GameObject MainSymbolShadow;
    */

    [System.Serializable]
    public struct StoneImageList
    {
        public List<Sprite> InnerStoneImageList;
    }

    [System.Serializable]
    public struct BoardAreaInnerList
    {
        public List<GameObject> InnerGameObjectList;
    }

    [System.Serializable]
    public struct OtherBoardAreaTransformList
    {
        public List<Transform> OtherBoardAreaInnerTransformList;
    }

    /*
    public void StoneCreationFunction()
    {
        //CreateStone(1,1,0);
        //CreateStone(3,2,1);
        //CreateStone(7,11,2);
    }
    */
    //이제 사용하지 않는 함수
    /*
    public void CreateStone(int targetNum, int stoneDetailType, int stoneNum)
    {
        Transform tempTransform = MyBoardArea_HandList.InnerGameObjectList[targetNum].transform;

        GameObject tempSymbol = Instantiate(MainSymbol, tempTransform);
        tempSymbol.GetComponent<SpriteRenderer>().sprite = ImageListAll[stoneDetailType].InnerStoneImageList[stoneNum];

        GameObject tempSymbolShadow = Instantiate(MainSymbolShadow, tempTransform);
        tempSymbolShadow.GetComponent<SpriteRenderer>().sprite = ImageListAll[stoneDetailType].InnerStoneImageList[stoneNum];

        GameObject tempStone = Instantiate(StonePrefab, tempTransform);
    }
    */

    //이미지를 바꾸는데 활용한다.
    //하휘 오브젝트에 접근해 정보를 변경한다.
    public void ChangeStone(int targetNum, int stoneDetailType, int stoneNum)
    {
        //symbol, symbol shadow, stone
        //GameObject tempGameObject
    }

    public void StoneAppearFromRightFunction(int num)
    {
        //StopCoroutine(StoneAppearCoroutine(0));
        StartCoroutine(StoneAppearFromRightCoroutine(num));
        //StartCoroutine(StoneAppearCoroutine(7));
    }

    public void StoneAppearFromRightFunction(int num, int userNum)
    {
        StartCoroutine(StoneAppearFromRightCoroutine(num, userNum));
    }


    public void StoneAppearFromDownFunction(int num)
    {
        StartCoroutine(StoneAppearFromDownCoroutine(num));
    }

    public void StoneDisappearToDownFunction(int num)
    {
        StartCoroutine(StoneDisappearToDownCoroutine(num));
    }

    public void StoneAppearAllFunction()
    {
        for(int i=0; i < 10; i++)
        {
            StartCoroutine(StoneAppearFromDownCoroutine(i));
        }
    }

    public void StoneDisappearAllFunction()
    {
        for (int i = 0; i < 10; i++)
        {
            StartCoroutine(StoneDisappearToDownCoroutine(i));
        }
    }


    //이전에는 자신 돌 연출만 처리했는데 새로운 돌 연출도 처리한다.
    IEnumerator StoneAppearFromRightCoroutine(int targetNum)
    {
        tempAppearSpeed = appearSpeed;
        tempAppearRotationSpeed = appearRotationSpeed;
        tempAppearSpeed_other = appearSpeed_other;

        //보정용 값
        float tempOriginX = MyBoardAreaTransformList[targetNum].localPosition.x;

        //다른 돌들의 값
        List<float> tempOriginOtherZ = new List<float> { 70, 70, 70 };

        for(int i=0; i<3; i++)
        {
            tempOriginOtherZ[i] = OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.z;
        }


        //강제로 일정 시간 이후로도 탈출 가능하게 한다.
        float tempTimeValue = 0f;

        //자신은 기본적으로 오른쪽으로 이동
        MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x + appearDistance, MyBoardAreaTransformList[targetNum].localPosition.y, MyBoardAreaTransformList[targetNum].localPosition.z);
        MyBoardAreaTransformList[targetNum].localEulerAngles = new Vector3(0, 0, - appearRotation);

        //타인은 기본적으로 위로 이동
        for (int i = 0; i < 3; i++)
        {
            OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition = new Vector3(OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.x, OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.y, OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.z - appearDistance_other);
        }

        yield return null;

        //본격적인 이동
        while (true)
        {
            if (MyBoardAreaTransformList[targetNum].localPosition.x < tempOriginX)
            {
                MyBoardAreaTransformList[targetNum].localPosition = new Vector3(tempOriginX, MyBoardAreaTransformList[targetNum].localPosition.y, MyBoardAreaTransformList[targetNum].localPosition.z);
                MyBoardAreaTransformList[targetNum].localEulerAngles = new Vector3(0, 0, 0);

                for (int i = 0; i < 3; i++)
                {
                    OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition = new Vector3(OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.x, OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.y, tempOriginOtherZ[i]);
                }

                yield break;
            }


            //실질적인 위치 변경
            MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x - tempAppearSpeed * Time.deltaTime, MyBoardAreaTransformList[targetNum].localPosition.y, MyBoardAreaTransformList[targetNum].localPosition.z);
            MyBoardAreaTransformList[targetNum].localEulerAngles = new Vector3(0, 0, MyBoardAreaTransformList[targetNum].localEulerAngles.z + tempAppearRotationSpeed * Time.deltaTime);

            for (int i = 0; i < 3; i++)
            {
                OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition = new Vector3(OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.x, OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.y, OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].localPosition.z + tempAppearSpeed_other * Time.deltaTime);
            }


            //나타나는 연출
            if (tempTimeValue > appearTiming)
            {
                MyBoardAreaStoneOnHandList[targetNum].SetAllActive(true);

                for(int i=0; i<3; i++)
                {
                    OtherBoardAreaTransformHandList[i].OtherBoardAreaInnerTransformList[targetNum].gameObject.SetActive(true);
                }
            }

            //시간이 넘으면 자동 탈출
            if (tempTimeValue > forceBreakTiming)
            {
                yield break;
            }

            tempTimeValue += Time.deltaTime;

            /*
            tempAppearSpeed *= appearAccel;
            tempAppearRotationSpeed *= appearAccel;
            tempAppearSpeed_other *= appearAccel;
            */
            //Debug.Log(tempTimeValue);

            yield return null;
        }

        yield break;
    }

    //특정 유저의 것만 나타나게 만들 수도 있다. (overload)
    IEnumerator StoneAppearFromRightCoroutine(int targetNum, int userNum)
    {
        tempAppearSpeed = appearSpeed;
        tempAppearRotationSpeed = appearRotationSpeed;
        tempAppearSpeed_other = appearSpeed_other;

        //다른 돌들의 값
        List<float> tempOriginOtherZ = new List<float> { 70, 70, 70 };

        //기본 선언
        float tempOriginX = MyBoardAreaTransformList[targetNum].localPosition.x;

        if (userNum == 0)
        {
            //보정용 값
            tempOriginX = MyBoardAreaTransformList[targetNum].localPosition.x;
        }
        else
        {
            tempOriginOtherZ[userNum-1] = OtherBoardAreaTransformHandList[userNum-1].OtherBoardAreaInnerTransformList[targetNum].localPosition.z;
        }

        //강제로 일정 시간 이후로도 탈출 가능하게 한다.
        float tempTimeValue = 0f;

        if(userNum == 0)
        {
            //자신은 기본적으로 오른쪽으로 이동
            MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x + appearDistance, MyBoardAreaTransformList[targetNum].localPosition.y, MyBoardAreaTransformList[targetNum].localPosition.z);
            MyBoardAreaTransformList[targetNum].localEulerAngles = new Vector3(0, 0, -appearRotation);
        }
        else
        {
            OtherBoardAreaTransformHandList[userNum-1].OtherBoardAreaInnerTransformList[targetNum].localPosition = new Vector3(OtherBoardAreaTransformHandList[userNum-1].OtherBoardAreaInnerTransformList[targetNum].localPosition.x, OtherBoardAreaTransformHandList[userNum-1].OtherBoardAreaInnerTransformList[targetNum].localPosition.y, OtherBoardAreaTransformHandList[userNum-1].OtherBoardAreaInnerTransformList[targetNum].localPosition.z - appearDistance_other);
        }

        yield return null;

        //본격적인 이동
        while (true)
        {
            //특정 값이 넘으면 종료한다.
            if(userNum == 0)
            {
                if (MyBoardAreaTransformList[targetNum].localPosition.x < tempOriginX)
                {
                    MyBoardAreaTransformList[targetNum].localPosition = new Vector3(tempOriginX, MyBoardAreaTransformList[targetNum].localPosition.y, MyBoardAreaTransformList[targetNum].localPosition.z);
                    MyBoardAreaTransformList[targetNum].localEulerAngles = new Vector3(0, 0, 0);

                    yield break;
                }
            }
            else
            {

                if (OtherBoardAreaTransformHandList[userNum - 1].OtherBoardAreaInnerTransformList[targetNum].localPosition.z > tempOriginOtherZ[userNum - 1])
                {
                    OtherBoardAreaTransformHandList[userNum - 1].OtherBoardAreaInnerTransformList[targetNum].localPosition = new Vector3(OtherBoardAreaTransformHandList[userNum - 1].OtherBoardAreaInnerTransformList[targetNum].localPosition.x, OtherBoardAreaTransformHandList[userNum - 1].OtherBoardAreaInnerTransformList[targetNum].localPosition.y, tempOriginOtherZ[userNum - 1]);

                    yield break;
                }

            }


            //실질적인 위치 연산 후 변경
            if(userNum == 0)
            {
                MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x - tempAppearSpeed * Time.deltaTime, MyBoardAreaTransformList[targetNum].localPosition.y, MyBoardAreaTransformList[targetNum].localPosition.z);
                MyBoardAreaTransformList[targetNum].localEulerAngles = new Vector3(0, 0, MyBoardAreaTransformList[targetNum].localEulerAngles.z + tempAppearRotationSpeed * Time.deltaTime);
            }
            else
            {
                OtherBoardAreaTransformHandList[userNum - 1].OtherBoardAreaInnerTransformList[targetNum].localPosition = new Vector3(OtherBoardAreaTransformHandList[userNum - 1].OtherBoardAreaInnerTransformList[targetNum].localPosition.x, OtherBoardAreaTransformHandList[userNum - 1].OtherBoardAreaInnerTransformList[targetNum].localPosition.y, OtherBoardAreaTransformHandList[userNum - 1].OtherBoardAreaInnerTransformList[targetNum].localPosition.z + tempAppearSpeed_other * Time.deltaTime);
            }

            
            //나타나는 연출
            if (tempTimeValue > appearTiming)
            {
                if (userNum == 0)
                {
                    MyBoardAreaStoneOnHandList[targetNum].SetAllActive(true);
                }
                else
                {
                    OtherBoardAreaTransformHandList[userNum-1].OtherBoardAreaInnerTransformList[targetNum].gameObject.SetActive(true);
                }
            }

            //시간이 넘으면 자동 탈출
            if (tempTimeValue > forceBreakTiming)
            {
                yield break;
            }

            tempTimeValue += Time.deltaTime;

            /*
            tempAppearSpeed *= appearAccel;
            tempAppearRotationSpeed *= appearAccel;
            tempAppearSpeed_other *= appearAccel;
            */
            //Debug.Log(tempTimeValue);

            yield return null;
        }

        yield break; ;
    }
    /*
    IEnumerator StoneAppearCoroutine(int targetNum)
    {

        Transform tempTransform;

        //tempTransform = MyBoardArea_HandList.InnerGameObjectList[appearTargetNum].transform;
        tempTransform = MyBoardArea_HandList.InnerGameObjectList[targetNum].transform;

        float tempOriginX = tempTransform.localPosition.x;

        //잠시 비활성화
        tempTransform.gameObject.SetActive(false);

        //강제로 일정 시간 이후로도 탈출 가능하게 한다.
        float tempTimeValue = 0f;

        //기본적으로 오른쪽으로 이동
        tempTransform.localPosition = new Vector3(tempTransform.localPosition.x + appearDistance, tempTransform.localPosition.y, tempTransform.localPosition.z);
        yield return null;

        while (true)
        {
            if (tempTransform.localPosition.x < tempOriginX)
            {
                tempTransform.localPosition = new Vector3(tempOriginX, tempTransform.localPosition.y, tempTransform.localPosition.z);
                yield break;
            }

            tempTransform.localPosition = new Vector3(tempTransform.localPosition.x - appearSpeed, tempTransform.localPosition.y, tempTransform.localPosition.z);

            //나타나는 연출
            if(tempTimeValue > appearTiming)
            {
                tempTransform.gameObject.SetActive(true);
            }

            //시간이 넘으면 자동 탈출
            if(tempTimeValue > forceBreakTiming)
            {
                yield break;
            }

            tempTimeValue += Time.deltaTime;

            //Debug.Log(tempTimeValue);

            yield return null;
        }
    }
    */

    IEnumerator StoneAppearFromDownCoroutine(int targetNum)
    {

        //보정용 값
        float tempOriginY = MyBoardAreaTransformList[targetNum].localPosition.y;

        //강제로 일정 시간 이후로도 탈출 가능하게 한다.
        float tempTimeValue = 0f;

        //기본적으로 아래쪽으로 이동
        MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x, MyBoardAreaTransformList[targetNum].localPosition.y - appearDistance, MyBoardAreaTransformList[targetNum].localPosition.z);
        yield return null;

        while (true)
        {
            if (MyBoardAreaTransformList[targetNum].localPosition.y > tempOriginY)
            {
                MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x, tempOriginY, MyBoardAreaTransformList[targetNum].localPosition.z);
                yield break;
            }

            MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x, MyBoardAreaTransformList[targetNum].localPosition.y + appearSpeed * Time.deltaTime, MyBoardAreaTransformList[targetNum].localPosition.z);

            //나타나는 연출
            if (tempTimeValue > appearTiming_2)
            {
                MyBoardAreaStoneOnHandList[targetNum].SetAllActive(true);
            }

            //시간이 넘으면 자동 탈출
            if (tempTimeValue > forceBreakTiming)
            {
                yield break;
            }

            tempTimeValue += Time.deltaTime;

            //Debug.Log(tempTimeValue);

            yield return null;
        }

        yield break;
    }

    IEnumerator StoneDisappearToDownCoroutine(int targetNum)
    {
        //보정용 값
        float tempOriginY = MyBoardAreaTransformList[targetNum].localPosition.y;

        //강제로 일정 시간 이후로도 탈출 가능하게 한다.
        float tempTimeValue = 0f;

        //기본적으로 아래쪽으로 이동
        MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x, MyBoardAreaTransformList[targetNum].localPosition.y - appearDistance, MyBoardAreaTransformList[targetNum].localPosition.z);
        yield return null;

        while (true)
        {
            MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x, MyBoardAreaTransformList[targetNum].localPosition.y - appearSpeed * Time.deltaTime, MyBoardAreaTransformList[targetNum].localPosition.z);

            //나타나는 연출
            if (tempTimeValue > appearTiming_3)
            {
                MyBoardAreaStoneOnHandList[targetNum].SetAllActive(false);
                MyBoardAreaTransformList[targetNum].localPosition = new Vector3(MyBoardAreaTransformList[targetNum].localPosition.x, tempOriginY, MyBoardAreaTransformList[targetNum].localPosition.z);
                yield break;
            }

            tempTimeValue += Time.deltaTime;

            yield return null;
        }

        yield break;
    }


    public void StoneMovableAllFunction(bool isMovable)
    {
        for(int i=0; i<10; i++)
        {
            MyBoardAreaStoneOnHandList[i].isMovementAvailable = isMovable;
        }
    }
}
