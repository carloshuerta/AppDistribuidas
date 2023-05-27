using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Ingrediente
{
    public int IdIngrediente { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Utilizado> Utilizados { get; set; } = new List<Utilizado>();
}
