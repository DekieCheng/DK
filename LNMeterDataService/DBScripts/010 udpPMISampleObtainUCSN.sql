
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpPMISampleObtainUCSN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpPMISampleObtainUCSN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
*************************************************************************************************
Author			: Dekie Cheng
Creation Date	: 2022/07/20 16:56PM
Explanation		: 
Script Version	: 2.8 and above
Parameter		: 
Output			: 
Is used			:
ModifiedHistory	:
*************************************************************************************************
*/
CREATE PROCEDURE [dbo].[udpPMISampleObtainUCSN]
	@xmlUnitSN text=null,
    @xmlProdOrder text=null,
    @xmlPart text=null,
    @xmlPackages text=null,
    @xmlStation text=null,
    @EmployeeID int,
    @xmlExtraData varchar(max)=null
As
Declare @ret int

--Get the SN Qty
Declare @SNQty int
exec @ret = uspXMLExtraData @xmlExtraData, 'SNQty', @SNQty output

--Define your own logics to obtain the Unit SN from someplace.
Select Top(@SNQty) [Value] as SN from ffSerialNumber Order By UnitID Desc

Return 0

GO

