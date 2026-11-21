select E.name from Employee as E join (select managerId from Employee group by managerId having count(*) >= 5) M on E.id = M.managerId;
-- postgres, mysql, sql server