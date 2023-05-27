using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Unidade
{
    public int IdUnidad { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Conversione> ConversioneIdUnidadDestinoNavigations { get; set; } = new List<Conversione>();

    public virtual ICollection<Conversione> ConversioneIdUnidadOrigenNavigations { get; set; } = new List<Conversione>();

    public virtual ICollection<Utilizado> Utilizados { get; set; } = new List<Utilizado>();
}
