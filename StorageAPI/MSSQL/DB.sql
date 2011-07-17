print Current_Timestamp
print '---ÓÄÀËßÅÌ ÒÀÁËÈÖÛ-----------------------------------------' 

DROP TABLE [WeighData]

print 'ÓÄÀËÅÍÈÅ ÏĞÎÈÇÎØËÎ ÓÑÏÅØÍÎ (íå îáğàùàòü âíèìàíèÿ íà îøèáêè)' 
print Current_Timestamp

print '---ÑÎÇÄÀÅÌ ÒÀÁËÈÖÛ-----------------------------------------'
create table WeighData(
	ID BIGINT IDENTITY NOT NULL, 
	CourseID INT DEFAULT 0 not null,
	WeighTime DATETIME2 default Sysutcdatetime()  not null, 
	Weigh float default 0 not null,
	AvgSpeed float default 0 not null

	primary key (ID)
) 

go


print '---ÊÎÍÅÖ---------------------------------------------------'
print Current_Timestamp