using System.Collections.Generic;
using System.Windows.Documents;
using TelerikWpfApp2.Model.Stand.StandModules;
using TelerikWpfApp2.Model.Stand.StandModules.Base;
using TelerikWpfApp2.Model.TestedVIPs;

namespace TelerikWpfApp2.Model.Stand;

public class Stand
{
    private List<BaseStandModule> StandModules = new()
    {
        new RelaySwitch(1),
        new RelaySwitch(12),
        new Supply(1)
    };


    
    private List<BaseVIP> VIPs = new()
    {
        new VIP71() { Number = 1, Temperature = 15, VoltageInput  = 20, VoltageOut1 = 290, VoltageOut2 = 8 },
        new VIP71() { Number = 2, Temperature = 25, VoltageInput  = 21, VoltageOut1 = 100, VoltageOut2 = 7 },
        new VIP71() { Number = 3, Temperature = 35, VoltageInput  = 22, VoltageOut1 = 90, VoltageOut2 = 5 },
        new VIP71() { Number = 4, Temperature = 45, VoltageInput  = 30, VoltageOut1 = 80, VoltageOut2 = 5 },
        new VIP71() { Number = 5, Temperature = 55, VoltageInput  = 10, VoltageOut1 = 200, VoltageOut2 = 1 },
    };

    public (bool,BaseStandModule) TestStandModules()
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

    public (bool, BaseVIP) TestVips()
    {
        if (VIPs != null)
        {
            foreach (var vip in VIPs)
            {
                if (vip.())
                {
                    return (true, module);
                }
                return (false, module);
            }
        }
        return (false, new BaseVIP());
    }
}