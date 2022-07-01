using System.Collections.Generic;
using System.Windows.Documents;


namespace TelerikWpfApp2;

public class Stand
{
    public List<BaseStandModule> StandModules { get; set; } = new()
    {
        new RelaySwitch(1),
        new RelaySwitch(12),
        new Supply(1)
    };

    public List<BaseVIP> VIPs { get; set; } = new()
    {
        new VIP71() { Name = "1", Temperature = 15, VoltageInput  = 20, VoltageOut1 = 290, VoltageOut2 = 8 },
        new VIP71() { Name = "2", Temperature = 25, VoltageInput  = 21, VoltageOut1 = 100, VoltageOut2 = 7 },
       new VIP71() { Name = "3", Temperature = 35, VoltageInput  = 22, VoltageOut1 = 90, VoltageOut2 = 5 },
       // new VIP71() { Number = 4, Temperature = 45, VoltageInput  = 30, VoltageOut1 = 80, VoltageOut2 = 5 },
       // new VIP71() { Number = 5, Temperature = 55, VoltageInput  = 10, VoltageOut1 = 200, VoltageOut2 = 1 },
    };

    public (bool, BaseStandModule) PreliminaryTestStandModules()
    {
        if (StandModules != null)
        {
            foreach (var module in StandModules)
            {
                if (module.WorkCheck())
                {
                    return (true, module);
                }
                return (false, module);
            }
        }
        return (false, NullStandModule.getInstance());
    }

    public (bool, BaseVIP) PreliminaryTestVips()
    {
        if (VIPs != null)
        {
            foreach (var vip in VIPs)
            {
                if (vip.WorkCheck())
                {
                    return (true, vip);
                }
                return (false, vip);
            }
        }
        return (false, new BaseVIP());
    }


}