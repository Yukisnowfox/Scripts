using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    public bool IsPlacingRandomTower;
    private TowerEntry randomTowerToBuild;
	private Node selectedNode;
	public NodeUI nodeUI;

    public bool CanBuild { get { return randomTowerToBuild != null; } }
    public bool HasRock { get { return PlayerStats.RocksRemaining >= randomTowerToBuild.cost; } }


    public void SelectNode (Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode(); // Deselect if already selected
            return;
        }

        selectedNode = node;
		randomTowerToBuild = null;

		nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
		nodeUI.Hide();
    }

	public void SelectRandomTowerToBuild (TowerEntry randomTower)
	{
		randomTowerToBuild = randomTower;
		DeselectNode();
	}

	public void BuildRockOn (Node node, GameObject towerPrefab)
	{
		Instantiate(towerPrefab, node.GetBuildPosition(), Quaternion.identity);
	}

	public TowerEntry GetRandomTowerToBuild ()
	{
		return randomTowerToBuild;
	}
}