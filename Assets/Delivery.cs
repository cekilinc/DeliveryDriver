using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
        bool hasPackage;
        [SerializeField] float destroyDelay = 0.2f;

        [SerializeField] Color32 hasPackageColor = new Color32(1,1,1,1);
        [SerializeField] Color32 noPackageColor = new Color32(1,1,1,1);

        //[SerializeField] List<GameObject> packageList = new List<GameObject>();
        [SerializeField] GameObject allPackages;

        List<GameObject> packageList;

        SpriteRenderer vehicleSpriteRenderer;

        int currentIndex;

        int nextIndex;

        List<int> usedPackages = new List<int>();


        void Start() 
        {
            vehicleSpriteRenderer = GetComponent<SpriteRenderer>();

            packageList = new List<GameObject>();

            foreach (Transform package in allPackages.transform)
            {
                packageList.Add(package.gameObject);
            }
            for (int i = 0; i < packageList.Count; i++)
            {
                packageList[i].SetActive(false);
            }
            
            currentIndex = Random.Range(0, packageList.Count);
            packageList[currentIndex].SetActive(true);
            usedPackages.Add(currentIndex);
            
        }

        void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.tag == "Package" && !hasPackage)
            {
                /*
                foreach (GameObject package in packageList)
                {
                    package.SetActive(false);
                }
                */
                /*
                for (int i = 0; i < packageList.Count; i++)
                {
                    packageList[i].SetActive(false);
                }
                */
                Debug.Log("Package picked up.");
                hasPackage = true;
                vehicleSpriteRenderer.color = hasPackageColor;
                //Destroy(other.gameObject,destroyDelay);
                do
                {
                        nextIndex = Random.Range(0,packageList.Count);
                }
                while (usedPackages.Contains(nextIndex) && usedPackages.Count < packageList.Count);

                if (usedPackages.Count == packageList.Count-1)
                    {
                        SpriteRenderer packageSpriteRenderer = packageList[nextIndex].GetComponent<SpriteRenderer>();
                        packageSpriteRenderer.color = Color.red;
                    }
                if (usedPackages.Count == packageList.Count)
                {
                    vehicleSpriteRenderer.color = Color.red;
                }
                packageList[currentIndex].SetActive(false);
            }
            else if (other.tag == "Customer" && hasPackage)
            {
                Debug.Log(packageList.Count.ToString());
                Debug.Log("Package delivered.");
                hasPackage = false;
                vehicleSpriteRenderer.color = noPackageColor;
                if (usedPackages.Count < packageList.Count)
                {
                    packageList[nextIndex].SetActive(true);
                }
                usedPackages.Add(nextIndex);
                currentIndex = nextIndex;
            }
        }
}
