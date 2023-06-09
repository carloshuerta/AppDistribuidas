/*
   Thursday, June 29, 202311:15:13 AM
   User: sa
   Server: localhost
   Database: ApplicacionesDistribuidas
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.recetas ADD
	personalizada bit NOT NULL CONSTRAINT DF_recetas_personalizada DEFAULT 0
GO
ALTER TABLE dbo.recetas SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
