using NUnit.Framework.Internal;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject projectilePrefab;

    private int testTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        InstantiateArrow(OVRInput.Controller.LTouch);
        InstantiateArrow(OVRInput.Controller.RTouch);

        /*
        if (testTime % 20 == 0)
        {
            Instantiate(projectilePrefab, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch), OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch));
        }
        testTime++;*/
    }

    void InstantiateArrow(OVRInput.Controller controller)
    {
        Vector3 posToSpawn = OVRInput.GetLocalControllerPosition(controller) + new Vector3(0, 0.15f, 0);
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            Instantiate(projectilePrefab, posToSpawn, OVRInput.GetLocalControllerRotation(controller));
        }
    }
}
