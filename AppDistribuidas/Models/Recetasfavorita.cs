using System;
using System.Collections.Generic;

namespace AppDistribuidas.Models;

public partial class Recetasfavorita
{
    public int IdRecetaFavorita { get; set; }

    public int Idreceta { get; set; }

    public int Idusuario { get; set; }
}
