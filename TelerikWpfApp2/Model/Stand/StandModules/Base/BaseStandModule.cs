namespace TelerikWpfApp2.Model.Stand.StandModules.Base;

public class BaseStandModule
{
    public BaseStandModule(int num)
    {
        Number = num;
    }


    public int Number { get; set; }

    public virtual bool WorkCheck()
    {
        return true;
    }
}