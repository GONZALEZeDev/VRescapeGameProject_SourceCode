using UnityEngine;

public class ManagerDoor : MonoBehaviour
{
    public Door[] Door; 
    //c'est un singleton

    public static ManagerDoor instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }




    void Update()
    {
        
    }
}
