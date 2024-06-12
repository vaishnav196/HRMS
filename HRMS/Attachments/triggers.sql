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


 create trigger InsteadOftrigger
 on Emp
 instead of insert
 as 
 begin
		 declare @salary decimal(9,2)
		 select @salary=salary from inserted;
		 if(@salary>20000)
		 begin
		 insert into Emp select name,salary from inserted;
		 end 

		 else
		 begin
		 print'Salary should be greater than 20000 to be inserted';
		 end
 end

 insert into Emp values('raj',150000);

 select * from Emp;



 create trigger createTriggers
 on database
 for create_table
 as
 begin
 print'Table Created succesfully';
 end

 create table Task(
 id int primary key identity,
 taskname varchar(100)
 );

 create proc CalcSalary
 @netsalary decimal(9,2) output
 as 
 begin
		declare @basicsal decimal(9,2);
		declare @pfAmt decimal(9,2);
		declare @pf decimal(9,2);
		set @basicsal=5000;
		set @pf=0.1;
		set @pfAmt=@basicsal* @pf;
		set @netsalary=@basicsal-@pfAmt;

 end

 declare @netsalary decimal(9,2) 
 exec CalcSalary @netsalary=@netsalary output;
 print cast(@netsalary as varchar(100))


 Create  proc Crud
 @id int=null,
 @name varchar(100)=null,
 @salary decimal(9,2)=null,
 @choice int 
 as
 begin

		if(@choice=1)
		begin
		insert into Emp values (@name,@salary);
		end

		if(@choice=2)
		begin
		select * from Emp;
		end

		else if(@choice=3)
		begin
		update  Emp set name=@name,salary=@salary where id=@id;
		end

		else
		begin
		delete from Emp where id=@id;
		end
 end
 --select
 exec Crud @choice=2;
 --update 
 exec Crud  @id=4,@name='ram ram',@salary=21000,  @choice=3;
 --insert
 exec Crud @name='hello',@salary=100000,@choice=1;
 --delete
 exec Crud @id=6,@choice=4;


alter proc PrintMsg
 as
 begin
 print 'inserted the data in the table '
 end 


 alter trigger CallTriggered
 on Emp
 after insert
 as 
 begin
 exec PrintMsg 
 end

 insert into Emp values('salam boiiiii',2550000);

 select * from Emp;
 select * from tracker;



 create table emps(
 id int primary key identity,
 name varchar(100),
 salary decimal(9,2)
 );

 create proc PrintMsgs
 as
 begin
 print 'inserted the data in the table '
 end 


 create trigger CallT
 on emps
 after insert
 as 
 begin
 exec PrintMsgs 
 end

  insert into emps values('salam boiiiii',2550000);
  select
  select * from emps;

--scalllar function

create function FinSalary(@name varchar(100))
returns decimal(9,2)
as 
begin
declare @sal decimal(9,2);
select @sal=salary from Emp where name=@name
return @sal;
end

select * from Emp;

--executuing udf
select dbo.FinSalary('vaishnav') as Salary;

--table valued function/inline table valued fucntion

create function FindName(@name varchar(100))
returns table
as
return
select * from Emp where name=@name;

--executing inline table valued functtion
select * from FindName('vaishnav');


