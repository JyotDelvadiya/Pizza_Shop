using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class City
{
    public int Cityid { get; set; }

    public string Cityname { get; set; } = null!;

    public int? Stateid { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual State? State { get; set; }
}
