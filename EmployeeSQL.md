Examples of an employee table iterative fill to walk a tree embedded with a table. 
```SQL

CREATE TABLE [dbo].[Employee](
	[E_ID] [int] IDENTITY(1,1) NOT NULL,
	[E_ManagerEID] [int] NULL,	
	[E_Name] [varchar](max) NULL,	
  CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([E_ID] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

  Go 

  set Identity_Insert dbo.Employee on   -- so we can insert primary key.  

  GO

  insert into dbo.Employee ( E_ID, E_ManagerEID, E_Name )
    select 1, 4, 'James' union 
    select 2, 4, 'John' union 
    select 3, 4, 'Jessie' union 
    select 4, 8, 'Jake' union    

    select 5, 1, 'Jay' union 
    select 6, 2, 'Jim' union 
    select 7, 3, 'Johnsen' union 
    select 8, 12, 'Eric' union 

    select 9, 8, 'Mark' union 
    select 10, 12, 'Mindy' union 
    select 11, 3, 'Madison' union 
    select 12, 24,'Marvin' 

GO

set Identity_Insert dbo.Employee off  -- Turn table identity insert back on. 

Go  -- test 

select * from Employee 

go 


Create Procedure dbo.GetManagedEmployee(@aEmployeeID int) As   
  declare @aTempTbl table ( EID int )                       -- setup temp list var among others
  insert into @aTempTbl (EID) 
    select E_ID from Employee where E_ID = @aEmployeeID  
  declare @aCount int set @aCount = (select count(*) from @aTempTbl )
  Declare @aLastCount int set @aLastCount = 0
  while (@aCount <> @aLastCount) begin                      --  main loop, if table didn't grow then stop.
    insert into @aTempTbl (EID)                             --  Add the Managed Employee to the list
      select E_ID from Employee 
        where (E_ManagerEID in (select EID from @aTempTbl)) and (E_ID not in (select EID from @aTempTbl)) 
    set @aLastCount = @aCount                               -- Reset the counts for the while check.
    set @aCount = (select count(*) from @aTempTbl)          -- 
  end 
  -- Create output
  select * from Employee where E_ID in (select EID from @aTempTbl)
return 

go 

select * from Employee 

Go 

exec dbo.GetManagedEmployee 12



```