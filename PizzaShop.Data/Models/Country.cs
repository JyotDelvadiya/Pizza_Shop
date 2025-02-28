using System;
using System.Collections.Generic;

namespace PizzaShop.Data.Models;

public partial class Country
{
    public int Countryid { get; set; }

    public string Countryname { get; set; } = null!;

    public string Countrycode { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<State> States { get; } = new List<State>();
}
