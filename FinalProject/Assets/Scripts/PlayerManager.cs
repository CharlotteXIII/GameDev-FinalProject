using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public static bool IsAnyDragging = false; //Check Drag

    private int Army_No;
    [SerializeField] private TextMeshPro Army_No_txt;
    // [SerializeField] private TMP_Text Army_No_txt; // TextMeshPro และ TextMeshProUGUI

    private IEnumerator Ie_fill_army , IeGenerateSoldier;

    private Vector3 offset , intialPos;
    [SerializeField] private float nearclip;
    public bool Drag;
    private Camera cam;

    public Transform enemy;


    void Start()
    {
       Instance = this; 
       Ie_fill_army = FillTheArmy();
       StartCoroutine(Ie_fill_army);

       cam = Camera.main;
       intialPos = transform.localPosition; 
    }

    private IEnumerator FillTheArmy()
    {
        int counter = 0;
        Army_No = 0;

        while(counter < 20)
        {
            counter++;
            Army_No_txt.text = counter.ToString();
            Army_No = counter;
            
            yield return new WaitForSecondsRealtime(0.5f);
        }

    }

    void Update() 
    {
        Vector3 currentMousePos = Input.mousePosition;

        if (float.IsInfinity(currentMousePos.x) || float.IsInfinity(currentMousePos.y))
        {
            return; 
        }
        Vector3 MousePos = cam.ScreenToWorldPoint(new Vector3(currentMousePos.x, currentMousePos.y, nearclip));
        
        //Check Drag
        if(Input.GetMouseButtonDown(0) && !IsAnyDragging)

        // if(Input.GetMouseButtonDown(0))
        {

            Ray ray = cam.ScreenPointToRay(currentMousePos);

            if(Physics.Raycast(ray, out var hit))
            {
                if(hit.collider != null && hit.collider.CompareTag("pointer"))
                // if(hit.transform == transform)
                // if(hit.collider.CompareTag("pointer") && hit.transform.parent == transform)
                {
                    offset = transform.position - MousePos;
                    Drag = true;
                    IsAnyDragging = true; //Check Drag
                }
            }
        }

        if(Drag)
        {
            transform.position = offset + MousePos;
        }

        if(Input.GetMouseButtonUp(0) && Drag)
        // if(Input.GetMouseButtonUp(0))
        {
            Drag = false;
            IsAnyDragging = false; //Check Drag
            transform.localPosition = intialPos; //Pointer back to first location
            
            if(enemy !=null)
            {
                IeGenerateSoldier = GenerateSoldier();
                StartCoroutine(IeGenerateSoldier);

                Ie_fill_army = FillTheArmy();
                StartCoroutine(Ie_fill_army);
            }        
        }

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("enemy"))
        {
            enemy = other.transform;
        }
        
    }

    public List<GameObject> group = new List<GameObject>();
    [SerializeField] float[] angles;
    [SerializeField] private Attack soldierPrefab;
    [SerializeField] private float SpreadTime;

    private IEnumerator GenerateSoldier()
    {
        var sildierNo = 0;
        var PlayerFakeSoldierNo = Army_No;
        var maxSoldierPerBatch = 3;

        while (sildierNo < PlayerFakeSoldierNo)
        {
            var soldierToGenerate = Mathf.Min(maxSoldierPerBatch , PlayerFakeSoldierNo -sildierNo);

            for(int i =0; i < soldierToGenerate; i++)
            {
                var newSoldier = Instantiate(soldierPrefab , transform.position , Quaternion.identity);
                newSoldier.SetupSoldier("Player1", new Color32(88, 255, 0, 255));
                group.Add(newSoldier.gameObject);
                newSoldier.ExecuteOrder(enemy.transform.position , angles[i]);
            }

            sildierNo += soldierToGenerate;
            Army_No = 0;
            Army_No_txt.text = Army_No.ToString();
            yield return new WaitForSecondsRealtime(SpreadTime);
        }

      
    }
}
