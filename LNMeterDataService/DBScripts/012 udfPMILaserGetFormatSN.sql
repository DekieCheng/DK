/****** Object:  UserDefinedFunction [dbo].[udfPMILaserGetFormatSN]    Script Date: 08/25/2019 13:36:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udfPMILaserGetFormatSN]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udfPMILaserGetFormatSN]
GO

/****** Object:  UserDefinedFunction [dbo].[udfPMILaserGetFormatSN]    Script Date: 08/25/2019 13:36:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
***************************************************************************
Author		 : Dekie Cheng
Creation Date: 7th Marcha, 2023
Explanation	 : Will auto-set the SN as specified format
Script Version: 2.x
Parameter	:	
Output		 :		
Is used		 :  	
ModifiedHistory : 
***************************************************************************
*/
Create OR Alter function [dbo].[udfPMILaserGetFormatSN](
    @sn varchar(200),
    @format varchar(200),
	@StringSeparator varchar(200)
)
returns varchar(200)
AS
BEGIN
declare 
	@len int, 
	@startPos int, 
	@subStrLen int, 
	@formattedSN varchar(200)

Set @formattedSN =''
Set @len = DATALENGTH(@format)
Set @startPos = 1
set @subStrLen = 0

while (@startPos<=@len)
Begin
	Set @subStrLen = cast(SUBSTRING(@format, @startPos,1) as int)
	Set @formattedSN = @formattedSN + SUBSTRING(@sn, 1 ,@subStrLen ) + ' '
	Set @sn =  SUBSTRING(@sn, @subStrLen+1 ,len(@sn)-@subStrLen )
	Set @startPos = @startPos + 1
End
Return rtrim(@formattedSN)

END

GO

