print Current_Timestamp
print '---ÓÄÀËßÅÌ ÒÀÁËÈÖÛ-----------------------------------------' 

DROP TABLE [WeighData]

print 'ÓÄÀËÅÍÈÅ ÏĞÎÈÇÎØËÎ ÓÑÏÅØÍÎ (íå îáğàùàòü âíèìàíèÿ íà îøèáêè)' 
print Current_Timestamp

print '---ÑÎÇÄÀÅÌ ÒÀÁËÈÖÛ-----------------------------------------'
create table WeighData(
	WaighID BIGINT IDENTITY NOT NULL, 
	WaighTime DATETIME2 default Sysutcdatetime()  not null, 
	WaighMass float default 0 not null,
	WaighSpeed float default 0 not null

	primary key (WaighID)
) 

go


print '---ÊÎÍÅÖ---------------------------------------------------'
print Current_Timestamp