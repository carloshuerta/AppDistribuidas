USE [ApplicacionesDistribuidas]
GO

/****** Object:  Table [dbo].[recetasfavoritas]    Script Date: 6/20/2023 6:57:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[recetasfavoritas](
	[idRecetaFavorita][int] IDENTITY(1,1) PRIMARY KEY,
	[idreceta] [int] NOT NULL,
	[idusuario] [int] NOT NULL,
) ON [PRIMARY]
GO