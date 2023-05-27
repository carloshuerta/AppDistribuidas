using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Mail { get; set; }

    public string Nickname { get; set; } = null!;

    public string? Habilitado { get; set; }

    public string? Nombre { get; set; }

    public string? Avatar { get; set; }

    public string? Password { get; set; }

    public string? Token { get; set; }

    public string? TipoUsuario { get; set; }

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
