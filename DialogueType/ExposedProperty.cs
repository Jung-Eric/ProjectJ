using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//black board에 사용할 데이터 양식
//추가 데이터 양식 설정은 여기서 한다.
[System.Serializable]
public class ExposedProperty
{
    public static ExposedProperty CreateInstance()
    {
        return new ExposedProperty();
    }

    public string propertyName = "New String";
    public string propertyValue = "New Value";

    
}

[System.Serializable]
public class CommentBlockData
{
    public List<string> ChildNodes = new List<string>();
    public Vector2 position;
    public string Title = "Comment Block";
}