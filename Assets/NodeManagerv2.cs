using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManagerv2 : MonoBehaviour
{
    public int pooledAmount = 25;
    public List<GameObject> pooledNodesv2;
    public GameObject NodePrefabv2;
    public bool willGrow = false;
    public static NodeManagerv2 current;

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
        Vector3 InstPos = transform.position;

        pooledNodesv2 = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            InstPos = new Vector3(InstPos.x += 0.05f, InstPos.y += 0.05f, InstPos.z += 0.05f);

            GameObject objN = (GameObject)Instantiate(NodePrefabv2,InstPos,Quaternion.identity);
            objN.SetActive(true);           
            pooledNodesv2.Add(objN);
        }
        yield return null;

        for (int i = 0; i < pooledNodesv2.Count; i++)
        {
            pooledNodesv2[i].GetComponent<OrbitMovementv2>().objectsToAvoid = pooledNodesv2;
        }
        StopBuilder();
    }

    public GameObject GetPooledNode()
    {
        for (int i = 0; i < pooledNodesv2.Count; i++)
        {
            if (!pooledNodesv2[i].activeInHierarchy)
            {
                return pooledNodesv2[i];
            }
        }

        if (willGrow)
        {
            GameObject objEN = (GameObject)Instantiate(NodePrefabv2, transform.position, Quaternion.identity);
            pooledNodesv2.Add(objEN);
            return objEN;
        }
        return null;
    }

}
