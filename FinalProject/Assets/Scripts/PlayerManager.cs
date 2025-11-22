using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private int Army_No;
    [SerializeField] private TextMeshPro Army_No_txt;
    private IEnumerator Ie_fill_army;

    private Vector3 offset , intialPos;
    [SerializeField] private float nearclip;
    public bool Drag;
    private Camera cam;

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

        while(counter < 10)
        {
            counter++;
            Army_No_txt.text = counter.ToString();
            Army_No = counter;

            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    // void Update() 
    // {
    //     Vector3 MousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, nearclip));

    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         Ray ray = cam.ScreenPointToRay(Input.mousePosition);

    //         if(Physics.Raycast(ray, out var hit))
    //         {
    //             if(hit.collider != null && hit.collider.CompareTag("pointer"))
    //             {
    //                 offset = transform.position - MousePos;
    //                 Drag = true;
    //             }
    //         }
    //     }

    //     if(Drag)
    //     {
    //         transform.position = offset + MousePos;
    //     }

    //     if(Input.GetMouseButtonUp(0))
    //     {
    //         Drag = false;
    //         transform.localPosition = intialPos; //Pointer back to first location
    //     }

    // }
    void Update() 
    {
        // 1. เก็บค่าเมาส์ใส่ตัวแปรไว้ก่อน
        Vector3 currentMousePos = Input.mousePosition;

        // 2. [จุดที่เพิ่ม] เช็กว่าค่าเมาส์เพี้ยน (Infinity) หรือไม่
        // ถ้าเพี้ยน ให้หยุดทำงานในรอบนี้ทันที เพื่อกัน Error
        if (float.IsInfinity(currentMousePos.x) || float.IsInfinity(currentMousePos.y))
        {
            return; 
        }

        // 3. [จุดที่แก้] เปลี่ยนจาก Input.mousePosition มาใช้ตัวแปร currentMousePos ที่เช็กแล้ว
        Vector3 MousePos = cam.ScreenToWorldPoint(new Vector3(currentMousePos.x, currentMousePos.y, nearclip));

        if(Input.GetMouseButtonDown(0))
        {
            // [จุดที่แก้] ใช้ตัวแปร currentMousePos แทน
            Ray ray = cam.ScreenPointToRay(currentMousePos);

            if(Physics.Raycast(ray, out var hit))
            {
                if(hit.collider != null && hit.collider.CompareTag("pointer"))
                {
                    offset = transform.position - MousePos;
                    Drag = true;
                }
            }
        }

        if(Drag)
        {
            transform.position = offset + MousePos;
        }

        if(Input.GetMouseButtonUp(0))
        {
            Drag = false;
            transform.localPosition = intialPos; //Pointer back to first location
        }

    }


}
