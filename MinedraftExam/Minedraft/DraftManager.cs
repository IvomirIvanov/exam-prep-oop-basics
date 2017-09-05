using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DraftManager
{
    //public List<string> arguments = new List<string>();
    private List<HammerHarvester> hammerHarvesters; 
    private List<SonicHarvester> sonicHarvesters; 
    private List<SolarProvider> solarProviders; 
    private List<PressureProvider> pressureProviders; 
    private double totalEnergyStored; 
    private double totalMinedOre;
    private string mode;

    public DraftManager()
    {
        hammerHarvesters = new List<HammerHarvester>();
        sonicHarvesters = new List<SonicHarvester>();
        solarProviders = new List<SolarProvider>();
        pressureProviders = new List<PressureProvider>();
        mode = "Full";
    }
    public string RegisterHarvester(List<string> arguments)
    {
        StringBuilder result = new StringBuilder();
        string type = arguments[0];
        string id = arguments[1];
        double oreOutput = double.Parse(arguments[2]);
        double energyRequirement = double.Parse(arguments[3]);
        var sonicFactor = 1;

        if (arguments.Count == 5)
        {
            sonicFactor = int.Parse(arguments[4]);
            try
            {
                SonicHarvester sh = new SonicHarvester(id, oreOutput, energyRequirement, sonicFactor);
                sonicHarvesters.Add(sh);
                result.AppendLine($"Successfully registered {type} Harvester - {id}");
            }
            catch (Exception ex)
            {
                result.AppendLine(ex.Message);
            }
        }
        if (arguments.Count == 4)
        {
            try
            {
                HammerHarvester hh = new HammerHarvester(id, oreOutput, energyRequirement);
                hammerHarvesters.Add(hh);
                result.AppendLine($"Successfully registered {type} Harvester - {id}");
            }
            catch (Exception ex)
            {
                result.AppendLine(ex.Message);
            }

        }

        return result.ToString().Trim();
    }
    public string RegisterProvider(List<string> arguments)
    {
        StringBuilder result = new StringBuilder();
        if (arguments.Count == 3)
        {
            string type = arguments[0];
            string id = arguments[1];
            double energyOutput = double.Parse(arguments[2]);

            if (type == "Solar")
            {
                try
                {
                    SolarProvider sp = new SolarProvider(id, energyOutput);
                    solarProviders.Add(sp);
                    result.AppendLine($"Successfully registered {type} Provider - {id}");
                }
                catch (Exception ex)
                {
                    result.AppendLine(ex.Message);
                }
            }
            if (type == "Pressure")
            {
                try
                {
                    PressureProvider pp = new PressureProvider(id, energyOutput);
                    pressureProviders.Add(pp);
                    result.AppendLine($"Successfully registered {type} Provider - {id}");
                }
                catch (Exception ex)
                {
                    result.AppendLine(ex.Message);
                }
            }
        }
        return result.ToString().Trim();
    }
    public string Day()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine("A day has passed.");
        totalEnergyStored += (solarProviders.Sum(s => s.EnergyOutput) + pressureProviders.Sum(p => p.EnergyOutput));
        double energyProvidedForTheDay = (solarProviders.Sum(s => s.EnergyOutput) + pressureProviders.Sum(p => p.EnergyOutput));
        double energyRequirement = hammerHarvesters.Sum(h => h.EnergyRequirement) + sonicHarvesters.Sum(s => s.EnergyRequirement);
        double minedOreForTheDay = 0;
        if (mode == "Full")
        {
            if (totalEnergyStored >= energyRequirement)
            {
                minedOreForTheDay = (hammerHarvesters.Sum(h => h.OreOutput) + sonicHarvesters.Sum(s => s.OreOutput));
                totalMinedOre += (hammerHarvesters.Sum(h => h.OreOutput) + sonicHarvesters.Sum(s => s.OreOutput));
                totalEnergyStored -= energyRequirement;
            }
        }
        //if (mode == "Energy")
        //{
        //    if (totalProvidedEnergy >= energyRequirement)
        //    {

        //    }
        //}
        if (mode == "Half")
        {
            double energyRequirementToSubtract = 0;
            energyRequirementToSubtract = 0.4 * energyRequirement;
            energyRequirement -= energyRequirementToSubtract;
            if (totalEnergyStored >= energyRequirement)
            {
                minedOreForTheDay = (hammerHarvesters.Sum(h => h.OreOutput) + sonicHarvesters.Sum(s => s.OreOutput)) / 2;
                totalMinedOre += minedOreForTheDay = (hammerHarvesters.Sum(h => h.OreOutput) + sonicHarvesters.Sum(s => s.OreOutput)) / 2;
                totalEnergyStored -= energyRequirement;
            }
        }
        result.AppendLine($"Energy Provided: {energyProvidedForTheDay}");
        result.AppendLine($"Plumbus Ore Mined: {minedOreForTheDay}");
        return result.ToString().Trim();

    }
    public string Mode(List<string> arguments)
    {
        StringBuilder result = new StringBuilder();
        if (arguments.Count == 1)
        {
            string newMode = arguments[0];
            mode = newMode;
            result.AppendLine($"Successfully changed working mode to {mode} Mode");
        }
        return result.ToString();
    }
    public string Check(List<string> arguments)
    {
        StringBuilder result = new StringBuilder();
        if (arguments.Count == 1)
        {
            string idToSearch = arguments[0];
            if (solarProviders.Any(s => s.Id == idToSearch))
            {
                var sp = solarProviders.Find(s => s.Id == idToSearch);
                result.Append(sp.ToString());
            }
            if (pressureProviders.Any(s => s.Id == idToSearch))
            {
                var pp = pressureProviders.Find(s => s.Id == idToSearch);
                result.Append(pp.ToString());
            }
            if (sonicHarvesters.Any(s => s.Id == idToSearch))
            {
                var sh = sonicHarvesters.Find(s => s.Id == idToSearch);
                result.Append(sh.ToString());
            }
            if (hammerHarvesters.Any(s => s.Id == idToSearch))
            {
                var hh = hammerHarvesters.Find(s => s.Id == idToSearch);
                result.Append(hh.ToString());
            }
            if (result.Length == 0)
            {
                result.AppendLine($"No element found with id - {idToSearch}");
            }
        }

        return result.ToString().Trim();
    }
    public string ShutDown()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine("System Shutdown");
        result.AppendLine($"Total Energy Stored: {totalEnergyStored}");
        result.AppendLine($"Total Mined Plumbus Ore: {totalMinedOre}");

        return result.ToString().Trim();
    }


}

