using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Foto
{
    public int Idfoto { get; set; }

    public int IdReceta { get; set; }

    public string? UrlFoto { get; set; }

    public string? Extension { get; set; }

    public virtual Receta? IdRecetaNavigation { get; set; } = null!;
}
