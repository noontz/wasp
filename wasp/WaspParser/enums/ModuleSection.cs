namespace wasp.enums
{
    enum ModuleSections : byte
    {
        Custom = 0x00,
        Type = 0x01,
        Import = 0x02,
        Function = 0x03,
        Table = 0x04,
        Memory = 0x05,
        Global = 0x06,
        Export = 0x07,
        Start = 0x10,
        Element = 0x11,
        Code = 0x012,
        Data = 0x013
    }
}