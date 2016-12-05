USE [Quarry]
GO

/****** Object:  StoredProcedure [dbo].[pa_ObjectSelect]    Script Date: 11/22/2016 10:34:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pa_ArmRegistrSelect]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pa_ArmRegistrSelect]
GO

/****** Object:  StoredProcedure [dbo].[pa_ObjectSelect]    Script Date: 05/20/2016 16:41:44 ******/
CREATE PROCEDURE [dbo].[pa_ArmRegistrSelect] 
AS
BEGIN
	SET NOCOUNT ON;
	declare @bitTrue as bit, @bitFalse as bit
	select @bitTrue=1, @bitFalse=0
	SELECT 
		o.ObjectId
		,isnull(e.number,v.number) as _Number
		,dbo.fn_getFullDescription(o.ObjectId) as [_Object]
		,o.InField
		, case when f.maxhChanged is null then case when o.InField =1 then 'На смене' else 'Не на смене' end 
			else CONVERT(varchar(50),f.maxhChanged,20) end as InFieldTime
		, Code
		--, o.ServiceId
		, s.Name as ServiceName
		, s.Chief 
		, s.Phone
		, o.ObjectTypeId
		, ot.Name as ObjectTypeName
		, [Description]
		--, [Time]
		--, DisconnectedEventCreated
		--, BaseStationId as BaseStationId
		--, BaseStationTime
		--, Location.Lat as Latitude
		--, Location.Long as Longitude
		--, Altitude
		--, SatelliteUsage
		--, NoGpsSignal
		--, Azimuth
		--, Speed
		, Charge
		, FuelLevel
		--, Caution
		--, [Emergency]
		--, [Notification]
		--, Answer
		--, SOS
		--, PacketError
		, Error
		, ErrorCode
		--, NoMotion
		--, PacketLossRate
		--, IsDeleted
		
		, v.FuelLevelMax
		, vt.Name as VehicleTypeName
		, e.Surname
		, e.Name
		, e.Patronymic
		, e.Position
		--, case when DATEDIFF(hour,isnull(f.maxhChanged,GETUTCDATE()),GETUTCDATE())>12 then @bitTrue  else @bitFalse end as LongTimeInField
		, case when o.InField=1 then DATEDIFF(hour,isnull(f.maxhChanged,GETUTCDATE()),GETUTCDATE())else 0 end as LongTime
		
	from dbo.[Object] o
		inner join ObjectType ot on o.ObjectTypeId=ot.ObjectTypeId
		inner join [Service] s on o.ServiceId=s.ServiceId
		left outer join Vehicle v on o.ObjectId=v.ObjectId
		left outer join VehicleType vt on v.VehicleTypeId=vt.VehicleTypeId
		left outer join Employee e on o.ObjectId=e.ObjectId
		left outer join 
			(select ObjectId, MAX(hChanged) as maxhChanged
			from ObjectInFieldH
			group by ObjectId) f on o.ObjectId = f.ObjectId
	where isnull(IsDeleted,0)=0 and o.ObjectTypeId!=4
		
END

