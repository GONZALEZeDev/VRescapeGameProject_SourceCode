using UnityEngine;
public enum TypeObject
{
    cube,
    pyramide,
    sphere
}

public class objetSpecial : MonoBehaviour
{
    public TypeObject typeObject;


    public TypeObject getTypeObject()
    {
        return typeObject;
    }
}
