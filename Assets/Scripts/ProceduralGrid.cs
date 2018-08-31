using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour {


    List<GameObject> dots;
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public Sprite SpritePrefab;

    public float cellSize = 1;
    int gridSize;
    public float bottomLeftX;
    public float bottomLeftY;
    public float bottomRightX;
    public float bottomRightY;

    public float topLeftX;
    public float topLeftY;
    public float topRightX;
    public float topRightY;

    // Use this for initialization
    void Awake () {
        mesh = GetComponent<MeshFilter>().mesh;
        dots = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Start () {
        MakeProceduralGrid();
    }

    void Update()
    {
        UpdateMesh();
    }

    void MakeProceduralGrid()
    {
        vertices = CreateVertices();
        
        foreach(Vector3 vertice in vertices)
        {
            dots.Add(CreateDot(vertice));
        }
    }

    private GameObject CreateDot(Vector3 vector)
    {

        GameObject dot = new GameObject("Dot");
        SpriteRenderer renderer = dot.AddComponent<SpriteRenderer>();
        renderer.sprite = SpritePrefab;

        dot.transform.SetParent(gameObject.transform);
        dot.transform.localScale = new Vector3(2f, 2f, 1f);
        renderer.material.color = new Color(1, 0.5f, 0);
        renderer.sortingOrder = 1;
        
        dot.transform.localPosition = vector;

        return dot;
    }

    private Vector3[] CreateVertices()
    {

        //set array sizes
        Vector3[] vertices = new Vector3[4];
        triangles = new int[6];

        //Set vertex offset
        float vertrexOffset = cellSize * 0.5f;

        //Populatie vertices and triangle arrays
        vertices[0] = new Vector3(-vertrexOffset, -vertrexOffset);
        vertices[1] = new Vector3(-vertrexOffset, vertrexOffset);
        vertices[2] = new Vector3(vertrexOffset, -vertrexOffset);
        vertices[3] = new Vector3(vertrexOffset, vertrexOffset);

        triangles[0] = 0;
        triangles[1] = triangles[4] = 1;
        triangles[2] = triangles[3] = 2;
        triangles[5] = 3;

        return vertices;
    }

    void UpdateMesh() {

        //Set vertex offset
        float vertrexOffset = cellSize * 0.5f;

        //Populatie vertices and triangle arrays
        vertices[0] = new Vector3(-vertrexOffset - bottomLeftX, -vertrexOffset - bottomLeftY);
        vertices[1] = new Vector3(-vertrexOffset - topLeftX, vertrexOffset + topLeftY);
        vertices[2] = new Vector3(vertrexOffset + bottomRightX, -vertrexOffset - bottomRightY);
        vertices[3] = new Vector3(vertrexOffset + topRightX, vertrexOffset + topRightY);

        triangles[0] = 0;
        triangles[1] = triangles[4] = 1;
        triangles[2] = triangles[3] = 2;
        triangles[5] = 3;

        UpdatePosition();

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
     
    }

    private void UpdatePosition()
    {
        if(dots != null && dots.Count > 0)
        {
            int x = 0;
            foreach (Vector3 vector in vertices)
            {
                dots[x].transform.localPosition = vector;
                x++;
            }
        }        
    }
}
