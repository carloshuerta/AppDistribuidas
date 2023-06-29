using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Receta
{
    public int IdReceta { get; set; }

    public int? IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Foto { get; set; }

    public int? Porciones { get; set; }

    public int? CantidadPersonas { get; set; }

    public int? IdTipo { get; set; }

    public bool Habilitado { get; set; }

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual ICollection<Foto> Fotos { get; set; } = new List<Foto>();

    public virtual Tipo? IdTipoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Paso> Pasos { get; set; } = new List<Paso>();

    public virtual ICollection<Utilizado> Utilizados { get; set; } = new List<Utilizado>();
}
