using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    public Material mat;

    Vector3[] vertices;
    int[] triangles;

    float width = 1;
    float height = 1;

    // Use this for initialization
    void Start()
    {
        MakeMeshData();
    }

    void MakeMeshData() {
        Mesh mesh = new Mesh();

        vertices = new Vector3[4];

        vertices[0] = new Vector3(-width, -height);
        vertices[1] = new Vector3(-width, height);
        vertices[2] = new Vector3(width, height);
        vertices[3] = new Vector3(width, -height);

        mesh.vertices = vertices;
        mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 };
        mesh.RecalculateNormals();

        GetComponent<MeshRenderer>().material = mat;

        GetComponent<MeshFilter>().mesh = mesh;

    }

}
