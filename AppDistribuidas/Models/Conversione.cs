using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Conversione
{
    public int IdConversion { get; set; }

    public int IdUnidadOrigen { get; set; }

    public int IdUnidadDestino { get; set; }

    public double? FactorConversiones { get; set; }

    public virtual Unidade? IdUnidadDestinoNavigation { get; set; } = null!;

    public virtual Unidade? IdUnidadOrigenNavigation { get; set; } = null!;
}
