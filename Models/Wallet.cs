using System;
using System.Collections.Generic;
using NodaTime;

namespace G_APIs.Models;

public partial class Wallet
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string? CreateDate { get; set; }

    public short Status { get; set; }

    public double Balance { get; set; }

}
