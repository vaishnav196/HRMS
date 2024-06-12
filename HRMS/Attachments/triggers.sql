create database testing;
use testing;
create table Emp(
id int  primary key identity,
name varchar(100),
salary decimal(9,2)

);

alter proc InsertOrUpdate
@Id int =null,
@Name varchar(100),
@salary decimal(9,2)
as
begin

if(@Id is null)
begin
insert into Emp(name ,salary) values (@Name,@salary);
end
else
begin
update Emp set name=@Name,salary=@salary where id=@Id;
end

end
Go


exec InsertOrUpdate  @id=2, @Name='y',@salary=1000;
exec InsertOrUpdate  1,'yogesh',1000;
select* from Emp;


drop proc InsertOrUpdate;

create trigger InsertTrigger
on Emp
after insert
as 
begin
 print' Data Inserted succesfully';
 end


 create table tracker(
 id int primary key identity,
 name varchar(100),
 salary decimal (9,2),
 mname varchar(100)

 );

 create trigger InsertTrigger2
 on Emp
 after insert
 as 
 begin
 insert into tracker 
 select name ,salary ,'johhhh' from inserted;
 end


 alter trigger UpdateTrigger
 on Emp
 after update
 as 
 begin
 update tracker set name=(select name from inserted),salary=(select salary from inserted)
 where id in (select id from inserted);
 end

 update Emp set name='RR',salary='2' where id=1

 insert into Emp values('ram',10);
 select * from tracker;

 select * from Emp;



 create  trigger DeleteTrigger
 on Emp
 after delete
 as
 begin
 insert into tracker (name,salary,mname)
 select name ,salary,'john' from deleted;
 end
  
   drop trigger DeleteTrigger;
   select * from Emp;
   select * from tracker;
 delete from Emp where id=1;