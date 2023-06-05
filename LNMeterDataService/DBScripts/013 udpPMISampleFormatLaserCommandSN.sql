
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpPMISampleFormatLaserCommandSN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpPMISampleFormatLaserCommandSN]
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
CREATE OR ALTER PROCEDURE [dbo].[udpPMISampleFormatLaserCommandSN]
	@xmlUnitSN varchar(max)=null,
    @xmlProdOrder varchar(max)=null,
    @xmlPart varchar(max)=null,
    @xmlPackages varchar(max)=null,
    @xmlStation varchar(max)=null,
    @EmployeeID int,
    @xmlExtraData varchar(max)=null
As

Declare @retValue int

Declare @PartID INT, @PartNumber varchar(200)
Set @xmlPart = REPLACE(@xmlPart,'<?xml version="1.0" encoding="ISO-8859-1"?>',N'')
EXEC @retValue= dbo.uspXMLPart @xmlPart,@PartID OUT,@PartNumber OUT
IF @retValue<>0 RETURN @retValue

Declare @StationID INT, @StationTypeID INT , @LineID INT 
Set @xmlStation = REPLACE(@xmlStation,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
EXEC @retValue= dbo.uspXMLStation @xmlStation,@StationID OUT,default,@StationTypeID OUT,@LineID OUT
IF @retValue<>0 RETURN @retValue

Declare @xmlFormatSN varchar(max), @DataFormat varchar(200), @StringSeparator varchar(200)
Set @xmlExtraData = REPLACE(@xmlExtraData,'<?xml version="1.0" encoding="ISO-8859-1"?>','')
exec @retValue = udpPMILaserXMLExtraData @xmlExtraData, 'FormatSN', @xmlFormatSN output
exec @retValue = udpPMILaserXMLExtraData @xmlExtraData, 'DataFormat', @DataFormat output
exec @retValue = udpPMILaserXMLExtraData @xmlExtraData, 'StringSeparator', @StringSeparator output

declare @ExtraData table (
	[ID] int identity(1,1), 
	orgSN varchar(200), 
	lenOrgSN int,
	formattedSN varchar(200))
Declare @xml xml
Set @xml = cast(@xmlFormatSN as xml)

insert @ExtraData (orgSN)
SELECT
	col.value('.','nvarchar(max)')
FROM @xml.nodes('/DT/D') tab(col)
Update @ExtraData Set lenOrgSN=len(orgSN) - len(REPLACE(orgSN,' ','' ))

If @DataFormat='88' And @StringSeparator=';' Begin
	Update @ExtraData Set formattedSN =SUBSTRING(orgSN,1,8)+ @StringSeparator + SUBSTRING(orgSN,10,8)

End else if @DataFormat='4334' And @StringSeparator='NA' begin
	Update @ExtraData Set formattedSN =OrgSN
End
else Begin
	Update @ExtraData Set formattedSN = [dbo].[udfPMILaserGetFormatSN](orgSN,@DataFormat,@StringSeparator) Where lenOrgSN <> 3
	Update @ExtraData Set formattedSN =OrgSN Where formattedSN is null
End

Select ID, orgSN, formattedSN from @ExtraData Order By ID

Return 0

GO