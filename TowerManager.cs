using UnityEngine;

public class TowerManager : MonoBehaviour
{
    BuildManager buildManager;

    public Node targetNode;

    void Start ()
    {
        buildManager = BuildManager.instance;
    }
    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectRandomTower();
        }
    }

    public void SelectRandomTower ()
    {
        Debug.Log("Random Tower Selected");
        buildManager.IsPlacingRandomTower = true;
        TowerEntry randomTower = targetNode.towers[Random.Range(0, targetNode.towers.Length)];
        buildManager.SelectRandomTowerToBuild(randomTower);
    }
}