using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrenemyVisualizationController : MonoBehaviour
{
    private List<GameObject> tierLayouts;
    private List<GameObject> agentButtons;

    private FrenemyGenerator frenemyGen;
    private GeneratorDatabase genData;

    public GameObject templateAgentVis;
    public GameObject templateTierVis;

    private void Start()
    {
        frenemyGen = GameObject.FindObjectOfType<FrenemyGenerator>();
        genData = GameObject.FindObjectOfType<GeneratorDatabase>();

        GenerateVisualization();
    }

    public void GenerateVisualization()
    {
        if (tierLayouts != null)
        {
            foreach (GameObject o in tierLayouts)
            {
                Destroy(o);
            }
            tierLayouts.Clear();
        }
        if (agentButtons != null)
        {
            foreach (GameObject o in agentButtons)
            {
                Destroy(o);
            }
            agentButtons.Clear();
        }
        tierLayouts = new List<GameObject>();
        agentButtons = new List<GameObject>();

        // generate complete visualization for frenemy system.

        // generate each tier
        templateAgentVis.SetActive(false);
        templateTierVis.SetActive(true);
        foreach (Rank rank in genData.ranks)
        {
            GameObject newTier = Instantiate(templateTierVis, templateTierVis.transform.parent);
            newTier.name = rank.name;
            newTier.GetComponent<Image>().color = rank.color;
            tierLayouts.Add(newTier);
        }
        templateTierVis.SetActive(false);
        // within each tier generate agents
        templateAgentVis.SetActive(true);
        foreach(Agent agent in frenemyGen.agents)
        {
            int tierIndex = genData.ranks.IndexOf(agent.rank);
            Transform targetParent = tierLayouts[tierIndex].transform;
            GameObject newAgentButton = Instantiate(templateAgentVis, targetParent);
            newAgentButton.name = agent.rank.name + " " + agent.name;
            newAgentButton.GetComponentInChildren<Text>().text = agent.rank.name + " " + agent.name;
            newAgentButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                frenemyGen.SwapPositionsWithSuperior(agent);
                GenerateVisualization();
            });
            agentButtons.Add(newAgentButton);
        }
        templateAgentVis.SetActive(false);
    }
}
