using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour {

    private bool dir;
    public int speed;

    public GameObject SplatterPrefab;
    public Text m_text;
    private int counter;
    private bool m_isSucceeded;
    private Renderer m_renderer;
    private List<GameObject> splatters = new List<GameObject>();
    // Use this for initialization
    void Start () {
        dir = false;
        counter = 0;
        m_isSucceeded = false;
        m_renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (dir && transform.position.x < -5)
            dir = !dir;
        else if (!dir && transform.position.x > 5)
            dir = !dir;

        if (dir)
            transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);

        //Debug.Log("HI");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Coll");
        var other = collision.collider.gameObject;
        Vector3 hit_position = other.transform.position;
        if (other.CompareTag("Ball"))
        {
            PhotonNetwork.Destroy(other);
            Quaternion rot = Quaternion.AngleAxis(Random.Range(0f, 360f), new Vector3(0, 0, 1)); //*transform.rotation;
            m_renderer.material.color = new Color(0.0f, 1.0f - counter * 0.2f, 0.0f);
            counter++;
            m_text.text = "Score : " + counter + " / 5";
            if (counter == 5)
            {
                m_isSucceeded = true;
                Time.timeScale = 0;
            }
        }

    }

    void OnGUI()
    {
        if (m_isSucceeded)
        {
            GUI.skin.label.fontSize = 20;
            GUI.skin.label.normal.textColor = new Color(0, 0, 0, 1);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            //GUIStyle background = new GUIStyle();
            //background.normal.background = img;
            //GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "", background);

            //int num = 8;

            GUI.Label(new Rect(0, Screen.height / 2, Screen.width, 25), "Game Succeeded!");
        }
    }
}
