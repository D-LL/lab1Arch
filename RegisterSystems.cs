using System.Collections.Generic;

public class RegisterSystems
{
    public static List<ISystem> GetListOfSystems()
    {
        // determine order of systems to add
        List<ISystem> toRegister = new List<ISystem>();

        // AJOUTEZ VOS SYSTEMS ICI
        toRegister.Add(new SysInits());
        toRegister.Add(new sysPosition());
        toRegister.Add(new sysCollision());
        toRegister.Add(new sysColor());
        toRegister.Add(new sysDraw());
        return toRegister;
    }
}