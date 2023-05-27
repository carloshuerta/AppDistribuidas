using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Multimedium
{
    public int IdContenido { get; set; }

    public int IdPaso { get; set; }

    public string? TipoContenido { get; set; }

    public string? Extension { get; set; }

    public string? UrlContenido { get; set; }

    public virtual Paso IdPasoNavigation { get; set; } = null!;
}
