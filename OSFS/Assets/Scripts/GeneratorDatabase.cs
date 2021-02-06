using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorDatabase : MonoBehaviour
{
    public string[] names = { "Kas", "Case", "Spoony", "Resin", "Victoria", "Lynne", "Zach Grey", 
                              "Shady Knight", "Fight Knight", "Tepan", "Evie", "Abigail Thorne",
                              "Jeep", "Shane", "Grigor", "Krull", "Nervous", "Gigaxis", "Grexlor",
                              "Levior", "Boris Spassky", "Coconut" };
    public string[] titles = { "the one-eyed one", "the loud one", "the super angry one", "the unreal dev", 
                               "our many-legged friend", "the animated-on-nickeloden-one", "the cat", 
                               "the chess grandmaster"};

    public List<Rank> ranks;
    public List<Attribute> attributes;

    private void Awake()
    {
        InitValues();
        CreateRanks();
        CreateAttributes();
    }

    protected void InitValues()
    {
        ranks = new List<Rank>();
        attributes = new List<Attribute>();
    }

    protected void CreateRanks()
    {
        ranks.Add(new Rank("Common", Color.grey));
        ranks.Add(new Rank("Uncommon", Color.green));
        ranks.Add(new Rank("Rare", Color.blue));
        ranks.Add(new Rank("Legendary", Color.magenta));
        ranks.Add(new Rank("Exotic", Color.yellow));
        ranks.Add(new Rank("Ethereal", Color.red));
    }

    public void CreateAttributes()
    {
        attributes.Add(new Attribute("Electricity"));
        attributes.Add(new Attribute("Fire"));
        attributes.Add(new Attribute("Water"));
        attributes.Add(new Attribute("Ice"));
        attributes.Add(new Attribute("Rainbows"));
        attributes.Add(new Attribute("Fairy"));
        attributes.Add(new Attribute("Nature"));
        attributes.Add(new Attribute("Poison"));
        attributes.Add(new Attribute("Spitting"));
        attributes.Add(new Attribute("Boomerangs"));
        attributes.Add(new Attribute("Teleportation"));
        attributes.Add(new Attribute("Earth"));
        attributes.Add(new Attribute("Coffee"));
        attributes.Add(new Attribute("Tea"));
    }

    public Agent CreateAgent(Rank rank)
    {
        return new Agent(GetRandomName(), rank, GetRandomAttributes(1, 2), GetRandomAttributes(1, 2));
    }

    public Agent CreateAgent()
    {
        return new Agent(GetRandomName(), GetRandomRank(), GetRandomAttributes(1, 2), GetRandomAttributes(1, 2));
    }

    public Rank GetRandomRank()
    {
        return ranks[Random.Range(0, ranks.Count)];
    }

    public List<Attribute> GetRandomAttributes(int min, int max)
    {
        int targetAmt = (int)Random.Range(min, max);
        List<Attribute> returnAttributes = new List<Attribute>();
        for (int i = 0; i < targetAmt; i++)
        {
            returnAttributes.Add(attributes[Random.Range(0, attributes.Count)]);
        }
        return returnAttributes;
    }

    public string GetRandomName()
    {
        return names[Random.Range(0, names.Length)];
    }

    public void AddTitle(Agent agent)
    {
        agent.name += " " + titles[Random.Range(0, titles.Length)];
    }
}

[System.Serializable]
public class Agent
{
    public string name;
    public Rank rank;
    public List<Attribute> strengths;
    public List<Attribute> weaknesses;

    public Agent superior;
    public List<Agent> inferiors;

    public Agent(string name, Rank rank, List<Attribute> strengths, List<Attribute> weaknesses)
    {
        this.name = name;
        this.rank = rank;
        this.strengths = strengths;
        this.weaknesses = weaknesses;

        inferiors = new List<Agent>();
    }

    public void SetSuperior(Agent superior)
    {
        this.superior = superior;
    }

    public void AddInferior(Agent inferior)
    {
        inferiors.Add(inferior);
    }

    public void SetInferiors(List<Agent> inferiors)
    {
        this.inferiors = inferiors;
    }
}

[System.Serializable]
public class Rank
{
    public string name;
    public Color32 color;

    public Rank(string name, Color32 color)
    {
        this.name = name;
        this.color = color;
    }
}

[System.Serializable]
public class Attribute
{
    public string name;

    public Attribute(string name)
    {
        this.name = name;
    }
}