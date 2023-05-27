using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Tipo
{
    public int IdTipo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
