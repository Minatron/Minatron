print Current_Timestamp
print '---������� �������-----------------------------------------' 

DROP TABLE [WeighData]

print '�������� ��������� ������� (�� �������� �������� �� ������)' 
print Current_Timestamp

print '---������� �������-----------------------------------------'
create table WeighData(
	WaighID BIGINT IDENTITY NOT NULL, 
	WaighTime DATETIME2 default Sysutcdatetime()  not null, 
	WaighMass float default 0 not null,
	WaighSpeed float default 0 not null

	primary key (WaighID)
) 

go


print '---�����---------------------------------------------------'
print Current_Timestamp