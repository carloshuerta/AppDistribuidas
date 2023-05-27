using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Paso
{
    public int IdPaso { get; set; }

    public int? IdReceta { get; set; }

    public int? NroPaso { get; set; }

    public string? Texto { get; set; }

    public virtual Receta? IdRecetaNavigation { get; set; }

    public virtual ICollection<Multimedium> Multimedia { get; set; } = new List<Multimedium>();
}
