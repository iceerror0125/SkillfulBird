using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelBackground : MonoBehaviour
{
    [Header("Range")]
    [SerializeField] private float cloudXRange;
    [SerializeField] private float treeXRange;
    [SerializeField] private float bushXRange;
    [Header("References")]
    [SerializeField] private List<GameObject> trees;
    [SerializeField] private GameObject cloud;
    [SerializeField] private GameObject bush;
    [Header("Constaints")]
    [SerializeField] private float minTreesDistance; // min distance between 2 trees

    private BoxCollider2D boxCollider;

    private float speed = -5;
    private float xMoving;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        xMoving = Time.deltaTime * speed;
        transform.position = new Vector2(transform.position.x+xMoving, transform.position.y);
    }

    public void SetSpeed(float speed)
    {
        this.speed = Mathf.Abs(speed) * -1;
    }
    public void GenerateBackground()
    {
        CreateTrees();
        CreateCloud();
        CreateBush();
    }
    private void CreateTrees()
    {
        int min = 0;
        int max = trees.Count + 1;
        int quantityOfTree = Random.Range(min, max);

        // reset trees state
        foreach (GameObject tree in trees)
        {
            tree.SetActive(false);
        }

        // init tree
        if (quantityOfTree == 1)
        {
            GameObject tree = GetRandomTree();
            tree.SetActive(true);
            SetRandomPostion(tree, treeXRange);
            return;
        }

        for (int i = 0; i < quantityOfTree; i++)
        {
            GameObject tree = trees[i];
            tree.SetActive(true);
            SetRandomPostion(tree, treeXRange);
        }

        // Check min distance between 2 trees
        float x1 = trees[0].transform.localPosition.x;
        float x2 = trees[1].transform.localPosition.x;
        float difference = Mathf.Abs(x1 - x2);
        if (difference <= minTreesDistance)
        {
            GameObject tree = GetTreeFarFromLimitRange();
            float x = tree.transform.localPosition.x;
            if (x < 0)
            {
                x += minTreesDistance;
            }
            else
            {
                x -= minTreesDistance;
            }
            tree.transform.localPosition = new Vector2(x, tree.transform.localPosition.y);
        }

        GameObject GetTreeFarFromLimitRange()
        {
            float dif1 = Mathf.Abs(treeXRange) - Mathf.Abs(x1);
            float dif2 = Mathf.Abs(treeXRange) - Mathf.Abs(x2);
            if (dif1 < dif2)
            {
                return trees[1];
            }
            return trees[0];
        }
    }
  
    private GameObject GetRandomTree()
    {
        int index = Random.Range(0, trees.Count);
        return trees[index];
    }
    private void SetRandomPostion(GameObject obj, float limit)
    {
        float xPos = Random.Range(-limit, limit);
        obj.transform.localPosition = new Vector2(xPos, transform.localPosition.y);
    }
    private void CreateCloud()
    {
        CreateRandomAppearObjectWithLimitRange(cloud, cloudXRange);
    }
    private void CreateBush()
    {
        CreateRandomAppearObjectWithLimitRange(bush, bushXRange);
    }
    private void CreateRandomAppearObjectWithLimitRange(GameObject obj, float limit)
    {
        int random = Random.Range(0, 2);

        if (random == 1)
        {
            obj.SetActive(true);
            SetRandomPostion(obj, cloudXRange);
        }
        else
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning(collision.tag);
        if (collision.tag.Equals("BackgroundLimit"))
        {
            Observer.Instance.Announce(new Message(EventType.BackgroundReachLimit, this));
        }
    }

    public float GetWidth()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            return renderer.size.x;
        }
        return -1;
    }
}
