using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ArrowGenerator : MonoBehaviour
{
    public float stemLength;
    public float stemWidth;
    public float tipLength;
    public float tipWidth;
    
    [System.NonSerialized]
    public List<Vector3> verticesList;
    [System.NonSerialized]
    public List<int> trianglesList;

    Mesh mesh;

    //added new varible
    private Camera cam;
    private Renderer arrowRend;
    
    void Start()
    {
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;

        cam = Camera.main;
        arrowRend = GetComponent<Renderer>();

        arrowRend.sortingOrder = 5;
    }

    void Update()
    {
        if(PlayerManager.Instance.Drag)
        {
          GenerateArrow();

          var dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);

          var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
          transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward);//rotate arrow

          var offset = PlayerManager.Instance.transform.position - transform.position;

          stemLength = offset.magnitude;

          arrowRend.enabled = true;
        }
        else//mouse up
        {
           stemLength = 0f;
           arrowRend.enabled = false;
        }   
    }

    void GenerateArrow()
    {
        //setup
        verticesList = new List<Vector3>();
        trianglesList = new List<int>();

        //stem setup
        Vector3 stemOrigin = Vector3.zero;
        float stemHalfWidth = stemWidth/2f;

        //Stem points
        verticesList.Add(stemOrigin+(stemHalfWidth*Vector3.down));
        verticesList.Add(stemOrigin+(stemHalfWidth*Vector3.up));
        verticesList.Add(verticesList[0]+(stemLength*Vector3.right));
        verticesList.Add(verticesList[1]+(stemLength*Vector3.right));
 
        //Stem triangles
        trianglesList.Add(0);
        trianglesList.Add(1);
        trianglesList.Add(3);
 
        trianglesList.Add(0);
        trianglesList.Add(3);
        trianglesList.Add(2);
        
        //tip setup
        Vector3 tipOrigin = stemLength*Vector3.right;
        float tipHalfWidth = tipWidth/2;
 
        //tip points
        verticesList.Add(tipOrigin+(tipHalfWidth*Vector3.up));
        verticesList.Add(tipOrigin+(tipHalfWidth*Vector3.down));
        verticesList.Add(tipOrigin+(tipLength*Vector3.right));
 
        //tip triangle
        trianglesList.Add(4);
        trianglesList.Add(6);
        trianglesList.Add(5);
 
        //assign lists to mesh.
        mesh.vertices = verticesList.ToArray();
        mesh.triangles = trianglesList.ToArray();
    }
}
