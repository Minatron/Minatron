print Current_Timestamp
print '---������� �������-----------------------------------------' 

DROP TABLE [WeighData]

print '�������� ��������� ������� (�� �������� �������� �� ������)' 
print Current_Timestamp

print '---������� �������-----------------------------------------'
create table WeighData(
	ID BIGINT IDENTITY NOT NULL, 
	CourseID INT DEFAULT 0 not null,
	WeighTime DATETIME2 default Sysutcdatetime()  not null, 
	Weigh float default 0 not null,
	AvgSpeed float default 0 not null

	primary key (ID)
) 

go


print '---�����---------------------------------------------------'
print Current_Timestamp