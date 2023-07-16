using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class NodeLinkData
{
    //시작 노드
    public string baseNodeGuid;
    //포트 자체 이름
    public string portName;
    //끝 노드
    public string targetNodeGuid;

}
