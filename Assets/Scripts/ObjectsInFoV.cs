using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsInFoV : MonoBehaviour
{
    Renderer[] renderers;
    public GameObject Sphere;
    public float radius = 0.5f;
    public float Timer = 0.3f;
    public List<GameObject> sphereList = new List<GameObject>();

    private Vector3 minScale;
    private Vector3 maxScale;
    public float speed = 2f;
    public float duration = 5f;


    void Awake()
    {
        GameObject walls = GameObject.Find("Particle");
        renderers = walls.GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        OutputVisibleRenderers(renderers);
    }

    private void Start()
    {
        maxScale = transform.localScale;
    }

    public IEnumerator ScaleOnRepeat(Vector3 a, Vector3 b, float time)
    {
        float i = 0.01f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i = Time.deltaTime * rate;
            transform.localScale=Vector3.Lerp(a,b,i);
            yield return null;
        }
    }

    GameObject sphereAlive;

    void OutputVisibleRenderers(Renderer[] renderers)
    {
        foreach (var renderer in renderers)
        {
            // output only the visible renderers' name
            if (IsVisible(renderer))
            {
                Timer -= Time.deltaTime;
                renderer.enabled = true;
                Vector3 pos = (Random.insideUnitSphere * radius) + renderer.transform.position;
                if (Timer <= 0f)
                {
                    sphereAlive = Instantiate(Sphere, pos, Quaternion.identity);
                    sphereList.Add(sphereAlive);
                    sphereAlive.transform.parent = renderer.transform;
                    Timer = 4f;
                    StartCoroutine(ScaleOnRepeat(minScale, maxScale, 6f));
                    
                }
            }
            else
            {
                maxScale = transform.localScale;
                StartCoroutine(ScaleOnRepeat(maxScale, minScale, 2f));
            }
        }
    }

    private bool IsVisible(Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            return true;
        else
            return false;
    }
}