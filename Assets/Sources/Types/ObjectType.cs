using System;

[Flags]
public enum ObjectType : int
{
    Customer = 1,
    Hub = 1 << 1,
    Man = 1 << 2,

    //customers variations
    Bikers = 1 << 3,
    Cowboy = 1 << 4,
    Aliens = 1 << 5,
    Robot = 1 << 6,
    Vault = 1 << 7,

    //resource variations
    Resource = 2 << 8,
    One = 2 << 9,
    Two = 2 << 10,
    Three = 2 << 11,
}