
declare @OldScriptName varchar(800) 
declare @NewScriptName varchar(800) 
declare @CurrDatetime datetime
set @CurrDatetime=getDate()

set @OldScriptName = 'PMIVideoJetLaserPrint_2.8.24.104_FlexPM#140308-04%'
set @NewScriptName = 'PMIVideoJetLaserPrint_2.8.24.104_FlexPM#140308-04'
if not exists (Select 1 from fsDBInfo where ScriptName like @OldScriptName)
	insert fsDBInfo (ScriptName,ApplyDate )
	values (@NewScriptName, @CurrDatetime)
else
	update fsDBInfo
	set 
	    ScriptName = @NewScriptName, 
	    ApplyDate = @CurrDatetime
	where 
	   ScriptName like @OldScriptName
