namespace TelerikWpfApp2.Model.Stand.StandModules.Base;

public class NullStandModule : BaseStandModule
{
    private static NullStandModule instance;

    private NullStandModule(int num) : base(num)
    { }

    public static NullStandModule getInstance()
    {
        if (instance == null)
            instance = new NullStandModule(-1);
        return instance;
    }
   

}