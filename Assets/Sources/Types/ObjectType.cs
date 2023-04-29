using System;

[Flags]
public enum ObjectType : int
{
    Resource = 0,
    Customer = 1,
    Hub = 1 << 1,
    Man = 1 << 2,
    
    //customers variations
    Bikers = 1 << 3,
    Cowboy = 1 << 4,
    Aliens = 1 << 5,
    Robot = 1 << 6,
    Vault = 1 << 7,
}