using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PressureProvider : Provider
{
    public PressureProvider(string id, double energyOutput)
    {
        this.Id = id;
        this.EnergyOutput = energyOutput;
        EnergyOutput += EnergyOutput / 2;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Pressure Provider - {this.Id}");
        sb.AppendLine($"Energy Output: {this.EnergyOutput}");        

        return sb.ToString();
    }
}
