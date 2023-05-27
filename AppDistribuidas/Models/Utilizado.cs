using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Utilizado
{
    public int IdUtilizado { get; set; }

    public int? IdReceta { get; set; }

    public int? IdIngrediente { get; set; }

    public int? Cantidad { get; set; }

    public int? IdUnidad { get; set; }

    public string? Observaciones { get; set; }

    public virtual Ingrediente? IdIngredienteNavigation { get; set; }

    public virtual Receta? IdRecetaNavigation { get; set; }

    public virtual Unidade? IdUnidadNavigation { get; set; }
}
