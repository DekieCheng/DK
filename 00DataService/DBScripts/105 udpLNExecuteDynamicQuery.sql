
/*
********************************************************************************************************************************
Author			: Dekie Cheng
Creation Date	: 5th Jun, 2023
Explanation		: A wrapper for dynamic query executed from the Client.
Script Version	: 1.0
Parameter	  	: @tableName 	varchar(8000),
				  @restriction 	varchar(8000) = '',	-- a selection clause prefix with ' WHERE '
				  @fieldName 	varchar(8000) = '*'
ModifiedHistory	
**********************************************************************************************************************************
*/

CREATE OR ALTER procedure [dbo].[udpLNExecuteDynamicQuery]
	@tableName		varchar(max),
	@restriction	varchar(max) = '',
	@fieldName		varchar(max) = '*'
AS
SET NOCOUNT ON

declare
	@QueryString as nvarchar(max)

set @QueryString = 'SELECT ' + @fieldName + ' FROM ' + @tableName + @restriction
exec sp_executesql @QueryString
return 0
















