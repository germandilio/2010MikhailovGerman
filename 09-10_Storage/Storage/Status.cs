using System;

namespace Storage
{
    /// <summary>
    /// Перечисление статусов заказов.
    /// </summary>
    [Flags]
    enum Status
    {
        Default = 0b_0000_0000,
        Processed = 0b_0000_0001,
        Paid = 0b_0000_0010,
        Uncoiled = 0b_0000_0100,
        Executed = 0b_0000_1000
    }
}
