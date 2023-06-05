IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpPMILaserSaveOperationLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpPMILaserSaveOperationLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 1st August, 2022
Explanation		:  
Script Version	: 2.8
Parameter		:
Output			:		
Is used			:  	
ModifiedHistory	:	
**************************************************************************
*/

CREATE procedure [dbo].[udpPMILaserSaveOperationLog]
	@xmlUnitSN varchar(max)=null,
    @xmlProdOrder varchar(max)=null,
    @xmlPart varchar(max)=null,
    @xmlStation varchar(max)=null,
    @EmployeeID int,
    @xmlExtraData nvarchar(max)=null
AS
SET NOCOUNT ON

declare @ret int, @Action nvarchar(max),@TrayID int

Set @xmlExtraData = REPLACE(@xmlExtraData,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
exec @ret = udpPMILaserXMLExtraData @xmlExtraData, 'Action', @Action output
exec @ret = udpPMILaserXMLExtraData @xmlExtraData, 'TrayID', @TrayID output

Declare @StationID int, @StationTypeID int, @LineID int
exec @ret = dbo.uspXMLStation @xmlStation, @StationID out, default, @StationTypeID out, @LineID out

Insert into udtPMILaserPrintLog(TrayID, Operation, StationID, EmployeeID)
	Values(@TrayID, @Action, @StationID, @EmployeeID)

return 0

GO

