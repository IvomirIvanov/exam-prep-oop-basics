﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Harvester
{
    private string id;
    private double oreOutput;
    private double energyRequirement;

    public string Id { get; protected set; }

    public double OreOutput
    {
        get
        {
            return this.oreOutput;
        }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException($"Harvester is not registered, because of it's OreOutput");
            }
            this.oreOutput = value;
        }
    }
    public double EnergyRequirement
    {
        get
        {
            return this.energyRequirement;
        }
        protected set
        {
            if (value < 0 || value > 20000)
            {
                throw new ArgumentException($"Harvester is not registered, because of it's EnergyRequirement");
            }
            this.energyRequirement = value;
        }
    }
}

