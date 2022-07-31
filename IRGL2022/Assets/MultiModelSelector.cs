using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiModelSelector : MonoBehaviour
{
    [SerializeField] GameObject[] models = new GameObject[0];
    [SerializeField] Material[] materials = new Material[0];
    public int currentActiveModel = 0;
    public int currentActiveMaterial = 0;

    void Start()
    {

        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].activeSelf)
                models[i].SetActive(false);
        }

        ActivateModelAt(Random.Range(0, models.Length));
        ActivateMaterialAt(Random.Range(0, materials.Length));
    }

    public void ActivateModelAt(int modelIndex)
    {
        models[currentActiveModel].SetActive(false);

        currentActiveModel = modelIndex;
        models[modelIndex].SetActive(true);
    }

    public void ActivateMaterialAt(int materialIndex)
    {
        models[currentActiveModel].GetComponent<SkinnedMeshRenderer>().material = materials[currentActiveMaterial];
        currentActiveMaterial = materialIndex;
    }

    private int RandomIndexModel()
    {
        int index = Random.Range(0, models.Length);
        if (index == currentActiveModel && models.Length > 1)//dont create infinite loop
            return RandomIndexModel();
        return index;
    }

    private int RandomIndexMaterial()
    {
        int index = Random.Range(0, materials.Length);
        if (index == currentActiveMaterial && materials.Length > 1)//dont create infinite loop
            return RandomIndexMaterial();
        return index;
    }
}
