using System;
using System.Collections.Generic;

namespace Zadanie1ClientAgentV1.ModelsDb;

public partial class Client
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public double? Phone { get; set; }

    public string? Email { get; set; }
}
