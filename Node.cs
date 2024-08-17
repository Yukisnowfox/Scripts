using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class Node : MonoBehaviour
{

	[SerializeField] public TowerEntry[] towers;
    public Color hoverColor;
    public Color notEnoughRocksColor;
    public Vector3 positionOffset;

    public GameObject tower;

    private Vector3 _startPoint;
    private Vector3 _endPoint;
	
	[HideInInspector] public TowerEntry towerEntry;

    private Renderer rend;
    private Color startColor;

    private NavMeshObstacle navMeshObstacle;
    BuildManager buildManager;
	
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;


        if (tower != null)
        {
            buildManager.BuildRockOn(this, tower);
        }
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (tower != null)
		{
			buildManager.SelectNode(this);
			return;
		}
		
		if (!buildManager.CanBuild)
			return;
		
        // Perform path check before placing the tower
        if (CanPlaceTowerWithoutBlockingPath(buildManager.GetRandomTowerToBuild()))
        {
            BuildRandomTower(buildManager.GetRandomTowerToBuild());
            buildManager.IsPlacingRandomTower = false; // Reset after placement
        }
        else
        {
            Debug.Log("Can't place tower here: it would block the path!");
        }
    }

    bool CanPlaceTowerWithoutBlockingPath(TowerEntry towerEntry)
    {
        // Calculate the path from start to end
        NavMeshPath path = new NavMeshPath();
        _startPoint = PathFindingManager.instance.startPoint;
        _endPoint = PathFindingManager.instance.endPoint;
        NavMesh.CalculatePath(_startPoint, _endPoint, NavMesh.AllAreas, path);

        // Check if the path is valid
        bool pathIsValid = path.status == NavMeshPathStatus.PathComplete;

        return pathIsValid;
    }

	void BuildRandomTower(TowerEntry randomTower)
    {
        if (PlayerStats.RocksRemaining < randomTower.cost)
		{
			Debug.Log("No more Rocks");
			return;
		}

		PlayerStats.RocksRemaining -= randomTower.cost;

		if (towers.Length == 0)
        {
            Debug.LogWarning("No towers in the list!");
            return;
        }

        // Calculate the total weight
        float totalWeight = 0f;
        foreach (var towerEntry in towers)
        {
            totalWeight += towerEntry.weight;
        }

        // Get a random value within the range of total weight
        float randomValue = Random.Range(0f, totalWeight);

        // Find the selected tower based on the random value
        float cumulativeWeight = 0f;
        foreach (var towerEntry in towers)
        {
            cumulativeWeight += towerEntry.weight;
            if (randomValue <= cumulativeWeight)
            {
                tower = Instantiate(towerEntry.towerPrefab, GetBuildPosition(), Quaternion.identity);
				
				GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
				Destroy(effect, 5f);

				Debug.Log(tower != null ? "Tower placed successfully" : "Failed to place tower");
                
                break;
            }
        }


		towerEntry = randomTower;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasRock)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughRocksColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}