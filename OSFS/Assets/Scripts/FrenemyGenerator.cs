using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenemyGenerator : MonoBehaviour
{
    private GeneratorDatabase genData;

    public List<TierRank> tierRanks;

    [HideInInspector] public int numAgents = 10;
    public List<Agent> agents;

    private void Start()
    {
        agents = new List<Agent>();
        genData = GameObject.FindObjectOfType<GeneratorDatabase>();
        GenerateTierRanks();
        Agent topAgent = genData.CreateAgent(tierRanks[tierRanks.Count - 1].rank);
        agents.Add(topAgent);
        GenerateFrenemyTree(topAgent, tierRanks.Count - 2);
        //GenerateRandomAgents();
    }

    private void GenerateFrenemyTree(Agent agent, int tierNum)
    {
        if (tierNum < 0)
            return;

        for(int i = 0; i < tierRanks[tierNum].numOfInferiors; i++)
        {
            Agent newInferior = genData.CreateAgent(tierRanks[tierNum].rank);
            newInferior.SetSuperior(agent);
            agent.AddInferior(newInferior);
            GenerateFrenemyTree(newInferior, tierNum - 1);
            agents.Add(newInferior);
        }
    }

    public void SwapPositionsWithSuperior(Agent agent)
    {
        // Agent swaps place with superior

        // save values from superior
        Agent PunchedSuperior = agent.superior;
        Rank newRank = agent.superior.rank;
        Agent newSuperior = agent.superior.superior;
        List<Agent> newInferiors = new List<Agent>(agent.superior.inferiors);
        newInferiors.Remove(agent);
        newInferiors.Add(agent.superior);

        // set superiors values to the target inferior
        List<Agent> oldInferiors = new List<Agent>(agent.inferiors);
        PunchedSuperior.SetInferiors(oldInferiors);
        PunchedSuperior.SetSuperior(agent);
        PunchedSuperior.rank = agent.rank;

        // put the formerly inferior agent in their rightful place
        agent.SetInferiors(newInferiors);
        agent.SetSuperior(newSuperior);
        agent.rank = newRank;

        // update the new superiors name with a title.
        genData.AddTitle(agent);
    }

    private void GenerateRandomAgents()
    {
        agents = new List<Agent>();
        for (int i = 0; i < numAgents; i++)
            agents.Add(genData.CreateAgent());
    }

    public void GenerateTierRanks()
    {
        tierRanks = new List<TierRank>();
        for (int i = 0; i < genData.ranks.Count; i++)
        {
            tierRanks.Add(new TierRank(genData.ranks[i].name, genData.ranks[i], 2));
        }
    }

    public class TierRank
    {
        public string name;
        public Rank rank;
        public int numOfInferiors;

        public TierRank(string name, Rank rank, int numOfInferiors)
        {
            this.name = name;
            this.rank = rank;
            this.numOfInferiors = numOfInferiors;
        }
    }
}
