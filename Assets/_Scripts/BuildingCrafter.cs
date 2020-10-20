using UnityEngine;
using System.Collections;
public class BuildingCrafter : MonoBehaviour
{
    [Header("Set in Inspector")] public int numBuilds = 5; // The # of buildings to make

    public GameObject buildPrefab;
    
    public Vector3 buildPosMin = new Vector3(-20, -10, 10);
    public Vector3 buildPosMax = new Vector3(50, -10, 10);

    public float buildScaleMin = 1;
    public float buildScaleMax = 3;
    public float buildSpeedMult = 0.5f;

    private GameObject[] buildInstances;
    void Awake()
    {
        buildInstances = new GameObject[numBuilds];
        
        GameObject anchor = GameObject.Find("BuildAnchor");
        GameObject build;

        for (int i = 0; i < numBuilds; i++)
        {
            build = Instantiate<GameObject>(buildPrefab);

            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(buildPosMin.x, buildPosMax.x);
            cPos.y = -10;
            
            float scaleU = Random.value;
            cPos.z = 100 - 90;

            build.transform.position = cPos;
            build.transform.SetParent(anchor.transform);
            buildInstances[i] = build;
        }
    }
    
    void Update()
    {
        foreach (GameObject build in buildInstances)
        {
            Vector3 cPos = build.transform.position;
            cPos.x -= Time.deltaTime * buildSpeedMult;
            
            if (cPos.x <= buildPosMin.x)
            {
                cPos.x = buildPosMax.x;
            }
            build.transform.position = cPos;
        }
    }
}