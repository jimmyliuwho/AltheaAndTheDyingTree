using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowVinesCollision : MonoBehaviour
{
    public List<MeshRenderer> growVinesMeshes;
    public float timeToGrow = 5;
    public float refreshRate = 0.05f;
    [Range(0,1)]
    public float minGrow = 0.2f;
    [Range(0,1)]
    public float maxGrow = 0.97f;

    private List<Material> growVinesMaterials = new List<Material>();
    private bool fullyGrown;

    Collider stemCollider;
    public Collider platformCollider;

    private bool needsToBeGrown;
    
    // Start is called before the first frame update
    void Start()
    {
        needsToBeGrown = true;

        stemCollider = this.GetComponent<Collider>();
        //platformCollider = GetComponent<Collider>();
        platformCollider.enabled = false;
        for(int i = 0; i<growVinesMeshes.Count; i++) {
            for(int j = 0; j<growVinesMeshes[i].materials.Length; j++) {
                if(growVinesMeshes[i].materials[j].HasProperty("Grow_")) 
                {
                    growVinesMeshes[i].materials[j].SetFloat("Grow_", minGrow);
                    growVinesMaterials.Add(growVinesMeshes[i].materials[j]);
                }
            }
        }

        if(LevelSwitcher.levelNum == 2)
        {
            //Debug.Log("Hit");
            platformCollider.enabled = !platformCollider.enabled;
            //Debug.Log("platform collider is: " + platformCollider.enabled);
            for (int i = 0; i < growVinesMaterials.Count; i++)
            {
                StartCoroutine(GrowVines(growVinesMaterials[i]));
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     for(int i = 0; i < growVinesMaterials.Count; i++) {
        //         StartCoroutine(GrowVines(growVinesMaterials[i]));
        //     }
        // }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (needsToBeGrown && col.gameObject.tag == "LightPower" && col.gameObject.layer == 9) {
            needsToBeGrown = false;
            Debug.Log("Vine Hit"); 
            platformCollider.enabled = !platformCollider.enabled;
            //Debug.Log("platform collider is: " + platformCollider.enabled);
            for(int i = 0; i < growVinesMaterials.Count; i++) 
            {
                StartCoroutine(GrowVines(growVinesMaterials[i]));
            }
        }
        
        
    }

    IEnumerator GrowVines (Material mat) {
        float growValue = mat.GetFloat("Grow_");
        if (!fullyGrown) 
        {
            while(growValue < maxGrow)
            {
                growValue += 1 / (timeToGrow / refreshRate);
                mat.SetFloat("Grow_", growValue);

                yield return new WaitForSeconds(refreshRate);
            }
        }
        else {
            while(growValue > minGrow)
            {
                growValue -= 1 / (timeToGrow / refreshRate);
                mat.SetFloat("Grow_", growValue);

                yield return new WaitForSeconds(refreshRate);
            }

        }

        if(growValue >= maxGrow)
        {
            fullyGrown = true;
            platformCollider.enabled = true;
            //Debug.Log("platform collider is: " + platformCollider.enabled);
        }
        else 
        {
            fullyGrown = false;
            platformCollider.enabled = false;
            //Debug.Log("platform collider is: " + platformCollider.enabled);
        }
    }
}
