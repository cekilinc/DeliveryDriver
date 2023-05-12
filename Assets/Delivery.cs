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
        [SerializeField] GameObject allCustomers;

        List<GameObject> packageList;
        List<GameObject> customerList;

        SpriteRenderer vehicleSpriteRenderer;

        int currentIndex;
        int currentCustomer;

        int nextIndex;
        int nextCustomer;

        List<int> usedPackages = new List<int>();

        ///<summary>
        ///This method takes a parent, creates a list of its childs and activates a random one
        ///</summary>
        ///<param name="desiredList">List to be filled with childs</param>
        ///<param name="parent">Parent from which list will be created</param>
        ///<param name="currentActive">Integer var to which activated index will be assigned</param>
        public void CreateInactiveListFromParentActivateOne (ref List<GameObject> desiredList,GameObject parent,ref int currentActive)
        {
            foreach (Transform child in parent.transform)
            {
                desiredList.Add(child.gameObject);
            }
            for (int i = 0; i < desiredList.Count; i++)
            {
                desiredList[i].SetActive(false);
            }
            currentActive = Random.Range(0,desiredList.Count);
            desiredList[currentActive].SetActive(true);
        }

        void Start() 
        {
            vehicleSpriteRenderer = GetComponent<SpriteRenderer>();

            packageList = new List<GameObject>();
            customerList = new List<GameObject>();

            CreateInactiveListFromParentActivateOne(ref packageList,allPackages,ref currentIndex);
            CreateInactiveListFromParentActivateOne(ref customerList,allCustomers,ref currentCustomer);

            /* foreach (Transform package in allPackages.transform)
            {
                packageList.Add(package.gameObject);
            }
            for (int i = 0; i < packageList.Count; i++)
            {
                packageList[i].SetActive(false);
            } */
            
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
                do
                {
                    nextCustomer = Random.Range(0,customerList.Count);
                } while (nextCustomer == currentCustomer);
                
                if (usedPackages.Count < packageList.Count)
                {
                    packageList[nextIndex].SetActive(true);
                    customerList[currentCustomer].SetActive(false);
                    customerList[nextCustomer].SetActive(true);
                    currentCustomer = nextCustomer;
                }
                usedPackages.Add(nextIndex);
                currentIndex = nextIndex;
            }
        }
}
