/*insert into [VTSData].dbo.Users (
IsAdministrator, Name, Surname, RegisteredDate, LastActivityDate)
select IsAdministrator, Name, Surname, RegisteredDate,LastActivityDate
from [VTSDataBck2].dbo.Users

insert into [VTSData].dbo.vehicles (Vin, UserIdentifyer, Firm, Brand, Registered, ProductionYear,Mileage)
select Vin, UserIdentifyer, Firm, Brand, Registered, ProductionYear,Mileage
from [VTSDataBck2].dbo.vehicles

insert into [VTSData].dbo.Events (VehicleId, Date, Name, Mileage, Type, RedMileage)
select VehicleId, Date, Name, Mileage, Type, RedMileage
from [VTSDataBck2].dbo.Events

insert into [VTSData].dbo.LexiaDatasets (Date, Guid)
select Date, Guid from [VTSDataBck2].dbo.LexiaDatasets

insert into [VTSData].dbo.LexiaGraphsData (LexiaDatasetId)
select LexiaDatasetId from [VTSDataBck2].dbo.LexiaGraphsData

insert into [VTSData].dbo.LexiaTracesData (LexiaDatasetId)
select LexiaDatasetId from [VTSDataBck2].dbo.LexiaTracesData

insert into [VTSData].dbo.LexiaGraphSessions 
(VehicleId, Date, Summary, LexiaGraphsDataId)
select VehicleId, Date, Summary, LexiaGraphsDataId 
from [VTSDataBck2].dbo.LexiaGraphSessions

insert into [VTSData].dbo.LexiaTraces 
(Date, Application, Format, Phone, PhoneChannel, Vin, 
Address, ToolSerialNumber, ToolName, SavesetId, VehicleId, LexiaTracesDataId)
select Date, Application, Format, Phone, PhoneChannel,
Vin, Address, ToolSerialNumber, ToolName, 
SavesetId, VehicleId, LexiaTracesDataId
from [VTSDataBck2].dbo.LexiaTraces

insert into [VTSData].dbo.LexiaChannelDatas
(Type, LexiaGraphSessionId)
select Type, LexiaGraphSessionId
from [VTSDataBck2].dbo.LexiaChannelDatas

insert into [VTSData].dbo.LexiaParametersSets
(Name, LexiaTraceId)
select Name, LexiaTraceId
from [VTSDataBck2].dbo.LexiaParametersSets

insert into [VTSData].dbo.LexiaParameterDatas
(Type, Units, [Values], LexiaParametersSetId, Name)
select Type, Units, [Values], LexiaParametersSetId, Name
from [VTSDataBck2].dbo.LexiaParameterDatas

insert into [VTSData].dbo.LexiaChannelPoints
(TimeStamp, Value, LexiaChannelDataId)
select TimeStamp, Value, LexiaChannelDataId
from [VTSDataBck2].dbo.LexiaChannelPoints/*