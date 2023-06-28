
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 6th Jun, 2023
Explanation		:  
Script Version	: 1.0
Parameter	:
Output			:		
Is used			:  	
ModifiedHistory		:	
**************************************************************************
*/

CREATE OR Alter procedure [dbo].[udpLNSaveRuntimeLogProxy]
	@MeterCategoryID int,
    @SerialPortServerID int,
	@MeterID int,
	@ErrorMessage nvarchar(max)
AS
SET NOCOUNT ON

begin tran
	insert udtLNRuntimeLogInfo (
		[MeterCategoryID] ,
		[SerialPortServerID] ,
		[MeterID],
		[Description]
		)
	Values(
		@MeterCategoryID,
		@SerialPortServerID,
		@MeterID,
		@ErrorMessage
	)
Commit tran

return 0


GO

