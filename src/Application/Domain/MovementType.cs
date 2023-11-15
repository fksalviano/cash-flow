using System.ComponentModel;

namespace Application.Domain;

public enum MovementType
{
    [Description("C")]
    Credit = 1,

    [Description("D")]
    Debit = 2
}

