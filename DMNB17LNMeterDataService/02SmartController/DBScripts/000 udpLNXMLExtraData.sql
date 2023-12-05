
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 25th July, 2022
Explanation		: 
	Parses xml string in the format (described in Parameter).
	If the unit exists, then check consistency for all the fields in the xml, and
		return UnitID and mentioned field value.
	If the unit does not exists, just returned parsed field value in @SN1 and @SN2.
Script Version	: 2.0
Parameter	

@xmlSerialNumbers =N'<DT>
						<D N="F"><![CDATA[1]]></D>
						<D N="G"><![CDATA[6]]></D>
					</DT>' 

Output			:		
Is used			:  	
ModifiedHistory	: 
**************************************************************************
*/
CREATE OR ALTER procedure [dbo].[udpLNXMLExtraData]
	@xmlExtraData XML,
	@AttName varchar(200),
	@AttValue nVarchar(max) output
AS
SET NOCOUNT ON

declare @ExtraData table (
	[ID] tinyint identity(1,1), 
	N varchar(200), 
	V_xml nvarchar(max))

insert @ExtraData (N, V_xml)
SELECT
	NULLIF(col.value('@N','varchar(200)'),''),
	col.value('.','nvarchar(max)')
FROM @xmlExtraData.nodes('/DT/D') tab(col)

select @AttValue=V_xml from @ExtraData where N=@AttName


