using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class terrainCreator : MonoBehaviour
{

	Mesh mesh;
	List<Vector3> meshPos = new List<Vector3> ();
	float highestYvalue, lowestYvalue;
	bool isVertice;
	public bool lastVertice;

	void Start ()
	{
		gameObject.AddComponent ("MeshFilter");
		gameObject.AddComponent ("MeshRenderer");
		mesh = GetComponent<MeshFilter> ().mesh;
		mesh.Clear ();
	}
		
	// Update is called once per frame
	void Update ()
	{
//		Unity's document code:
//		gameObject.AddComponent("MeshFilter");
//		gameObject.AddComponent("MeshRenderer");
//		Mesh mesh = GetComponent<MeshFilter>().mesh;
//		mesh.Clear();
//		mesh.vertices = new Vector3[] {new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)};
//		mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
//		mesh.triangles = new int[] {0, 1, 2};

		mouseCreateVertices ();
	
		if (isVertice == true)
			for (int i = 0; i < meshPos.Count; i++) { 

				int[] triangles = new int[10];

				mesh.name = "custom_terrain";
				mesh.vertices = meshPos.ToArray ();

				Vector2[] uvArray = new Vector2[3];
				uvArray [0] = new Vector2 (0, 0);
				uvArray [1] = new Vector2 (0, 1);
				uvArray [2] = new Vector2 (1, 1);
				uvArray [3] = new Vector2 (1, 0);

				mesh.uv = uvArray;
				int[] triangleArr = new int[3] {0, 1, 2};
				mesh.triangles = triangleArr;

	


				if (lastVertice == true) {

					float distance = highestYvalue - lowestYvalue;
						
					Vector3 firstVertice = meshPos [0];
					Vector3 endVertice = meshPos [meshPos.Count];

					float heightDeduction = endVertice.y - distance - 10;

					Vector3 bottomRightVertice;
					bottomRightVertice.x = endVertice.x;
					bottomRightVertice.y = heightDeduction;
					bottomRightVertice.z = 0;

					Vector3 bottomLeftVertice;
					bottomLeftVertice.x = firstVertice.x;
					bottomLeftVertice.y = heightDeduction;
					bottomLeftVertice.z = 0;

					Vector3[] pos = new Vector3[1];
					pos [0] = bottomRightVertice;
					pos [1] = bottomLeftVertice;

					createManySpheres (pos);
				}
				mesh.RecalculateNormals ();
			}
	}

	void mouseCreateVertices ()
	{
		Vector3 mousePos;
		Camera cam = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.camera;

		if (Input.GetMouseButtonDown (0)) {  //if left click

			isVertice = true;
			mousePos = Input.mousePosition;
			Vector3 newPos = cam.ScreenToWorldPoint (mousePos);
			newPos.z = 0;

			createSphere (newPos);
		}
	}

	void CreateMesh ()
	{
		
		
	}

	/// <summary>
	/// Finds the highest Y and lowest value in the array
	/// </summary>
	void findHeightestLowestVertice ()
	{
		float[] arrays = new float[meshPos.Count];

		for (int i = 0; i < meshPos.Count; i++) {
			arrays [i] = meshPos [i].y;
		}

		highestYvalue = arrays.Max ();
		lowestYvalue = arrays.Min ();
		Debug.Log ("high Y: " + highestYvalue);
		Debug.Log ("low Y: " + lowestYvalue);
	}

	void createManySpheres (Vector3[] position)
	{
		for (int i = 0; i < position.Length; i++) {
			GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			obj.transform.localScale = new Vector3 (0.5f, 0.5f, 0.1f);
			obj.transform.position = position [i];
			meshPos.Add (new Vector3 (position [i].x, position [i].y, 0));
		}
	}

	void createSphere (Vector3 position)
	{
		GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		obj.transform.localScale = (new Vector3 (0.5f, 0.5f, 0.1f));
		obj.transform.position = position;
		meshPos.Add (new Vector3 (position.x, position.y, 0));
	}
}
