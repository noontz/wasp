namespace wasp.enums
{
    enum LanguageTypes : sbyte
    {
        None = 0x00,
        Int32 = -0x01,
        Int64 = -0x02,
        Float32 = -0x03,
        Float64 = -0x04,
        AnyFunc = -0x10,
        Func = -0x20,
        EmptyBlock = -0x40
    }
}