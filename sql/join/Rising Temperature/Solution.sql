select  today.id from Weather as today cross join Weather as yesterday where today.recordDate - yesterday.recordDate = 1 and today.temperature > yesterday.temperature; -- postgres

select  today.id from Weather as today cross join Weather as yesterday where datediff(day, yesterday.recordDate, today.recordDate) = 1 and today.temperature > yesterday.temperature; -- sql server