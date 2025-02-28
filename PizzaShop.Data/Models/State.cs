using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class State
{
    public int Stateid { get; set; }

    public string Statename { get; set; } = null!;

    public string Statecode { get; set; } = null!;

    public int? Countryid { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual Country? Country { get; set; }
}
