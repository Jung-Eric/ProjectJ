using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpController : MonoBehaviour
{
    //복제 대상이 되는 GameObject
    public GameObject baseTargetObject;
    public Transform parentTransform;

    //현재 Transform대상
    public List<GameObject> TargetGameObjectList;

    public float positionXMin = 110f;
    public float positionXMax = 170f;

    public float InitialPositionY = 810f;
    public float intervalY = 220f;


    //public float[] positionX = new 
    public float[] positionZ = new float[4] {281, 330, 460, 2000};

    [Space(10)]
    public float rotationXRange = 30f;
    public float rotationYRange = 15f;
    public float rotationZRange = 12f;

    private void Start()
    {
        InitialCreate();
    }

    public void InitialCreate()
    {

        for(int i=0; i<3; i++)
        {
            for(int j=0; j<2; j++)
            {
                GameObject tempGameObject = Instantiate(baseTargetObject, parentTransform);

                float tempXFloat = Random.Range(positionXMin, positionXMax);

                float tempXRot = Random.Range(-rotationXRange, rotationXRange);
                float tempYRot = Random.Range(-rotationYRange, rotationYRange);
                float tempZRot = Random.Range(-rotationZRange, rotationZRange);

                if (j == 0)
                {
                    tempGameObject.transform.localPosition = new Vector3(-tempXFloat, InitialPositionY - (intervalY * ((2 * i) + j)), positionZ[i]);
                }
                else
                {
                    tempGameObject.transform.localPosition = new Vector3(tempXFloat, InitialPositionY - (intervalY * ((2 * i) + j)), positionZ[i]);
                }

                tempGameObject.transform.localEulerAngles = new Vector3(tempXRot, tempYRot, tempZRot);


                TargetGameObjectList.Add(tempGameObject);
            }
            
        }

    }

    //마지막 2개 버리고 새롭게 2개 생성
    public void StartCreate2AndDestroy2()
    {

        StartCoroutine(Create2AndDestroy2());

    }

    IEnumerator Create2AndDestroy2()
    {
        for(int i=1; i >=0; i--)
        {
            GameObject tempGameObject = Instantiate(baseTargetObject, parentTransform);

            float tempXFloat = Random.Range(positionXMin, positionXMax);

            float tempXRot = Random.Range(-rotationXRange, rotationXRange);
            float tempYRot = Random.Range(-rotationYRange, rotationYRange);
            float tempZRot = Random.Range(-rotationZRange, rotationZRange);

            if (i == 0)
            {
                tempGameObject.transform.localPosition = new Vector3(-tempXFloat, InitialPositionY , positionZ[0]);
            }
            else
            {
                tempGameObject.transform.localPosition = new Vector3(tempXFloat, InitialPositionY - (intervalY), positionZ[0]);
            }

            tempGameObject.transform.localEulerAngles = new Vector3(tempXRot, tempYRot, tempZRot);

            //맨 앞에 2개를 넣는다.
            TargetGameObjectList.Insert(0, tempGameObject);
        }

        yield return null;

        //이동 대상이 되는 거리를 만든다.
        List<Vector3> TempTargetPostion = new List<Vector3>();

        for(int i=2; i<8; i++)
        {
            int tempNum = ((i - 2) >> 1) + 1;

            TempTargetPostion.Add(new Vector3(TargetGameObjectList[i].transform.localPosition.x, TargetGameObjectList[i].transform.localPosition.y - (2 * intervalY), positionZ[tempNum]));

        }

        yield return null;

        List<Transform> TempTransformList = new List<Transform>();

        for(int i=2; i<8; i++)
        {
            TempTransformList.Add(TargetGameObjectList[i].transform);
        }

        yield return null;

        //float tempMovingY = 0;
        float tempTimer = 0f;

        //이동시긴다.
        while (true)
        {
            if(tempTimer > 2)
            {
                for (int i = 0; i < 6; i++)
                {

                    TempTransformList[i].localPosition = TempTargetPostion[i];

                }

                break;
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {

                    TempTransformList[i].localPosition = Vector3.Lerp(TempTargetPostion[i], TempTransformList[i].localPosition, 0.987f);

                }

                for(int i = 4; i < 6; i++)
                {
                    TempTransformList[i].rotation = Quaternion.Lerp(Quaternion.Euler(-90, 0, 0), TempTransformList[i].rotation, 0.99f);
                }
            }

            tempTimer += Time.deltaTime;

            yield return null;
        }

        
        //최종적으로 제거된다.
        Destroy(TargetGameObjectList[6]);
        TargetGameObjectList.RemoveAt(6);

        Destroy(TargetGameObjectList[6]);
        TargetGameObjectList.RemoveAt(6);
        


        yield break;
    }


}
