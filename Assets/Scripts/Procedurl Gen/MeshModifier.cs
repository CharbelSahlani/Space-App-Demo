using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshModifier : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(1.5f, 5f)]
    [SerializeField] float radius = 3f;
    [Range(1.5f, 5f)]
    [SerializeField] float deformation_strenght = 2f;
    private Mesh mesh;
    private Vector3[] vertices, modified_vertices;
    void Start()
    {
        mesh = GetComponentInChildren<MeshFilter>().mesh;
        vertices = mesh.vertices;
        modified_vertices = mesh.vertices;
    }
    //recalculate mesh based on new deforming vertices 
   void recalculate_mesh()
    {
        mesh.vertices = modified_vertices;
        GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            for(int v = 0; v<modified_vertices.Length; v++)
            {
                Vector3 distance = modified_vertices[v] - hit.point;
                float smoothing_factor = 2f;
                float force = deformation_strenght / (1f + hit.point.sqrMagnitude);
                //comapre distances
                if(distance.sqrMagnitude < radius)
                {
                    if (Input.GetMouseButton(0))
                    {
                        modified_vertices[v] = modified_vertices[v] + (Vector3.up * force) / smoothing_factor;
                    }else if (Input.GetMouseButton(1)){
                        modified_vertices[v] = modified_vertices[v] + (Vector3.down * force) / smoothing_factor;

                    }
                }
            }
        }
        recalculate_mesh();
    }
}
