using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Calificacione
{
    public int IdCalificacion { get; set; }

    public int? Idusuario { get; set; }

    public int? IdReceta { get; set; }

    public int? Calificacion { get; set; }

    public string? Comentarios { get; set; }

    public virtual Receta? IdRecetaNavigation { get; set; }

    public virtual Usuario? IdusuarioNavigation { get; set; }
}
