
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpPMISampleExtraValidation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpPMISampleExtraValidation]
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
CREATE PROCEDURE [dbo].[udpPMISampleExtraValidation]
	@xmlUnitSN text=null,
    @xmlProdOrder text=null,
    @xmlPart text=null,
    @xmlPackages text=null,
    @xmlStation text=null,
    @EmployeeID int,
    @xmlExtraData varchar(max)=null
As

--Put your validation codes

Return 0

GO