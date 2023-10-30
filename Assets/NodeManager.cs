using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public int pooledAmount = 25;
    public List<GameObject> pooledNodes;
    public GameObject NodePrefab;
    public bool willGrow = false;
    public static NodeManager current;
    IEnumerator BuildIEnum;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        RunBuilder();
    }

    void RunBuilder()
    {
        if (BuildIEnum != null)
        {
            StopCoroutine(BuildIEnum);
        }
        BuildIEnum = BuildI();
        StartCoroutine(BuildIEnum);
    }

    void StopBuilder()
    {
        StopCoroutine(BuildIEnum);
    }

    IEnumerator BuildI()
    {
        pooledNodes = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject objN = (GameObject)Instantiate(NodePrefab,transform.position,Quaternion.identity);
            objN.SetActive(true);
            objN.GetComponent<OrbitMovement>().sphereRadius = Random.Range(1.0f,2.0f);
            objN.GetComponent<OrbitMovement>().moveSpeed = Random.Range(1f,2f);            
            pooledNodes.Add(objN);
        }
        yield return null;

        for (int i = 0; i < pooledNodes.Count; i++)
        {
            pooledNodes[i].GetComponent<OrbitMovement>().objectsToAvoid = pooledNodes;
            pooledNodes[i].GetComponent<OrbitMovement>().objectsToAvoid.Remove(pooledNodes[i]);
        }
        StopBuilder();
    }

    public GameObject GetPooledNode()
    {
        for (int i = 0; i < pooledNodes.Count; i++)
        {
            if (!pooledNodes[i].activeInHierarchy)
            {
                return pooledNodes[i];
            }
        }

        if (willGrow)
        {
            GameObject objEN = (GameObject)Instantiate(NodePrefab, transform.position, Quaternion.identity);
            pooledNodes.Add(objEN);
            return objEN;
        }
        return null;
    }

}
