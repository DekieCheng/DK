IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udpLNXMLExtraData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[udpLNXMLExtraData]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
***************************************************************************
Author			: Dekie Cheng
Creation Date	: 4th Jun, 2023
Explanation		: 
	Parses xml string in the format (described in Parameter).
Script Version	: 1.0
Parameter	

@xmlSample =N'<DT>
				<D N="F"><![CDATA[1]]></D>
				<D N="G"><![CDATA[6]]></D>
			</DT>' 
Output			:		
Is used			:  	
ModifiedHistory	: 
**************************************************************************
*/
CREATE procedure [dbo].[udpLNXMLExtraData]
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


